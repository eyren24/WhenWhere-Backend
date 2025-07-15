using System.ComponentModel.DataAnnotations;
using DTO.Agenda;
using DTO.Nota;

namespace Repository.interfaces;

public interface INotaRepo
{
    Task<int> AddAsync(ReqNotaDTO nota);
    Task UpdateAsync(int id, ReqNotaDTO notaUpdt);
    Task RemoveAsync(int notaId);

    Task<List<ResNotaDTO>> GetListAsync([Required] int agendaId, FiltriAgendaDTO filtri);
}