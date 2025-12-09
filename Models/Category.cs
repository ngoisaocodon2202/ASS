using System.ComponentModel.DataAnnotations;

namespace ASS.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public List<Food>? Foods { get; set; }
    }
}
