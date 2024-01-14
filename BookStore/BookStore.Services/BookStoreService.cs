using BookStore.Entites;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

                cmd.Parameters.AddWithValue("@id", categoryId);

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

        public int SaveBookInCart(Cart cart)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "Insert into [Cart](BookId,Name,Price,Quantity,TotalAmount,UserId)" +
                    "values(@bookId,@bookName,@price,@Quantity,@totalAmount,@userId)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@bookId", cart.BookId);
                    cmd.Parameters.AddWithValue("@bookName", cart.BookName);
                    cmd.Parameters.AddWithValue("@price", cart.Price);
                    cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
                    cmd.Parameters.AddWithValue("@totalAmount", cart.TotalAmount);
                    cmd.Parameters.AddWithValue("@userId", cart.UserId);

                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    return result;
                }
            }
        }


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


        public Cart GetCartById(int cartId)
        {
            Cart cart = new Cart();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "select * from [Cart] where Id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", cartId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        cart.Id = Convert.ToInt32(reader["Id"]);
                        cart.BookId = Convert.ToInt32(reader["BookId"]);
                        cart.BookName = reader["Name"].ToString();
                        cart.Price = Convert.ToDecimal(reader["Price"]);
                        cart.Quantity = Convert.ToInt32(reader["Quantity"]);
                        cart.TotalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                        cart.UserId = Convert.ToInt32(reader["UserId"]);

                    }

                    conn.Close();
                }
            }

            return cart;
        }

        public int UpdateCart(int cartId, decimal totalAmount, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "Update [Cart] Set Quantity=@quantity,TotalAmount=@totalAmount where Id=@id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@totalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@id", cartId);


                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                    return result;
                }
            }
        }


        public int DeleteCartById(int id)
        {
            using(SqlConnection conn = new SqlConnection(ConnectionString)) 
            {
                conn.Open();

                string sql = "DELETE FROM [Cart] WHERE Id=@id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.AddWithValue("@id", id);

                    int result = cmd.ExecuteNonQuery();

                    conn.Close();

                    return result;
                }
            }
        }

        public void DeleteAllCartItemsByUserId(int userId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "Delete from [Cart] where UserId=@id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.AddWithValue("@id", userId);
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public bool AddCartToOrder(List<Cart> userCart)
        {

            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    DataTable sourceTable = ConvertListToDataTable(userCart);

                    using(SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                    {
                        bulkCopy.DestinationTableName = "[dbo].[Order]";

                        bulkCopy.ColumnMappings.Add("BookId", "BookId");
                        bulkCopy.ColumnMappings.Add("Name", "Name");
                        bulkCopy.ColumnMappings.Add("Price", "Price");
                        bulkCopy.ColumnMappings.Add("Quantity", "Quantity");
                        bulkCopy.ColumnMappings.Add("TotalAmount", "TotalAmount");
                        bulkCopy.ColumnMappings.Add("UserId", "UserId");
                        bulkCopy.ColumnMappings.Add("OrderDate", "OrderDate");

                        bulkCopy.WriteToServer(sourceTable);

                    }

                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public DataTable ConvertListToDataTable(List<Cart> userCarts)
        {
            DataTable table = new DataTable();
            table.Columns.Add("BookId", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("TotalAmount", typeof(decimal));
            table.Columns.Add("UserId", typeof(int));
            table.Columns.Add("OrderDate", typeof(DateTime));

            foreach (var item in userCarts)
            {
                table.Rows.Add(item.BookId, item.BookName, item.Price, item.Quantity, item.TotalAmount, item.UserId, DateTime.Now);
            }

            return table;
        }


        public List<OrderDetail> GetAllOrders()
        {
            List<OrderDetail> orders = new List<OrderDetail>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "select * from [Order] ORDER BY OrderDate desc";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        orders.Add(new OrderDetail
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            BookId = Convert.ToInt32(dataReader["BookId"]),
                            BookName = dataReader["Name"].ToString(),
                            Price = Convert.ToDecimal(dataReader["Price"]),
                            Quantity = Convert.ToInt32(dataReader["Quantity"]),
                            TotalAmount = Convert.ToDecimal(dataReader["TotalAmount"]),
                            UserId = Convert.ToInt32(dataReader["UserId"]),
                            OrderDate = Convert.ToDateTime(dataReader["OrderDate"])




                        });
                    }
                }
                connection.Close();
            }
            return orders;
        }


        public List<OrderDetail> GetAllOrders(int? userId)
        {
            List<OrderDetail> orders = new List<OrderDetail>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "select * from [Order] where UserId=@userId";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@userId", userId);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        orders.Add(new OrderDetail
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            BookId = Convert.ToInt32(dataReader["BookId"]),
                            BookName = dataReader["Name"].ToString(),
                            Price = Convert.ToDecimal(dataReader["Price"]),
                            Quantity = Convert.ToInt32(dataReader["Quantity"]),
                            TotalAmount = Convert.ToDecimal(dataReader["TotalAmount"]),
                            UserId = Convert.ToInt32(dataReader["UserId"]),
                            OrderDate = Convert.ToDateTime(dataReader["OrderDate"])




                        });
                    }
                }
                connection.Close();
            }
            return orders;
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
