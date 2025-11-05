namespace Repository.interfaces;

public interface IMailTrapRepository
{
    Task SendWelcomeMailAsync(string userEmail, string nomeCompleto, string link);
    Task SendEventoReminderAsync(string destinatarioEmail, string nomePartecipante, string nomeEvento, DateTime dataEvento, TimeSpan oraEvento, string luogoEvento);
}