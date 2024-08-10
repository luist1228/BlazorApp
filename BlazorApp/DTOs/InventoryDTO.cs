using BlazorApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.DTOs
{
    public class InventoryDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }

        [Required, EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        [Required, EnumDataType(typeof(IO))]
        public IO IO { get; set; }

    }
}
