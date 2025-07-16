using DTO.Utente;

namespace Repository.interfaces;

public interface IUtenteRepo
{
    Task UpdateAsync(int id, ReqUpdateUtenteDTO utenteUpdate);
    Task ToggleStatusAsync(int id);
    Task<List<ResUtenteDTO>> GetListAsync(FiltriUtenteDTO filtri);
}