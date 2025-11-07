using System.Net.Http.Json;
using System.Text.Json;
using DTO.mail;
using Repository.interfaces;

namespace Repository.services.mail;

public class MailTrapRepository : IMailTrapRepository
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "f88ae43a1eb6875d3e92055280faec38";
    private const string BaseUrl = "https://send.api.mailtrap.io/api/send"; // ✅ endpoint corretto (no /v2)

    public MailTrapRepository()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiKey);
    }

    // === Mail di benvenuto ===
    public async Task SendWelcomeMailAsync(string userEmail, string nomeCompleto, string code)
    {
        if (!string.Equals(userEmail, "matteo-martelli@outlook.it", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"MailTrap disattivato per {userEmail}");
            return;
        }
        var email = new
        {
            from = new { email = "noreply@whenwhere.com", name = "When&Where" },
            to = new[] { new { email = userEmail, name = nomeCompleto } },
            template_uuid = "20ef81b9-189c-4828-81b4-44eb58d4005c",
            template_variables = new
            {
                NOME_UTENTE = nomeCompleto,
                CODICE = code
            }
        };

        var response = await _httpClient.PostAsJsonAsync(BaseUrl, email);
        var body = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Mailtrap → {(int)response.StatusCode} {response.StatusCode}");
        Console.WriteLine(body);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Mailtrap error {response.StatusCode}: {body}");
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
        var email = new
        {
            from = new { email = "noreply@whenwhere.com", name = "When&Where" },
            to = new[] { new { email = destinatarioEmail, name = nomePartecipante } },
            template_uuid = "d3841573-196f-4a5d-b6b5-fd38689b5f5f", // UUID template promemoria
            template_variables = new
            {
                NOME_EVENTO = nomeEvento,
                NOME_PARTECIPANTE = nomePartecipante,
                DATA_EVENTO = dataEvento.ToString("dd/MM/yyyy"),
                ORA_EVENTO = oraEvento.ToString(@"hh\\:mm"),
                LUOGO_EVENTO = luogoEvento,
                ANNO = DateTime.Now.Year.ToString(),
                NOME_ORGANIZZAZIONE = "When&Where"
            }
        };

        var response = await _httpClient.PostAsJsonAsync(BaseUrl, email);
        var body = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Mailtrap → {(int)response.StatusCode} {response.StatusCode}");
        Console.WriteLine(body);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Mailtrap error {response.StatusCode}: {body}");
    }
}