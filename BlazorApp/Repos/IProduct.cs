using BlazorApp.DTOs;
using static BlazorApp.Responses.CustomResponses;

namespace BlazorApp.Repos
{
    public interface IProduct
    {
        Task<GetProductsResponse> GetProductsAsync();

        Task<RegisterProductResponse> RegisterProductAsync(ProductDTO model);


        Task<DeleteProductResponse> DeleteProductAsync(int ID);
    }
}
