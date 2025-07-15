using DTO.Agenda;
using DTO.Evento;

namespace Repository.interfaces;

public interface IEventoRepo
{
    Task<List<ResEventoDTO>> GetAllAsync(int agendaId, FiltriAgendaDTO filtri);
    Task<int> AddAsync(ReqEventoDTO evento);
    Task RemoveAsync(ReqEventoDTO eventoId);
    Task UpdateAsync(int id, ReqUpdateEventoDTO eventoDto);
}