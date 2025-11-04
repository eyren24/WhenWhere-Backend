using System.ComponentModel.DataAnnotations;
using DTO.Agenda;

namespace Repository.interfaces;

public interface IAgendaRepo
{
    Task<int> AddAsync(ReqAgendaDTO agenda);
    Task UpdateAsync(int id, ReqUpdateAgenda agendaUpdt);
    Task RemoveAsync(int agendaId);
    Task<List<ResAgendaDTO>> GetPersonalAgenda();
    Task<ResAgendaDTO> GetById(int id);
    Task<List<ResAgendaDTO>> GetAll();
    Task<List<ResAgendaDTO>> ListTopAgendeAsync();
    Task<List<ResAgendaDTO>> GetAllLiked();
    Task<List<ResAgendaDTO>> GetByOwner(string username);
}