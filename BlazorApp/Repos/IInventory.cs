using BlazorApp.DTOs;
using BlazorApp.Responses;

namespace BlazorApp.Repos
{
    public interface IInventory
    {
        Task<InventoryReponses.GetInventoryResponse> GetInventoryAsync(int productId);

        Task<InventoryReponses.RegisterInventoryResponse> RegisterInventoryAsync(InventoryDTO model);
    }
}
