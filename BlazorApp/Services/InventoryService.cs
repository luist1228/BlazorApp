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
        private readonly AuthService authService;

        public InventoryService(HttpClient httpClient, NavigationManager navigationManager, AuthService authService)
        {
            this.httpClient = httpClient;
            this.authService = authService;
            this.navigationManager = navigationManager;
        }

        public async  Task<GetInventoryResponse> GetInventoryAsync(int productId)
        {
            authService.GetProtectedClient();
            var response = await httpClient.GetAsync($"{url}/{productId}");
            bool check = authService.CheckIfAuthorized(response);
            if (check)
            {
                var refreshTokenResonse = await authService.GetRefreshToken();
                if (refreshTokenResonse.Flag == false)
                {
                    navigationManager.NavigateTo("/", true);
                }
                return null!;
            }
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
