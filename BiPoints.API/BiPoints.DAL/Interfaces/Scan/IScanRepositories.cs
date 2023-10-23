namespace BiPoints.DAL.Interfaces.Scan
{
    public interface IScanRepositories
    {
        Task<bool> AddScan(Guid userId, string codeQr, int points, bool scanSuccess);
        bool CheckIfTheCodeWasScanned(string codeQr);
    }
}
