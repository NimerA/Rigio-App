using System;
using Rigio.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rigio.Views.Rigios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateRigioView : ContentPage
    {
        public CreateRigioView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            SetToolBarItems();
        }

        private void SetToolBarItems()
        {
            ToolbarItems.Clear();

            ToolbarItem saveToolBarItem = new ToolbarItem
            {
                Text = "Guardar",
                Icon = "save.png"
            };
            saveToolBarItem.Clicked += SaveToolBarItem_Clicked;

            ToolbarItems.Add(saveToolBarItem);
        }

        private async void SaveToolBarItem_Clicked(object sender, EventArgs e)
        {
            const string format = "yyyy-MM-dd HH:MM:ss";

            var date = DateEntry.Date;
            var combined  = date.Add(TimeEntry.Time);

            var match = new Match
            {
                Name = NameEntry.Text,
                Description = DescriptionEntry.Text,
                MaxPlayers = Convert.ToInt32(PlayersEntry.Text),
                CreatorId = Convert.ToInt32(App.Account.UserId),
                Date = combined.ToString(format)
            };

            var response = await App.AccountManager.PostMatch(match);

            if (!response)
            {
                await DisplayAlert("Rigio", "Ha ocurrido un error", "Ok");
                return;
            }
               
            await DisplayAlert("Rigio", "Guardado exitosamente", "Ok");
            await Navigation.PopAsync();
        }
    }
}