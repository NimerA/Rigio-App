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
            descriptionData.SetBinding(Label.TextProperty, new Binding("Date"));

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
        }
    }
}