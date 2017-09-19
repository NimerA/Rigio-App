using Autofac;
using Rigio.Data;
using Xamarin.Forms;

namespace Rigio
{
    public class AppSetup
    {
        public static IContainer CreateContainer()
        {
            ContainerBuilder cb = new ContainerBuilder();

            // Services
            cb.RegisterType<AccountService>().As<IAccountService>().SingleInstance();

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
