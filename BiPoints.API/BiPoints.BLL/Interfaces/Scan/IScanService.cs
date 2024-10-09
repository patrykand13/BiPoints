using BiPoints.Common.DTO.Response.Scan;

namespace BiPoints.BLL.Interfaces.Scan
{
    public interface IScanService
    {
        Task<ItemResponse> AddScanAsync(Guid userId, string result);
    }
}
