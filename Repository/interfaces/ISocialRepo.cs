using DTO.Agenda;
using DTO.social;

namespace Repository.services.social;

public interface ISocialRepo
{
    Task<List<ResSocialDTO>> ListTopAgendeAsync();
    Task<List<ResSocialDTO>> GetUtenteByUsernameAsync(string username);
}