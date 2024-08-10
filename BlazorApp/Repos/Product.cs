using Models = BlazorApp.Models;
using BlazorApp.DTOs;
using Microsoft.EntityFrameworkCore;
using static BlazorApp.Responses.CustomResponses;
using BlazorApp.Data;

namespace BlazorApp.Repos
{
    public class Product : IProduct
    {

        private readonly AppDbContext appDbContext;
        private readonly IConfiguration config;


        public Product(AppDbContext appDbContext, IConfiguration config)
        {
            this.appDbContext = appDbContext;
            this.config = config;
        }

        public async Task<DeleteProductResponse> DeleteProductAsync(int ID)
        {
            Models.Product product = new Models.Product() { Id = ID};
            appDbContext.Products.Attach(product);
            appDbContext.Products.Remove(product);
            await appDbContext.SaveChangesAsync();

            return new DeleteProductResponse(true, "Product Deleted");
        }

        public async Task<GetProductsResponse> GetProductsAsync()
        {
            var products = await appDbContext.Products.ToListAsync();

            return new GetProductsResponse(true, products);
        }

        public async Task<RegisterProductResponse> RegisterProductAsync(ProductDTO model)
        {

            var product = new Models.Product()
            {
                Name = model.Name,
                ManufactureMethod = model.ManufactureMethod,
                Stock = 0,
            };

            appDbContext.Products.Add(product);

            await appDbContext.SaveChangesAsync();

            return new RegisterProductResponse(product, true, "Product added");
        }
    }
}
