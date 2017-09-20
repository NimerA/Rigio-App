using System;
using System.Net.Http;
using Autofac;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rigio.Data;
using Rigio.Views;
using Xamarin.Forms;
using Rigio.Models;

namespace Rigio
{
    public class App : Application
    {
      //  public static ServicesManager AccountManager { get; private set; }

        // new 
        public static Account Account { get; private set; }

        public static IContainer Container { get; set; }

        public App()
		{
            //var baseUrl = "http://172.16.11.32:3000/";
            //var apiUrl = baseUrl + "api/";
            //var client = new HttpClient
            //{
            //    BaseAddress = new Uri(apiUrl),
            //    MaxResponseContentBufferSize = 256000

            //};
            //var jsonSettings = new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore,
            //    ContractResolver = new CamelCasePropertyNamesContractResolver()
            //};

            //var accountInfo = new AccountInfo();
            //AccountManager = new ServicesManager(
            //    new AccountService(baseUrl, client, jsonSettings),
            //    new MatchService(client, jsonSettings, accountInfo),
            //    new InvitationService(client, jsonSettings, accountInfo)
            //);
            //Account = new Account();
            //MainPage = new NavigationPage(new LoginPage());


          //  NEW
            var builder = AppSetup.CreateContainer();

            Container = builder;

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
