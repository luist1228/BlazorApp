using BlazorApp.DTOs;
using BlazorApp.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BlazorApp.Responses.InventoryReponses;

namespace BlazorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventory inventoryRepo;

        public InventoryController(IInventory inventoryRepo)
        {
            this.inventoryRepo = inventoryRepo;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetInventoryResponse>> GetInventoryAsync(int id)
        {
            var result = await inventoryRepo.GetInventoryAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RegisterInventoryResponse>> RegisterInventoryAsync(InventoryDTO model)
        {
            var result = await inventoryRepo.RegisterInventoryAsync(model);
            return Ok(result);
        }


    }
}
