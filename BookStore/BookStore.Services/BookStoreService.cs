using BookStore.Entites;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookStoreService : IBookStoreService
    {
        private string ConnectionString = string.Empty;

        public BookStoreService(IConfiguration configuration)
        {
            ConnectionString = configuration["ConnectionStrings:DefaultConnection"];
        }


        public int DMLTransactions(string query)
        {
            throw new NotImplementedException();
        }

        public Book GetBookById(int bookId)
        {
            throw new NotImplementedException();
        }

        public Cart GetCartDetails(string query)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategoriesByBookId(int bookId)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetListOfBooks()
        {
            List<Book> books = new List<Book>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "select * from [Book]";

                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            ISBNNumber = reader["ISBN"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            BookImage = reader["PictureUri"].ToString(),
                            BookAuthorId = Convert.ToInt32(reader["BookAuthorId"]),
                            BookStoreId = Convert.ToInt32(reader["BookStoreId"])
                        });
                    }

                    conn.Close();
                }
            }

            return books;
        }

        public List<Cart> GetListOfCart(string query)
        {
            throw new NotImplementedException();
        }

        public List<Store> GetListOfStore(string query)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> GetListOrders(string query)
        {
            throw new NotImplementedException();
        }

        public OrderDetail GetOrderDetails(string query)
        {
            throw new NotImplementedException();
        }

        public Store GetStoryById(string query)
        {
            throw new NotImplementedException();
        }

        public int InsertMutipleRecord(Book book, List<BookCategory> categories)
        {
            throw new NotImplementedException();
        }
    }
}
