using System;
using System.Threading.Tasks;

namespace BiPoints.Interfaces.Scan
{
    internal interface IGetScanHistoryService
    {
        Task<string> GetScanHistoryList(Guid userId, int skipRecords);
    }
}
