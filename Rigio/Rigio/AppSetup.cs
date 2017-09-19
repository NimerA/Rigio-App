using Autofac;
using Rigio.Data;

namespace Rigio
{
    public class AppSetup
    {
        public static IContainer CreateContainer()
        {
            var cb = new ContainerBuilder();

            // Services
            cb.RegisterType<AccountService>().As<IAccountService>().SingleInstance();
            cb.RegisterType<AccountInfo>().As<IAccountInfo>().SingleInstance();
            cb.RegisterType<InvitationService>().As<IInvitationService>().SingleInstance();
            cb.RegisterType<MatchService>().As<IMatchService>().SingleInstance();

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
