using BiPoints.API;
using BiPoints.DAL.Entities;
using BiPoints.DAL.Interfaces.Scan;

namespace BiPoints.DAL.Repositories.Scan
{
    public class ScanHistoryRepositories : IScanHistoryRepositories
    {
        private readonly AppDbContext _context;
        public ScanHistoryRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddScan(Guid userId, string codeQr, int points, bool scanSuccess)
        {
            var model = new ScanHistoryEntity
            {
                UserId = userId,
                CodeQr = codeQr,
                Points = points,
                ScanSuccess = scanSuccess,
                AddDate = DateTime.Now,
            };
            await _context.ScanHistories.AddAsync(model);
            return true;
        }

        public bool CheckIfTheCodeWasScanned(string codeQr)
        {
            return _context.ScanHistories.Any(x => x.CodeQr == codeQr);
        }
    }
}
