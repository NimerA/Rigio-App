using System.Collections.Generic;
using System.Threading.Tasks;
using Rigio.Models;

namespace Rigio.Data
{
    public interface IAccountService
    {
        Task<Account> GetAccountsAsync(string token);
        Task Logout();
    }
}