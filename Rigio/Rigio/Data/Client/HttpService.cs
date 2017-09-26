using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Rigio.Models;

namespace Rigio.Data.Client
{
    public class HttpService: IHttpService
    {
        public string BaseAddress { get; set; }
        public HttpClient Client { get; set; }

        public HttpService()
		{
            BaseAddress = AppSetup.BaseUrl;
            var apiUrl = BaseAddress + "api/";
			Client = new HttpClient
			{
				BaseAddress = new Uri(apiUrl)
			};
		}

		public void AddRequestHeader(string key, string data)
		{
            Client.DefaultRequestHeaders.Add(key, data);
		}

		public async Task<Response> Get(string url)
		{
            var response = await Client.GetAsync(url);
            return new Response((int)response.StatusCode, await response.Content.ReadAsStringAsync());
		}

		public async Task<Response> Post(string url, string content)
		{
			var stringContent = content == null ? null : new StringContent(content, Encoding.UTF8, "application/json");
			var response = await Client.PostAsync(url, stringContent);
			return new Response((int)response.StatusCode, await response.Content.ReadAsStringAsync());
		}

		public async Task<Response> Put(string url, string content)
		{
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(url, stringContent);
            return new Response((int)response.StatusCode, await response.Content.ReadAsStringAsync());
		}

        public async Task<Response> Delete(string url)
		{
            var response = await Client.DeleteAsync(url);
            return new Response((int)response.StatusCode, await response.Content.ReadAsStringAsync());
		}
    }
}
