﻿using System;
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
            var respnse = await App.AccountManager.Logout();

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