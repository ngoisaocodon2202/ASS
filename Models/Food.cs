using System.ComponentModel.DataAnnotations;

namespace ASS.Models
{
    public class Food
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Image { get; set; }

        [Range(1000, 999999)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
