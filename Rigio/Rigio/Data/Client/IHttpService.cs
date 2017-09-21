using System.Threading.Tasks;
using Rigio.Models;

namespace Rigio.Data
{
	public interface IHttpService
	{
		string BaseAddress { get; set; }

        void AddRequestHeader(string key, string data);
        Task<Response> Get(string url);
		Task<Response> Post(string url, string content);
		Task<Response> Put(string url, string content);
		Task<Response> Delete(string url);
	}
}
