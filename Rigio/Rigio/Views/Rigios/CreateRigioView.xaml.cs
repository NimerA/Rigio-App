using System;
using System.Globalization;
using Rigio.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rigio.Views.Rigios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateRigioView : ContentPage
    {
        private Match _rigio;
        private readonly bool _isEditing;
        public CreateRigioView(Match rigio, bool isEditing)
        {
            InitializeComponent();

            _rigio = rigio;
            _isEditing = isEditing;

            NameEntry.Text = _rigio.Name;
            DescriptionEntry.Text = _rigio.Description;
            PlayersEntry.Text = _rigio.MaxPlayers.ToString();
           
            if (!isEditing) return;

            var utcTime = DateTime.Parse(_rigio.Date);
            DateEntry.Date = utcTime.Date;
            TimeEntry.Time = utcTime.TimeOfDay;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            SetToolBarItems();
        }

        private void SetToolBarItems()
        {
            ToolbarItems.Clear();

            var saveToolBarItem = new ToolbarItem
            {
                Text = "Guardar",
                Icon = "save.png"
            };
            saveToolBarItem.Clicked += SaveToolBarItem_Clicked;

            ToolbarItems.Add(saveToolBarItem);
        }

        private async void SaveToolBarItem_Clicked(object sender, EventArgs e)
        {
            var date = DateEntry.Date;
            var combined  = date.Add(TimeEntry.Time);

            _rigio = new Match
            {
                id = _rigio.id,
                Name = NameEntry.Text,
                Description = DescriptionEntry.Text,
                MaxPlayers = Convert.ToInt32(PlayersEntry.Text),
                CreatorId = Convert.ToInt32(App.Account.UserId),
                Date = combined.ToString()
            };

            bool response;
            if (_isEditing)
                response = await App.AccountManager.UpdateMatch(_rigio);
            else
                response = await App.AccountManager.CreateMatch(_rigio);

            if (!response)
            {
                await DisplayAlert("Rigio", "Ha ocurrido un error", "Ok");
                return;
            }
               
            await Navigation.PopAsync();
        }
    }
}