using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entites
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ISBNNumber { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string BookImage { get; set; } = string.Empty;

        public int BookAuthorId { get; set; }

        public int BookStoreId { get; set; }
    }
}
