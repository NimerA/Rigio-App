using System.Threading.Tasks;
using Rigio.Models;

namespace Rigio.Data
{
    public interface IFacebookService
    {
        Task<LoginResult> Login();
        void Logout();
    }
}
