using DTO.admin;

namespace Repository.interfaces;

public interface IAdminRepo
{
    Task<ResAdminStatsDTO> GetDashboardStatsAsync();
    Task<ResAdminAgendeStatsDTO> GetAgendeStatsAsync();
}