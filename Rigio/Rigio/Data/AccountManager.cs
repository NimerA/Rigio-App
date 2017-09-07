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

        public Task<bool> Logout()
        {
            return _accountService.Logout();
        }

        public Task<List<Match>> GetMatches()
        {
            return _accountService.getMatches();
        }

        public Task<bool> PostMatch(Match match)
        {
            return _accountService.createMatch(match);
        }
    }
}
