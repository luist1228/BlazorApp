using BlazorApp.Models;

namespace BlazorApp.Responses
{
    public class InventoryReponses
    {
        public record RegisterInventoryResponse(Inventory Item ,bool Flag, string Message = null!);

        public record GetInventoryResponse(Product Product , bool Flag = false);

    }
}
