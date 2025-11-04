using DTO.social;

namespace Repository.interfaces;

public interface ISocialRepo
{
    Task<List<ResSocialDTO>> GetUtenteByUsernameAsync(string username);
}