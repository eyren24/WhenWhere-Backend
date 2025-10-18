using System.ComponentModel.DataAnnotations;
using DTO.Agenda;

namespace Repository.interfaces;

public interface IAgendaRepo
{
    Task<int> AddAsync(ReqAgendaDTO agenda);
    Task UpdateAsync(int id, ReqUpdateAgenda agendaUpdt);
    Task RemoveAsync(int agendaId);
    Task<List<ResAgendaDTO>> GetListAsync();
    Task<ResAgendaDTO> GetById(int id);
    Task<ResAgendaDTO> GetByOwner(string username);
}