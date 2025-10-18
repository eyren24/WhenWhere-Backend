using DTO.Utente;

namespace Repository.interfaces;

public interface IUtenteRepo
{
    Task UpdateAsync(int id, ReqUpdateUtenteDTO utenteUpdate);
    Task ToggleStatusAsync(int id);
    Task<List<ResUtenteDTO>> GetListAsync(FiltriUtenteDTO filtri);
    Task<ResUtenteDTO> GetUtenteByIdAsync(int id);
    Task<ResUtenteDTO> GetUtenteByUsernameAsync(string username);
}