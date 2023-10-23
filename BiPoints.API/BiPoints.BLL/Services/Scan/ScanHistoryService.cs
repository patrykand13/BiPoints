using BiPoints.BLL.DTO.Response;
using BiPoints.BLL.DTO.Response.Scan;
using BiPoints.BLL.Interfaces.Scan;
using BiPoints.DAL.Interfaces.Scan;

namespace BiPoints.BLL.Services.Scan
{
    public class ScanHistoryService : IScanHistoryService
    {
        private readonly IScanHistoryRepositories _scanRepositories;
        public ScanHistoryService(IScanHistoryRepositories scanRepositories)
        {
            _scanRepositories = scanRepositories;
        }
        public async Task<BaseResponse> GetScanHistory(Guid userId, int skipRecords)
        {
            try
            {
                var scanHistoryData = _scanRepositories.GetScanHistory(userId, skipRecords);
                if (scanHistoryData == null)
                {
                    return new BaseResponse
                    {
                        Status = "Success"
                    };
                }
                List<ScanHistoryItemResponse> mappedResults = new List<ScanHistoryItemResponse>();

                foreach (var result in scanHistoryData)
                {
                    var scanHistoryItem = new ScanHistoryItemResponse
                    {
                        Image = result.Image,
                        Name = result.Name,
                        DateAdded = result.AddDate.ToString("dd.MM.yyyy"),
                        Points = result.Points,
                        SuccessVisible = result.ScanSuccess,
                        DuplicateVisible = !result.ScanSuccess,
                    };
                    mappedResults.Add(scanHistoryItem);
                }
                return new BaseResponse
                {
                    Status = "Success",
                    Obj = mappedResults
                };
            }
            catch
            {
                return new BaseResponse
                {
                    Status = "InternalError",
                    Message = "ErrorGetDataFailed"
                };
            }
        }
    }
}
