using System;
namespace Rigio.Data
{
    public class AccountInfo: IAccountInfo
    {
        public AccountInfo()
        {
        }

        public string GetUserId()
        {
            return App.Account.UserId;
        }
    }
}
