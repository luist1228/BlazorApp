using BlazorApp.Data;
using BlazorApp.DTOs;
using BlazorApp.Responses;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static BlazorApp.Responses.InventoryReponses;

namespace BlazorApp.Repos
{
    public class Inventory : IInventory
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration config;

        public Inventory(AppDbContext appDbContext, IConfiguration config)
        {
            this.appDbContext = appDbContext;
            this.config = config;
        }
        public async Task<GetInventoryResponse> GetInventoryAsync(int productId)
        {
            var product = await appDbContext.Products
                .Where(e => e.Id == productId)
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync();


            if (product == null) {
                return new GetInventoryResponse(null!, false);
            }
            
           
            return new GetInventoryResponse( product, true);
        }

        public async Task<RegisterInventoryResponse> RegisterInventoryAsync(InventoryDTO model)
        {
            Debug.WriteLine("REGISTER");
            Debug.WriteLine(model.ToJson().ToString());
            // Get the Product for the inventory item
            var product = await appDbContext.Products.Include("Inventory")
                .FirstOrDefaultAsync(e => e.Id == model.ProductId);

            if (product == null)
            {
                return new RegisterInventoryResponse(null!, false);
            }

            if (model.IO == Models.IO.Out && product.Stock < model.Quantity) {
                return new RegisterInventoryResponse(null!, false, "Not enough stock");
            }

            // Update Product Stock base on inventory IO 
            product.Stock = model.IO == Models.IO.In
                ? product.Stock + model.Quantity
                : product.Stock - model.Quantity;

            // Create new inventory item
            var inventory = new Models.Inventory()
            {
                Product = new Models.Product() { Id = model.ProductId },
                Quantity = model.Quantity,
                Status = model.Status,
                IO = model.IO,
            };

            Debug.WriteLine(inventory.ToString());
            product.Inventory.Add(inventory);

            appDbContext.Products.Update(product);

            await appDbContext.SaveChangesAsync();

            return new RegisterInventoryResponse(inventory, true, "Inventory Added");
        }
    }
}
