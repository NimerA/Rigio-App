using Autofac;
using Rigio.Views;
using Xamarin.Forms;
using Rigio.Models;

namespace Rigio
{
    public class App : Application
    {
        public static Account Account { get; private set; }

        public static IContainer Container { get; set; }

        public App()
		{
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
