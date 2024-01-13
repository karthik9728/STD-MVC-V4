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
    }
}
