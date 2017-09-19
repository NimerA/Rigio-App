using System;
using Autofac;
using Rigio.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rigio.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var respnse = await ((AccountService)App.Container.Resolve<IAccountService>()).Logout();

            if (respnse)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                await DisplayAlert("Error", "Ha ocurrido un error","Ok");
            }
            
        }
    }
}