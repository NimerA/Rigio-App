using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rigio.Models;

namespace Rigio.Data
{
    public class AccountManager
    {
        private readonly IAccountService _accountService;

        public AccountManager(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Task<Account> GetAccountAsync(string token)
        {
            return _accountService.GetAccountsAsync(token);
        }

        public Task Logout()
        {
            return _accountService.Logout();
        }
    }
}
