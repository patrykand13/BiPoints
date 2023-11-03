using System;
using System.Threading.Tasks;

namespace BiPoints.Interfaces.Scan
{
    interface IScanPointsServices
    {
        Task<string> GetPointsInformations(Guid userId);
    }
}
