﻿using BookStore.Entites;
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


        public int DMLTransactions(string query)
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



        public List<Cart> GetListOfCart(string query)
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


        public int InsertMutipleRecord(Book book, List<BookCategory> categories)
        {
            throw new NotImplementedException();
        }
    }
}
