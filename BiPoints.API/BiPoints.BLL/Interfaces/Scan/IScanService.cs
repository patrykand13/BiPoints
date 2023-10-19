using BiPoints.BLL.DTO.Response;

namespace BiPoints.BLL.Interfaces.Scan
{
    public interface IScanService
    {
        Task<BaseResponse> AddScan(Guid userId, string result);
    }
}
