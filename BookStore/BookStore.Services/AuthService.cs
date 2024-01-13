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
    public class AuthService : IAuthService
    {
        private string ConnectionString = string.Empty;

        public AuthService(IConfiguration configuration)
        {
            ConnectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public int AddRole(string roleName)
        {
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "INSERT INTO [Role] (Name) VALUES (@name)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", roleName);
                    int result = cmd.ExecuteNonQuery();
                    
                    conn.Close();

                    return result;
                }
            }
        }


        public List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "select * from [Role]";

                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }

                    conn.Close();
                }
            }

            return roles;
        }


        public int AddUser(AuthenticatedUser user)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = "INSERT INTO [User] (UserName,Email,Password,Name,ContactNumber,Address,RoleId) VALUES (@userName,@email,@password,@name,@contactNumber,@address,@roleId)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@userName", user.UserName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@contactNumber", user.ContactNumber);
                    cmd.Parameters.AddWithValue("@address", user.Address);
                    cmd.Parameters.AddWithValue("@roleId", 2);
                    int result = cmd.ExecuteNonQuery();

                    conn.Close();

                    return result;
                }
            }
        }
    }
}
