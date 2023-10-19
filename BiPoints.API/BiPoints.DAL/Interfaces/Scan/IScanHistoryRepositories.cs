namespace BiPoints.DAL.Interfaces.Scan
{
    public interface IScanHistoryRepositories
    {
        Task<bool> AddScan(Guid userId, string codeQr, int points, bool scanSuccess);
        bool CheckIfTheCodeWasScanned(string codeQr);
    }
}
