using System.Collections.Generic;
using System.Threading.Tasks;
using Rigio.Models;

namespace Rigio.Data
{
    public interface IAccountService
    {
        Task<Account> GetAccounts(string facebook_token);
        Task<bool> Logout();

        Task<List<User>> GetUsers();
    }
}