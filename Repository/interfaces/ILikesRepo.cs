using DTO.Likes;

namespace Repository.interfaces;


public interface ILikesRepo
{
    Task<List<ResLikesDTO>> GetListByUserIdAsync(int id);
    Task<List<ResLikesDTO>> GetallLikesasync(int id);
    Task<int> AddLikeAsync(ReqLikesDTO like);
    Task RemoveLikeAsync(int id);
}