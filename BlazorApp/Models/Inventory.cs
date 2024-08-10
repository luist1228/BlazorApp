using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BlazorApp.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; } = 0;

        [Required]
        public Status Status { get; set; }

        [Required]
        public IO IO { get; set; }

        public DateTime Created { get; set; }
        
        public DateTime Updated { get; set; }

        public Inventory()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

    }

    public enum IO
    {
        In,
        Out,
    }

    public enum Status
    {
        Good,
        Broken,
        Damaged,
    }
}
