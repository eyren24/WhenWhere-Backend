using DTO.Likes;
using DTO.social;

namespace Repository.interfaces;

public interface ILikesRepo
{
    Task<List<ResLikesDTO>> GetListByUserIdAsync(int id);
    Task<ResLikesDTO> GetLikeByIdAsync(int id);
    Task<int> AddLikeAsync(ReqLikesDTO like);
    Task RemoveLikeAsync(int id);
    Task<List<ResLikesDTO>> GetLikeByAgendaIdAsync(int id);
    Task<bool> GetIfUserLikeAgenda(int agendaId);
}