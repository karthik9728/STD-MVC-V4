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
    }
}
