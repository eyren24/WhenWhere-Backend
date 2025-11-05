using System.Text.Json.Serialization;

namespace DTO.mail;

public class MailtrapEmailRequestDTO
{
    [JsonPropertyName("from")] // forza la serializzazione col nome giusto
    public MailtrapSenderDTO From { get; set; }

    [JsonPropertyName("to")] public List<MailtrapRecipientDTO> To { get; set; }

    [JsonPropertyName("template_uuid")] public string TemplateUuid { get; set; }

    [JsonPropertyName("template_variables")]
    public Dictionary<string, string> TemplateVariables { get; set; }
}