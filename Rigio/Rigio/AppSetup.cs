using System;
using System.Net.Http;
using Autofac;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rigio.Data;
using Rigio.Data.Client;
using Xamarin.Forms;

namespace Rigio
{
    public class AppSetup
    {
        private static IContainer _instance;
        public static string BaseUrl = "http://192.168.0.9:3000/";

        private AppSetup()
        {
            
        }

		public void Destroyer()
		{
            _instance = CreateContainer();
		}

        public static IContainer Instance
		{
			get
			{
				if (_instance != null) return _instance;
                _instance = CreateContainer();
				return _instance;
			}
		}

        static IContainer CreateContainer()
        {
            var cb = new ContainerBuilder();

            var client2 = new HttpService();
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var accountInfo = new AccountInfo();

            // Services
            cb.Register(ctx => new AccountService(BaseUrl,client2,jsonSettings)).As<IAccountService>();
            cb.Register(ctx => new MatchService(client2, jsonSettings, accountInfo)).As<IMatchService>();
            cb.Register(ctx => new InvitationService(client2, jsonSettings, accountInfo)).As<IInvitationService>();
            cb.RegisterInstance(DependencyService.Get<IFacebookService>()).As<IFacebookService>().SingleInstance();

            return cb.Build();
        }
    }
}
