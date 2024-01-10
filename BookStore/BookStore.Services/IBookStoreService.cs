using BookStore.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBookStoreService
    {
        public List<Book> GetListOfBooks();

        public Book GetBookById(int bookId);

        public List<Store> GetListOfStore();

        public Store GetStoryById(int storeId);

        public List<Category> GetCategoriesByBookId(int bookId);

        public OrderDetail GetOrderDetails(string query);

        public Cart GetCartDetails(string query);

        public List<Cart> GetListOfCart(string query);

        public List<OrderDetail> GetListOrders(string query);

        public int InsertMutipleRecord(Book book,List<BookCategory> categories);



        public int DMLTransactions(string query);
    }
}
