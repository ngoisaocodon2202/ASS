using ASS.Models;

namespace ASS.ViewModels
{
    public class CategoryViewModel
    {
        public List<Food>? Foods { get; set; }
        public string? CategoryName { get; set; }
        public int CategoryId { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
