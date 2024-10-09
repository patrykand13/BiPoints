using BiPoints.API;
using BiPoints.Common.Exceptions;
using BiPoints.DAL.Entities;
using BiPoints.DAL.Interfaces.Scan;
using Microsoft.EntityFrameworkCore;

namespace BiPoints.DAL.Repositories.Scan
{
    public class ScanRepositories : IScanRepositories
    {
        private readonly AppDbContext _context;
        public ScanRepositories(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddScanAsync(ScanHistoryEntity scanHistory)
        {
            try
            {
                await _context.ScanHistories.AddAsync(scanHistory);
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while updating the database.", "ScanRepositories/AddScanAsync", ex);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while adding scanned.", "ScanRepositories/AddScanAsync", ex);
            }
        }

        public async Task<bool> CheckIfTheCodeWasScannedAsync(string codeQr)
        {
            try
            {
                return await _context.ScanHistories.AnyAsync(x => x.CodeQr == codeQr);
            }
            catch (DbUpdateException ex)
            {
                throw new DataAccessException("Error occurred while updating the database.", "ScanRepositories/CheckIfTheCodeWasScannedAsync", ex);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("An unexpected error occurred while checking scanned.", "ScanRepositories/CheckIfTheCodeWasScannedAsync", ex);
            }
        }
    }
}
