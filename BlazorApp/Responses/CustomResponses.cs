using BlazorApp.Models;

namespace BlazorApp.Responses
{
    public class CustomResponses
    {
        public record RegistrationResponse(bool Flag = false, string Message = null!);

        public record LoginResponse(bool Flag = false, string Message = null!, string JWTToken = null!);

        public record GetProductsResponse(bool Flag= false, List<Product> products= null! );

        public record RegisterProductResponse(Product Product, bool Flag,  string Message = null!);

        public record DeleteProductResponse(bool Flag, string Message = null!);
    }
}
