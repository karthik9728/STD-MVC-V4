using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entites
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string BookName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

    }
}
