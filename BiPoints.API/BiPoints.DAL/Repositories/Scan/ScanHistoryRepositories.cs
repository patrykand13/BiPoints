using BiPoints.API;
using BiPoints.Common.DTO.Model.Scan;
using BiPoints.Common.Exceptions;
using BiPoints.DAL.Interfaces.Scan;
using Microsoft.EntityFrameworkCore;

namespace BiPoints.DAL.Repositories.Scan
{
    public class ScanHistoryRepositories : IScanHistoryRepositories
    {
        private readonly AppDbContext _context;
        public ScanHistoryRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ScanHistoryDTO>> GetScanHistoryAsync(Guid userId, int skipRecords)
        {
            try
            {
                var scanHistoryList = await (from u in _context.ScanHistories
                                             where u.UserId == userId
                                             join item in _context.Items on u.CodeQr equals item.CodeQr
                                             orderby u.AddDate descending
                                             select new ScanHistoryDTO
                                             {
                                                 Image = item.Image,
                                                 Name = item.Name,
                                                 AddDate = u.AddDate,
                                                 Points = u.Points,
                                                 ScanSuccess = u.ScanSuccess
                                             }).Skip(skipRecords).Take(20).ToListAsync();
                return scanHistoryList;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while updating the database.", "ScanHistoryRepositories/GetScanHistoryAsync", ex);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while getting scan history.", "ScanHistoryRepositories/GetScanHistoryAsync", ex);
            }
        }
        public async Task<int> GetTodaysPointsAsync(Guid userId)
        {
            try
            {
                var points = await (from u in _context.ScanHistories
                                    where u.UserId == userId && u.AddDate > DateTime.Today && u.ScanSuccess
                                    orderby u.AddDate descending
                                    select u.Points).SumAsync();
                return points;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while updating the database.", "ScanHistoryRepositories/GetTodaysPointsAsync", ex);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while getting todays points.", "ScanHistoryRepositories/GetTodaysPointsAsync", ex);
            }
        }
        public async Task<int> GetWeekPointsAsync(Guid userId)
        {
            try
            {
                var points = await (from u in _context.ScanHistories
                                    where u.UserId == userId && u.AddDate > DateTime.Today.AddDays(-7) && u.ScanSuccess
                                    orderby u.AddDate descending
                                    select u.Points).SumAsync();
                return points;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while updating the database.", "ScanHistoryRepositories/GetWeekPointsAsync", ex);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while getting week points.", "ScanHistoryRepositories/GetWeekPointsAsync", ex);
            }
        }
    }
}
