using BlazorApp.DTOs;
using BlazorApp.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BlazorApp.Responses.CustomResponses;

namespace BlazorApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct productRepo;
        public ProductController (IProduct productRepo)
        {
            this.productRepo = productRepo;
        }

        [HttpGet]
       
        public async Task<ActionResult<GetProductsResponse>> GetProductsAsync()
        {
            var result = await productRepo.GetProductsAsync();
            return Ok(result);
        }

        [HttpPost]

        public async Task<ActionResult<RegisterProductResponse>> RegisterProductAsync(ProductDTO model)
        {
            var result = await productRepo.RegisterProductAsync(model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteProductResponse>> DeleteProductAsync(int id)
        {
            var result = await productRepo.DeleteProductAsync(id);
            return Ok(result);
        }



    }
}
