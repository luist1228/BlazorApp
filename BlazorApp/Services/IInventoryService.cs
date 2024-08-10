using BlazorApp.DTOs;
using static BlazorApp.Responses.InventoryReponses;

namespace BlazorApp.Services
{
    public interface IInventoryService
    {
        Task<GetInventoryResponse> GetInventoryAsync(int productId);

        Task<RegisterInventoryResponse> RegisterInventoryAsync(InventoryDTO model);
    }
}
