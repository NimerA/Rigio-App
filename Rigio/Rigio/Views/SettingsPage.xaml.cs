using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rigio.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            App.AccountManager.Logout();

            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}