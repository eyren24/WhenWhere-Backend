using System.Net.Http.Json;
using DTO.mail;
using Repository.interfaces;

namespace Repository.services.mail;

public class MailTrapRepository : IMailTrapRepository
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "a5e614c727cc08b45aacb91b3604a2e5";
    private const string BaseUrl = "https://send.api.mailtrap.io/api/send";

    public MailTrapRepository()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiKey);
    }

    // === Mail di benvenuto ===
    public async Task SendWelcomeMailAsync(string userEmail, string nomeCompleto, string code)
    {
        var email = new MailtrapEmailRequestDTO
        {
            From = new MailtrapSenderDTO { Email = "support@whenwhere.com", Name = "When&Where" },
            To = [new MailtrapRecipientDTO { Email = userEmail, Name = nomeCompleto }],
            TemplateUuid = "20ef81b9-189c-4828-81b4-44eb58d4005c",
            TemplateVariables = new Dictionary<string, string>
            {
                { "NOME_UTENTE", nomeCompleto },
                { "CODICE", code }
            }
        };

        var response = await _httpClient.PostAsJsonAsync(BaseUrl, email);
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());
    }

    // === Promemoria evento ===
    public async Task SendEventoReminderAsync(
        string destinatarioEmail,
        string nomePartecipante,
        string nomeEvento,
        DateTime dataEvento,
        TimeSpan oraEvento,
        string luogoEvento)
    {
        var email = new MailtrapEmailRequestDTO
        {
            From = new MailtrapSenderDTO { Email = "support@whenwhere.com", Name = "When&Where" },
            To = [new MailtrapRecipientDTO { Email = destinatarioEmail, Name = nomePartecipante }],
            TemplateUuid = "3b8d0d0a-d5b2-4dc9-a248-13a3c27822e4", // <-- UUID del template promemoria Mailtrap
            TemplateVariables = new Dictionary<string, string>
            {
                { "NOME_EVENTO", nomeEvento },
                { "NOME_PARTECIPANTE", nomePartecipante },
                { "DATA_EVENTO", dataEvento.ToString("dd/MM/yyyy") },
                { "ORA_EVENTO", oraEvento.ToString(@"hh\:mm") },
                { "LUOGO_EVENTO", luogoEvento },
                { "ANNO", DateTime.Now.Year.ToString() },
                { "NOME_ORGANIZZAZIONE", "When&Where" }
            }
        };

        var response = await _httpClient.PostAsJsonAsync(BaseUrl, email);
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());
    }
}
