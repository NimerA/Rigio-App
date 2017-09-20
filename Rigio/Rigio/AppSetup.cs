using System;
using System.Net.Http;
using Autofac;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rigio.Data;

namespace Rigio
{
    public class AppSetup
    {
        public static IContainer CreateContainer()
        {
            var cb = new ContainerBuilder();

            var baseUrl = "http://172.16.11.32:3000/";
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

            // Services

            cb.Register(ctx => new AccountService(baseUrl,client,jsonSettings)).As<IAccountService>();
            cb.Register(ctx => new MatchService(client, jsonSettings, accountInfo)).As<IMatchService>();
            cb.Register(ctx => new InvitationService(client, jsonSettings, accountInfo)).As<IInvitationService>();
            //cb.RegisterType<AccountService>().As<IAccountService>().SingleInstance();
            //cb.RegisterType<AccountInfo>().As<IAccountInfo>().SingleInstance();
            //cb.RegisterType<InvitationService>().As<IInvitationService>().SingleInstance();
            //cb.RegisterType<MatchService>().As<IMatchService>().SingleInstance();

            //cb.RegisterInstance(DependencyService.Get<IFacebookService>()).As<IFacebookService>().SingleInstance();


            return cb.Build();
        }

        //public IContainer CreateContainer()
        //{
        //    var cb = new ContainerBuilder();
        //    RegisterDepenencies(cb);
        //    return cb.Build();
        //}

        //private void RegisterDepenencies(ContainerBuilder cb)
        //{
        //    // Services
        //    cb.RegisterType<AccountService>().As<IAccountService>().SingleInstance();
        //}
    }
}
