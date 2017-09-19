using System.Diagnostics;
using Autofac;
using Rigio.Data;
using Rigio.Models;
using Xamarin.Forms;

namespace Rigio.Views.Rigios
{
    public class RigioCell : ViewCell
    {
        public RigioCell()
        {
            var nameLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Nombre:"
            };

            var nameData = new Label
            {
                HorizontalOptions = LayoutOptions.Start
            };
            nameData.SetBinding(Label.TextProperty, new Binding("Name"));

            var descriptionLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Descripción:"
            };

            var descriptionData = new Label
            {
                HorizontalOptions = LayoutOptions.Start
            };
            descriptionData.SetBinding(Label.TextProperty, new Binding("Description"));

            var dateLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Start,
                Text = "Fecha:"
            };

            var dateData = new Label
            {
                HorizontalOptions = LayoutOptions.Start
            };
            dateData.SetBinding(Label.TextProperty, new Binding("Date"));

            var panel1 = new StackLayout
            {
                Children = { nameLabel, nameData },
                Orientation = StackOrientation.Horizontal
            };

            var panel2 = new StackLayout
            {
                Children = { descriptionLabel, descriptionData },
                Orientation = StackOrientation.Horizontal
            };

            //var panel3 = new StackLayout
            //{
            //    Children = { dateLabel, dateData },
            //    Orientation = StackOrientation.Horizontal
            //};
            
            View = new StackLayout
            {
                Children = { panel1, panel2 },
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Fill,
            };

            var editAction = new MenuItem { Text = "Editar" };
            editAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            editAction.Clicked += async (sender, e) => {
                var mi = ((MenuItem)sender).CommandParameter as Match;

                await Application.Current.MainPage.Navigation.PushAsync(new CreateRigioView(mi,true));
            };

            var deleteAction = new MenuItem { Text = "Borrar", IsDestructive = true }; // red background
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

            deleteAction.Clicked += async (sender, e) => {
              
                var response = await Application.Current.MainPage.DisplayAlert("Alert", "Seguro que desea eliminar", "Si", "Cancelar");

                if (!response) return;

                var mi = ((MenuItem)sender).CommandParameter as Match;
                //await ((MatchService)App.Container.Resolve<IAccountService>()).DeleteMatchById((int)mi.id);

                //((ListView) Parent).ItemsSource = await ((MatchService)App.Container.Resolve<IMatchService>()).GetMatches();
                await App.AccountManager.DeleteMatch((int)mi.id);

                ((ListView)Parent).ItemsSource = await App.AccountManager.GetMatches();


            };
            // add to the ViewCell's ContextActions property
            ContextActions.Add(editAction);
            ContextActions.Add(deleteAction);
        }
    }
}