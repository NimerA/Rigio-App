using System;
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

        private void SaveToolBarItem_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}