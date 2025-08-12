using DTO.Agenda;
using DTO.Evento;

namespace Repository.interfaces;

public interface IEventoRepo
{
    Task<List<ResEventoDTO>> GetAllAsync(int agendaId, FiltriAgendaDTO filtri);
    Task<int> AddAsync(ReqEventoDTO evento);
    Task RemoveAsync(int eventoId);
    Task UpdateAsync(int id, ReqUpdateEventoDTO eventoDto);
    Task<ResEventoDTO> GetByTitle(int agendaId, string titolo);
}