using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Web.ViewModels
{
    public class CreateBookViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ISBNNumber { get; set; }
        public decimal Price { get; set; }
        public int BookStoreId { get; set; }
        public int BookAuthorId { get; set; }
        public IFormFile PictureUri { get; set; }
        public List<SelectListItem> BookCategories { get; set; } = new List<SelectListItem>();
    }
}
