using System;
using System.Threading.Tasks;

namespace BiPoints.Interfaces.Scan
{
    interface IScanPointsServices
    {
        Task<string> GetPointsInformationsAsync(Guid userId);
    }
}
