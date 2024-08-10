using BlazorApp.DTOs;
using static BlazorApp.Responses.CustomResponses;

namespace BlazorApp.Services
{
    public interface IProductService
    {
        Task<GetProductsResponse> GetProductsAsync();
        Task<RegisterProductResponse> RegisterProductAsync(ProductDTO model);

        Task<DeleteProductResponse> DeleteProductAsync(int ID);

    }
}
