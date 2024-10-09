using BiPoints.BLL.Interfaces.Scan;
using BiPoints.Common.DTO.Response.Scan;
using BiPoints.Common.Exceptions;
using BiPoints.DAL.Entities;
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
        public async Task<ItemResponse> AddScanAsync(Guid userId, string result)
        {
            try
            {
                // Retrieve product information based on the scanned QR code.
                var item = await _itemRepositories.GetItemByCodeQrAsync(result);

                // If the product with the given QR code doesn't exist, return an error.
                if (item == null)
                    throw new BusinessException("The item doesn't exist.");

                // Check if the code has already been scanned by the user.
                var scanExist = await _scanRepositories.CheckIfTheCodeWasScannedAsync(result);

                // Add the scan to the history.
                var scanHistory = new ScanHistoryEntity
                {
                    UserId = userId,
                    CodeQr = result,
                    Points = item.Points,
                    ScanSuccess = !scanExist,
                    AddDate = DateTime.Now,
                };
                var isSuccess = await _scanRepositories.AddScanAsync(scanHistory);

                // If the scan addition operation failed, return an error.
                if (!isSuccess)
                    throw new BusinessException("Add scan failed.");

                // If the code was previously scanned, return a duplicate error.
                if (scanExist)
                {
                    // Save the changes made in the repository.
                    await _saveRepositories.Save();
                    throw new BusinessException("Duplicate.");
                }

                // Add the earned points to the user's account.
                isSuccess = await _addPointsRepositories.AddPointsAsync(userId, item.Points);

                // If the points addition operation failed, return an error.
                if (!isSuccess)
                    throw new BusinessException("Add points failed.");

                // Save the changes made in the repository.
                await _saveRepositories.Save();

                // Prepare a response containing information about the product.
                ItemResponse itemResponse = new ItemResponse
                {
                    Name = item.Name,
                    Image = item.Image
                };

                // Return a success response with product data.
                return itemResponse;

            }
            catch (DataAccessException ex)
            {
                throw new BusinessException(ex.Message, "ScanService/AddScanAsync");
            }
            catch (BusinessException ex)
            {
                throw new BusinessException(ex.Message, "ScanService/AddScanAsync");
            }
            catch (Exception)
            {
                throw new BusinessException("An unexpected error occurred in the business logic.", "ScanService/AddScanAsync");
            }
        }

    }
}
