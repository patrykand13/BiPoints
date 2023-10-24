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
                // Retrieve scan history data from the repository.
                var scanHistoryData = _scanRepositories.GetScanHistory(userId, skipRecords);

                // Check if scan history data is available.
                if (scanHistoryData == null)
                {
                    return new BaseResponse
                    {
                        Status = "Success"
                    };
                }

                // Transform scan history data into appropriate response objects.
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

                // Return a response with processed scan history data.
                return new BaseResponse
                {
                    Status = "Success",
                    Obj = mappedResults
                };
            }
            catch
            {
                // If an error occurred during processing, return an internal error response.
                return new BaseResponse
                {
                    Status = "InternalError",
                    Message = "ErrorGetDataFailed"
                };
            }
        }

    }
}
