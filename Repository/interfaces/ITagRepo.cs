using DTO.Tag;

namespace Repository.interfaces;

public interface ITagRepo
{
    Task<int> AddAsync(ReqTagDTO tag);
    Task UpdateAsync(int id, ReqUpdateTagDTO tagUpdt);
    Task RemoveAsync(int tagId);
    Task<List<ResTagDTO>> GetListAsync();
}