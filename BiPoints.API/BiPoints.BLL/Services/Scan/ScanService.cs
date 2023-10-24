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
                // Retrieve product information based on the scanned QR code.
                var item = _itemRepositories.GetItemByCodeQr(result);

                // If the product with the given QR code doesn't exist, return an error.
                if (item == null)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorTheItemDoesntExist"
                    };
                }

                // Check if the code has already been scanned by the user.
                var scanExist = _scanRepositories.CheckIfTheCodeWasScanned(result);

                // Add the scan to the history.
                var isSuccess = await _scanRepositories.AddScan(userId, result, item.Points, !scanExist);

                // If the scan addition operation failed, return an error.
                if (!isSuccess)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorAddFailed"
                    };
                }

                // If the code was previously scanned, return a duplicate error.
                if (scanExist)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorDuplicate"
                    };
                }

                // Add the earned points to the user's account.
                isSuccess = _addPointsRepositories.AddPoints(userId, item.Points);

                // If the points addition operation failed, return an error.
                if (!isSuccess)
                {
                    return new BaseResponse
                    {
                        Status = "Error",
                        Message = "ErrorAddFailed"
                    };
                }

                // Save the changes made in the repository.
                await _saveRepositories.Save();

                // Prepare a response containing information about the product.
                ItemResponse itemResponse = new ItemResponse
                {
                    Name = item.Name,
                    Image = item.Image
                };

                // Return a success response with product data.
                return new BaseResponse
                {
                    Status = "Success",
                    Obj = itemResponse
                };
            }
            catch
            {
                // If an error occurred during processing, return an internal error response.
                return new BaseResponse
                {
                    Status = "InternalError",
                    Message = "ErrorAddFailed"
                };
            }
        }

    }
}
