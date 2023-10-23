using BiPoints.API;
using BiPoints.DAL.DAO;
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

        public List<ScanHistoryDao> GetScanHistory(Guid userId, int skipRecords)
        {
            var scanHistoryList = (from u in _context.ScanHistories
                                   where u.UserId == userId
                                   join item in _context.Items on u.CodeQr equals item.CodeQr
                                   select new ScanHistoryDao
                                   {
                                       Image = item.Image,
                                       Name = item.Name,
                                       AddDate = u.AddDate,
                                       Points = u.Points,
                                       ScanSuccess = u.ScanSuccess
                                   }).Skip(skipRecords).Take(20).ToList();
            return scanHistoryList;
        }
    }
}
