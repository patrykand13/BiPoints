using System;
using System.Threading.Tasks;

namespace BiPoints.Interfaces.Scan
{
    internal interface IGetScanHistoryService
    {
        Task<string> GetScanHistoryListAsync(Guid userId, int skipRecords);
    }
}
