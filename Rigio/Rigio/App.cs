using System;
using Rigio.Data;
using Rigio.Views;
using Xamarin.Forms;
using Rigio.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Rigio
{
    public class App : Application
    {

        public static ServicesManager AccountManager { get; private set; }
        public static Account Account { get; private set; }

        public App()
		{
			var baseUrl = "http://10.102.1.157:3000/";
			var apiUrl = baseUrl + "api/";
			var client = new HttpClient
			{
				BaseAddress = new Uri(apiUrl),
				MaxResponseContentBufferSize = 256000

			};
			var jsonSettings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};
            var accountInfo = new AccountInfo();
            AccountManager = new ServicesManager(
                new AccountService(baseUrl, client, jsonSettings), 
                new MatchService(client, jsonSettings, accountInfo),
                new InvitationService(client, jsonSettings, accountInfo)
            );
            Account = new Account();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
