using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BlazorApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Stock { get; set; } = 0;
        [Required]
        public Methods ManufactureMethod { get; set; }
        
        public ICollection<Inventory> Inventory { get; set; }

        public Product()
        {
            Inventory = new Collection<Inventory>();
        }
    }
    public enum Methods
    {
        HandMade,
        HandMachineMade,
    }
}
