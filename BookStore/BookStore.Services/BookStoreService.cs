using BookStore.Entites;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        public Book GetBookById(int bookId)
        {
            Book book = new Book();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = $"select * from [Book] where Id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing condition values as parameter
                cmd.Parameters.AddWithValue("@id", bookId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        book.Id = Convert.ToInt32(reader["Id"]);
                        book.Name = reader["Name"].ToString();
                        book.Description = reader["Description"].ToString();
                        book.ISBNNumber = reader["ISBN"].ToString();
                        book.Price = Convert.ToDecimal(reader["Price"]);
                        book.BookImage = reader["PictureUri"].ToString();
                        book.BookAuthorId = Convert.ToInt32(reader["BookAuthorId"]);
                        book.BookStoreId = Convert.ToInt32(reader["BookStoreId"]);
                    }

                    conn.Close();
                }
            }

            return book;
        }



        public List<Store> GetListOfStore()
        {
            List<Store> stores = new List<Store>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "select * from [BookStore]";

                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stores.Add(new Store
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }

                    conn.Close();
                }
            }

            return stores;
        }


        public Store GetStoryById(int storeId)
        {

            Store store = new Store();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "select * from [BookStore] where Id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", storeId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        store.Id = Convert.ToInt32(reader["Id"]);
                        store.Name = reader["Name"].ToString();

                    }

                    conn.Close();
                }
            }

            return store;
        }


        public List<BookAuthor> GetListOfBookAuthor()
        {
            List<BookAuthor> bookAuthors = new List<BookAuthor>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "select * from [BookAuthor]";

                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bookAuthors.Add(new BookAuthor
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }

                    conn.Close();
                }
            }

            return bookAuthors;
        }


        public BookAuthor GetBookAuthorById(int bookAuthorId)
        {

            BookAuthor bookAuthor = new BookAuthor();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "select * from [BookAuthor] where Id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", bookAuthorId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        bookAuthor.Id = Convert.ToInt32(reader["Id"]);
                        bookAuthor.Name = reader["Name"].ToString();

                    }

                    conn.Close();
                }
            }

            return bookAuthor;
        }



        public List<Category> GetListOfCategories()
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "select * from [Category]";

                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }

                    conn.Close();
                }
            }

            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            Category category = new Category();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "select * from [Category] where Id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id",categoryId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        category.Id = Convert.ToInt32(reader["Id"]);
                        category.Name = reader["Name"].ToString();

                    }

                    conn.Close();
                }
            }

            return category;
        }


        public List<Category> GetCategoriesByBookId(int bookId)
        {
            List<Category> categories = new List<Category>();


            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "select DISTINCT c.Id,c.Name from Book b inner join " +
                    "BookCategory bc on b.Id = bc.BookId inner join Category c " +
                    $"on bc.CategoryId = c.Id Where b.Id = @bookId";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@bookId", bookId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }

                    conn.Close();
                }
            }

            return categories;
        }

        public List<Cart> GetCartDetailsByUserId(int userId)
        {
            List<Cart> carts = new List<Cart>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "select * from [Cart] where UserId=@userId";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@userId", userId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        carts.Add(new Cart
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            BookId = Convert.ToInt32(reader["BookId"]),
                            BookName = reader["Name"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                        });
                    }

                    conn.Close();
                }
            }

            return carts;
        }


        //public List<Cart> GetListOfCart(string query)
        //{
        //    throw new NotImplementedException();
        //}


        public List<OrderDetail> GetListOrders()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "select * from [Order]";

                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orderDetails.Add(new OrderDetail
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            BookId = Convert.ToInt32(reader["BookId"]),
                            BookName = reader["Name"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            OrderDate = Convert.ToDateTime(reader["OrderDate"])
                        });
                    }

                    conn.Close();
                }
            }

            return orderDetails;
        }

        public OrderDetail GetOrderDetails(string query)
        {
            throw new NotImplementedException();
        }


        public int DMLTransactions(string query)
        {
            throw new NotImplementedException();
        }


        public int InsertMutipleRecord(Book book, List<BookCategory> categories)
        {
            throw new NotImplementedException();
        }


    }
}
