using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rigio.Models;
using Rigio.Data.Wrapper;

namespace Rigio.Data
{
    public class AccountService : IAccountService
    {
        private APIWrapper wrapper;

        public AccountService()
        {
            wrapper = new APIWrapper();
        }

        public async Task<Account> GetAccountsAsync(string token)
        {
            Account account = null;
            try
            {
                account = await wrapper.login(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return account;
        }

        public async Task<bool> Logout()
        {
            try
            {
                return await wrapper.logout();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return false;
        }
    }
}
