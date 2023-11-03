using BiPoints.BLL.DTO.Response;

namespace BiPoints.BLL.Interfaces.Scan
{
    public interface IScanHistoryService
    {
        Task<BaseResponse> GetScanHistory(Guid userId, int skipRecords);
        Task<BaseResponse> GetPointsInformations(Guid userId);
    }
}
