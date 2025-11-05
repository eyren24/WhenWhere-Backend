using Database.Data;
using Microsoft.EntityFrameworkCore;
using Repository.interfaces;

namespace WhenWhereBackend;

public class EventReminderJob(IServiceProvider services) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await SendRemindersAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EventReminderJob] Errore: {ex.Message}");
            }

            // attende 24 ore prima del prossimo ciclo
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }

    private async Task SendRemindersAsync()
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var mailRepo = scope.ServiceProvider.GetRequiredService<IMailTrapRepository>();

        var targetDate = DateTime.Today.AddDays(2);

        // Include per navigare fino all'utente proprietario
        var eventi = await context.Evento
            .Include(e => e.agenda)
            .ThenInclude(a => a.utente)
            .Where(e => e.notifica && e.dataFine != null && e.dataFine.Value.Date == targetDate.Date)
            .ToListAsync();

        if (eventi.Count == 0)
        {
            Console.WriteLine("[EventReminderJob] Nessun evento da notificare oggi.");
            return;
        }

        foreach (var evento in eventi)
        {
            var utente = evento.agenda?.utente;
            if (utente == null || string.IsNullOrWhiteSpace(utente.email) || evento.dataFine == null)
                continue;

            try
            {
                var dataEvento = evento.dataFine.Value;
                var oraEvento = dataEvento.TimeOfDay;

                await mailRepo.SendEventoReminderAsync(
                    utente.email,
                    $"{utente.nome} {utente.cognome}",
                    evento.titolo,
                    dataEvento,
                    oraEvento, // ora come stringa
                    evento.luogo ?? "Da definire"
                );

                Console.WriteLine($"[EventReminderJob] Email inviata a {utente.email} per evento {evento.titolo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EventReminderJob] Errore invio a {evento.agenda?.utente?.email}: {ex.Message}");
            }
        }
    }
}
