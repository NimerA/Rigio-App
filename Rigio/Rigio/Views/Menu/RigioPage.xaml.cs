using System;
using Autofac;
using Rigio.Data;
using Rigio.Models;
using Rigio.Renderers;
using Rigio.Views.Rigios;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rigio.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RigioPage : ContentPage
    {
        ListView lvRigios = new ListView();

        public RigioPage()
        {
            InitializeComponent();

            InitializeUi();
        }

        private void InitializeUi()
        {
            FloatingActionButtonView _Fab;
            ScrollView _ScrollView;

            lvRigios.ItemTemplate = new DataTemplate(typeof(RigioCell));

            _ScrollView = new ScrollView
            {
                Content = new StackLayout
                {
                    Spacing = 0,
                    Children =
                    {
                        lvRigios
                    }
                }
            };

            if (Device.RuntimePlatform.Equals("Android"))
            {
                InitializeAndroidAddButton(_ScrollView, out _Fab);
            }
            else
            {
                InitializeIOsAddButton(_ScrollView);
            }
        }

        private void InitializeIOsAddButton(ScrollView scrollView)
        {
            var addButton = new ToolbarItem
            {
                Text = "Add",
                Icon = "add_ios_gray",
                //Order = ToolbarItemOrder.Primary
            };

            addButton.Clicked += async (sender, args) =>
            {
                NavigationPage.SetBackButtonTitle(this, "Back");

                await Navigation.PushAsync(new CreateRigioView(new Match(), false));
            };

            ToolbarItems.Add(addButton);

            Content = scrollView;
        }

        private void InitializeAndroidAddButton(ScrollView scrollView, out FloatingActionButtonView fab)
        {
            fab = new FloatingActionButtonView
            {
                ImageName = "fab_add.png",
                ColorNormal = Color.FromHex("53BA9D"),
                ColorPressed = Color.FromHex("42947D"),
                ColorRipple = Color.FromHex("53BA9D"),

                Clicked = NavigateToCreateMatch
            };

            var absolute = new AbsoluteLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            // Position the pageLayout to fill the entire screen.
            // Manage positioning of child elements on the page by editing the pageLayout.
            AbsoluteLayout.SetLayoutFlags(scrollView, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(scrollView, new Rectangle(0f, 0f, 1f, 1f));
            absolute.Children.Add(scrollView);

            // Overlay the FAB in the bottom-right corner
            AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            absolute.Children.Add(fab);

            Content = absolute;
        }

        Action<object, EventArgs> NavigateToCreateMatch
        {
            get
            {
                return async (o, e) =>
                {
                    NavigationPage.SetBackButtonTitle(this, "Back");

                    await Navigation.PushAsync(new CreateRigioView(new Match(), false));
                };
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //lvRigios.ItemsSource = await ((MatchService)App.Container.Resolve<IMatchService>()).GetMatches();
            lvRigios.ItemsSource = await App.AccountManager.GetMatches();
        }
    }
}