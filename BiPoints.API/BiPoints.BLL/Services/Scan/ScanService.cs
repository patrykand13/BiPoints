using BiPoints.BLL.DTO.Response;
using BiPoints.BLL.DTO.Response.Scan;
using BiPoints.BLL.Interfaces.Scan;
using BiPoints.DAL.Interfaces;
using BiPoints.DAL.Interfaces.Item;
using BiPoints.DAL.Interfaces.Point;
using BiPoints.DAL.Interfaces.Scan;

namespace BiPoints.BLL.Services.Scan
{
    public class ScanService : IScanService
    {
        private readonly IScanRepositories _scanRepositories;
        private readonly IItemRepositories _itemRepositories;
        private readonly IAddPointsRepositories _addPointsRepositories;
        private readonly ISaveRepositories _saveRepositories;
        public ScanService(IScanRepositories scanRepositories, IItemRepositories itemRepositories, IAddPointsRepositories addPointsRepositories, ISaveRepositories saveRepositories)
        {
            _scanRepositories = scanRepositories;
            _itemRepositories = itemRepositories;
            _addPointsRepositories = addPointsRepositories;
            _saveRepositories = saveRepositories;
        }
        public async Task<BaseResponse> AddScan(Guid userId, string result)
        {
            try
            {
                var item = _itemRepositories.GetItemByCodeQr(result);
                if (item == null)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorTheItemDoesntExist"
                    };
                }
                var scanExist = _scanRepositories.CheckIfTheCodeWasScanned(result);
                var isSuccess = await _scanRepositories.AddScan(userId, result, item.Points, !scanExist);
                if (!isSuccess)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorAddFailed"
                    };
                }
                if (scanExist)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorDuplicate"
                    };
                }
                isSuccess = _addPointsRepositories.AddPoints(userId, item.Points);
                if (!isSuccess)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorAddFailed"
                    };
                }
                await _saveRepositories.Save();
                ItemResponse itemResponse = new ItemResponse
                {
                    Name = item.Name,
                    Image = item.Image
                };
                return new BaseResponse
                {
                    Status = "Success",
                    Obj = itemResponse
                };

            }
            catch
            {
                return new BaseResponse
                {
                    Status = "InternalError",
                    Message = "ErrorAddFailed"
                };
            }
        }
    }
}
