using BlazorApp.Models;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.DTOs
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Methods ManufactureMethod { get; set; }

    }
}
