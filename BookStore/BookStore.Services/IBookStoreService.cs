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
        //Book
        public List<Book> GetListOfBooks();

        public Book GetBookById(int bookId);

        //BookStore
        public List<Store> GetListOfStore();

        public Store GetStoryById(int storeId);

        //Book Author
        public List<BookAuthor> GetListOfBookAuthor();

        public BookAuthor GetBookAuthorById(int bookAuthorId);

        //Category
        public List<Category> GetListOfCategories();

        public Category GetCategoryById(int categoryId);

        public List<Category> GetCategoriesByBookId(int bookId);

        //Cart
        public List<Cart> GetCartDetailsByUserId(int userId);

        //public List<Cart> GetListOfCart(string query);

        public int SaveBookInCart(Cart cart);

        public Cart GetCartById(int cartId);

        public int UpdateCart(int cartId, decimal totalAmount, int quantity);

        public int DeleteCartById(int id);

        public void DeleteAllCartItemsByUserId(int userId);

        //Order
        public bool AddCartToOrder(List<Cart> userCart);


        public List<OrderDetail> GetListOrders();

        public OrderDetail GetOrderDetails(string query);



        public int InsertMutipleRecord(Book book,List<BookCategory> categories);

        public int DMLTransactions(string query);
    }
}
