using BiPoints.BLL.DTO.Response;
using BiPoints.BLL.DTO.Response.Scan;
using BiPoints.BLL.Interfaces.Scan;
using BiPoints.DAL.Interfaces.Point;
using BiPoints.DAL.Interfaces.Scan;

namespace BiPoints.BLL.Services.Scan
{
    public class ScanHistoryService : IScanHistoryService
    {
        private readonly IScanHistoryRepositories _scanRepositories;
        private readonly IGetPointRepositories _pointRepositories;
        public ScanHistoryService(IScanHistoryRepositories scanRepositories, IGetPointRepositories pointRepositories)
        {
            _scanRepositories = scanRepositories;
            _pointRepositories = pointRepositories;
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

        public async Task<BaseResponse> GetPointsInformations(Guid userId)
        {
            try
            {
                // Get the user's points for today
                var todaysPoints = _scanRepositories.GetTodaysPoints(userId);

                // Get the user's points for the current week
                var weekPoints = _scanRepositories.GetWeekPoints(userId);

                // Get the user's general points
                var biPoints = _pointRepositories.GetPointsByUserId(userId);

                // Check if general points retrieval was successful
                if (biPoints == null)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorGetBiPointsDataFailed" // Error - failed to retrieve general points data (biPoints)
                    };
                }

                // Create a response object with calculated points data
                PointsInformationResponse mappedResults = new PointsInformationResponse
                {
                    DayAchievementToGet = 60, // Daily achievement goal to reach (pre-defined)
                    WeekAchievementToGet = 300, // Weekly achievement goal to reach (pre-defined)
                    BiPoints = biPoints.BiPoints, // User's general points
                    BiPointsForUse = biPoints.BiPointsForUse, // General points available for use
                    BiPointsUsed = biPoints.BiPointsUsed // General points already used
                };

                // Calculate daily achievement progress
                mappedResults.DayAchievementProgress = Math.Round((double)todaysPoints / (double)mappedResults.DayAchievementToGet, 2);
                if (mappedResults.DayAchievementProgress > 1) mappedResults.DayAchievementProgress = 1;
                mappedResults.DayAchievementValue = (int)(mappedResults.DayAchievementProgress * 100);
                // Determine the color of the daily achievement progress based on the value
                mappedResults.DayAchievementColor = mappedResults.DayAchievementValue > 80 ? "#00AA00" : mappedResults.DayAchievementValue > 30 ? "#FFFF00" : "#AA0000";

                // Calculate weekly achievement progress
                mappedResults.WeekAchievementProgress = Math.Round((double)todaysPoints / (double)mappedResults.WeekAchievementToGet, 2);
                if (mappedResults.WeekAchievementProgress > 1) mappedResults.WeekAchievementProgress = 1;
                mappedResults.WeekAchievementValue = (int)(mappedResults.WeekAchievementProgress * 100);
                // Determine the color of the weekly achievement progress based on the value
                mappedResults.WeekAchievementColor = mappedResults.WeekAchievementValue > 80 ? "#00AA00" : mappedResults.WeekAchievementValue > 30 ? "#FFFF00" : "#AA0000";

                return new BaseResponse
                {
                    Status = "Success", // Success - data was successfully processed
                    Obj = mappedResults // Object with calculated points data
                };
            }
            catch
            {
                // If an error occurred during processing, return an internal error response.
                return new BaseResponse
                {
                    Status = "InternalError", // Internal error - an error occurred during data processing
                    Message = "ErrorGetDataFailed" // Error - failed to retrieve data
                };
            }
        }


    }
}
