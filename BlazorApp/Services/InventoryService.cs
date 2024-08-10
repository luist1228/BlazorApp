using BlazorApp.DTOs;
using BlazorApp.Responses;
using Microsoft.AspNetCore.Components;
using static BlazorApp.Responses.InventoryReponses;

namespace BlazorApp.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly HttpClient httpClient;
        private readonly string url = "api/Inventory";
        private readonly NavigationManager navigationManager;

        public InventoryService(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }

        public async  Task<GetInventoryResponse> GetInventoryAsync(int productId)
        {
            var response = await httpClient.GetAsync($"{url}/{productId}");
            var result = await response.Content.ReadFromJsonAsync<GetInventoryResponse>();
            return result!;
        }

        public async Task<RegisterInventoryResponse> RegisterInventoryAsync(InventoryDTO model)
        {
            var response = await httpClient.PostAsJsonAsync(url, model);
            var result = await response.Content.ReadFromJsonAsync<RegisterInventoryResponse>();
            return result!;
        }
    }
}
