using System.Threading.Tasks;

namespace Rigio
{
    public interface IFacebookService
    {
        Task<LoginResult> Login();
        void Logout();
    }
}
