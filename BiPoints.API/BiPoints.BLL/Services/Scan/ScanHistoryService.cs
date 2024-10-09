using BiPoints.BLL.Interfaces.Scan;
using BiPoints.Common.DTO.Response.Scan;
using BiPoints.Common.Exceptions;
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
        public async Task<List<ScanHistoryItemResponse>> GetScanHistoryAsync(Guid userId, int skipRecords)
        {
            try
            {
                // Retrieve scan history data from the repository.
                var scanHistoryData = await _scanRepositories.GetScanHistoryAsync(userId, skipRecords);

                // Check if scan history data is available.
                if (scanHistoryData == null)
                    return null;

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
                return mappedResults;
            }
            catch (DataAccessException ex)
            {
                throw new BusinessException(ex.Message, "ScanHistoryService/GetScanHistoryAsync");
            }
            catch (Exception)
            {
                throw new BusinessException("An unexpected error occurred in the business logic.", "ScanHistoryService/GetScanHistoryAsync");
            }
        }

        public async Task<PointsInformationResponse> GetPointsInformationsAsync(Guid userId)
        {

            try
            {// Get the user's points for today
                var todaysPoints = await _scanRepositories.GetTodaysPointsAsync(userId);

                // Get the user's points for the current week
                var weekPoints = await _scanRepositories.GetWeekPointsAsync(userId);

                // Get the user's general points
                var biPoints = await _pointRepositories.GetPointsByUserIdAsync(userId);

                // Check if general points retrieval was successful
                if (biPoints == null)
                    throw new BusinessException("Get BiPoints data failed");// Error - failed to retrieve general points data (biPoints)

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
                if (mappedResults.DayAchievementProgress > 1)
                    mappedResults.DayAchievementProgress = 1;
                mappedResults.DayAchievementValue = (int)(mappedResults.DayAchievementProgress * 100);
                // Determine the color of the daily achievement progress based on the value
                mappedResults.DayAchievementColor = mappedResults.DayAchievementValue > 80 ? "#00AA00" : mappedResults.DayAchievementValue > 30 ? "#FFD64D" : "#AA0000";

                // Calculate weekly achievement progress
                mappedResults.WeekAchievementProgress = Math.Round((double)weekPoints / (double)mappedResults.WeekAchievementToGet, 2);
                if (mappedResults.WeekAchievementProgress > 1)
                    mappedResults.WeekAchievementProgress = 1;
                mappedResults.WeekAchievementValue = (int)(mappedResults.WeekAchievementProgress * 100);
                // Determine the color of the weekly achievement progress based on the value
                mappedResults.WeekAchievementColor = mappedResults.WeekAchievementValue > 80 ? "#00AA00" : mappedResults.WeekAchievementValue > 30 ? "#FFD64D" : "#AA0000";

                return mappedResults;
            }
            catch (DataAccessException ex)
            {
                throw new BusinessException(ex.Message, "ScanHistoryService/GetPointsInformationsAsync");
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, "ScanHistoryService/GetPointsInformationsAsync");
            }
            catch (Exception)
            {
                throw new BusinessException("An unexpected error occurred in the business logic.", "ScanHistoryService/GetPointsInformationsAsync");
            }
        }


    }
}
