using System.Net.Http.Json;
using System.Text.Json;
using DTO.mail;
using Repository.interfaces;

namespace Repository.services.mail;

public class MailTrapRepository : IMailTrapRepository
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://send.api.mailtrap.io/api/send";

    private const string ApiKey = "f88ae43a1eb6875d3e92055280faec38";
    private static readonly HashSet<string> Allowed = new(StringComparer.OrdinalIgnoreCase)
    {
        "matteo-martelli@outlook.it"
    };

    public MailTrapRepository()
    {
        _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(15)
        };
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiKey);
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("WhenWhere-Mailer/1.0");
        _httpClient.DefaultRequestHeaders.Accept.ParseAdd("application/json");
    }

    public async Task SendWelcomeMailAsync(string userEmail, string nomeCompleto, string code)
    {
        if (!Allowed.Contains(userEmail))
        {
            Console.WriteLine($"MailTrap skipped → {userEmail}");
            return;
        }

        var payload = new
        {
            from = new { email = "noreply@whenwhere.com", name = "When&Where" }, // dominio VERIFICATO
            to = new[] { new { email = userEmail, name = nomeCompleto } },
            template_uuid = "20ef81b9-189c-4828-81b4-44eb58d4005c",
            template_variables = new
            {
                NOME_UTENTE = nomeCompleto,
                CODICE = code
            }
        };

        await SendAsync(payload);
    }

    public async Task SendEventoReminderAsync(
        string destinatarioEmail,
        string nomePartecipante,
        string nomeEvento,
        DateTime dataEvento,
        TimeSpan oraEvento,
        string luogoEvento)
    {
        if (!Allowed.Contains(destinatarioEmail))
        {
            Console.WriteLine($"MailTrap skipped → {destinatarioEmail}");
            return;
        }

        var payload = new
        {
            from = new { email = "noreply@whenwhere.com", name = "When&Where" }, // dominio VERIFICATO
            to = new[] { new { email = destinatarioEmail, name = nomePartecipante } },
            template_uuid = "d3841573-196f-4a5d-b6b5-fd38689b5f5f",
            template_variables = new
            {
                NOME_EVENTO = nomeEvento,
                NOME_PARTECIPANTE = nomePartecipante,
                DATA_EVENTO = dataEvento.ToString("dd/MM/yyyy"),
                ORA_EVENTO = oraEvento.ToString(@"hh\:mm"),
                LUOGO_EVENTO = luogoEvento,
                ANNO = DateTime.UtcNow.Year.ToString(),
                NOME_ORGANIZZAZIONE = "When&Where"
            }
        };

        await SendAsync(payload);
    }

    private async Task SendAsync(object payload)
    {
        // Usa le Web defaults per camelCase
        var opts = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        using var resp = await _httpClient.PostAsJsonAsync(BaseUrl, payload, opts);
        var body = await resp.Content.ReadAsStringAsync();

        Console.WriteLine($"Mailtrap → {(int)resp.StatusCode} {resp.StatusCode}");
        Console.WriteLine(body);

        if (!resp.IsSuccessStatusCode)
        {
            // 401 = token errato; 403 = dominio non verificato; 422 = template/variabili errate
            throw new Exception($"Mailtrap error {(int)resp.StatusCode}: {body}");
        }
    }
}
