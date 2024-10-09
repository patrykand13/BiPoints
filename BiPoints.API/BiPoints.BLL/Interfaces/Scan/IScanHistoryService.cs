using BiPoints.Common.DTO.Response.Scan;

namespace BiPoints.BLL.Interfaces.Scan
{
    public interface IScanHistoryService
    {
        Task<List<ScanHistoryItemResponse>> GetScanHistoryAsync(Guid userId, int skipRecords);
        Task<PointsInformationResponse> GetPointsInformationsAsync(Guid userId);
    }
}
