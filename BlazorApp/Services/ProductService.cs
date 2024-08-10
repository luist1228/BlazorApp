using BlazorApp.DTOs;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using static BlazorApp.Responses.CustomResponses;

namespace BlazorApp.Services
{
    public class ProductService: IProductService
    {
        private readonly HttpClient httpClient;
        private readonly string url = "api/Product";
        private readonly NavigationManager navigationManager;

        private readonly AuthService authService;

        public ProductService(HttpClient httpClient, AuthService authService, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.authService = authService;
            this.navigationManager = navigationManager;
        }

        public async Task<DeleteProductResponse> DeleteProductAsync(int ID)
        {
            var response = await httpClient.DeleteAsync($"{url}/{ID}");
            var result = await response.Content.ReadFromJsonAsync<DeleteProductResponse>();
            return result!;
        }

        public async Task<GetProductsResponse> GetProductsAsync()
        {
            authService.GetProtectedClient();
            var response = await httpClient.GetAsync(url);
            bool check = authService.CheckIfAuthorized(response);

            if (check)
            {
                var result = await authService.GetRefreshToken();
                if(result.Flag ==false)
                {
                    navigationManager.NavigateTo("/", true);
                }
                return null!;
            }

            return await response.Content.ReadFromJsonAsync<GetProductsResponse>();
        }

        public async Task<RegisterProductResponse> RegisterProductAsync(ProductDTO model)
        {
            var response = await httpClient.PostAsJsonAsync(url, model);
            var result = await response.Content.ReadFromJsonAsync<RegisterProductResponse>();
            return result!;
        }

    }
}
