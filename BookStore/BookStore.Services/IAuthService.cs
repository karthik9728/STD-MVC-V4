using BookStore.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IAuthService
    {
        int AddRole(string roleName);

        List<Role> GetAllRoles();

        Role GetRole(int roleId);

        int AddUser(AuthenticatedUser user);

        AuthenticatedUser CheckUser(string userName,string password);

        bool CheckUserExists(string userName,string password);

        public AuthenticatedUser GetUserByUserId(int userId);

        public int UpdateUserDetails(int id, string address, string contactNumber);
    }
}
