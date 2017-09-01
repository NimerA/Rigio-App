using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rigio.Data;
using Rigio.Models;
using Rigio.Renderers;
using Xamarin.Forms;

namespace Rigio.Views
{
    public class LoginPage : ContentPage
    {
        readonly Label _hintLabel;
        readonly List<Button> _loginButtons = new List<Button>();
        bool _isAuthenticated;

        public LoginPage()
        {
            Title = "Rigio Login";

            _hintLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Unauthenticated"
            };

            var stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = { _hintLabel }
            };

            var providers = new[] { "Facebook"};
            foreach (var provider in providers)
            {
                var loginButton = new Button
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Text = $"Login {provider}",
                    AutomationId = provider,
                    HeightRequest = 35
                };

                loginButton.Clicked += LoginButtonOnClicked;

                _loginButtons.Add(loginButton);
                stackLayout.Children.Add(loginButton);
            }

            Content = stackLayout;
        }

        async void LoginButtonOnClicked(object sender, EventArgs e)
        {
            if (_isAuthenticated)
            {
                _hintLabel.Text = "Unauthenticated";

                var senderBtn = sender as Button;
                if (senderBtn == null) return;

                Logout(senderBtn.AutomationId);

                _isAuthenticated = false;
                foreach (var btn in _loginButtons)
                {
                    btn.IsEnabled = true;
                    btn.Text = $"Login {btn.AutomationId}";
                }
                
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                var senderBtn = sender as Button;
                if (senderBtn == null) return;
                
                _hintLabel.Text = "Login. Please wait";
                var loginResult = await Login(senderBtn.AutomationId);


                foreach (var btn in _loginButtons.Where(b => b != senderBtn))
                    btn.IsEnabled = false;

                switch (loginResult.LoginState)
                {
                    case LoginState.Canceled:
                        _hintLabel.Text = "Canceled";
                        foreach (var btn in _loginButtons.Where(b => b != senderBtn))
                            btn.IsEnabled = true;
                        break;
                    case LoginState.Success:
                        //_hintLabel.Text = $"Hi {loginResult.FirstName}! Your email is {loginResult.Email}";
                        //senderBtn.Text = $"Logout {senderBtn.AutomationId}";
                        var account = await App.AccountManager.GetAccountAsync(loginResult.Token);

                        if (account != null)
                        {
                            App.Account.Access_Token = account.Access_Token;
                            App.Account.UserId = account.UserId;

                            _isAuthenticated = true;

                            Application.Current.MainPage = new NavigationPage(new MainPage());
                        }
                        else
                        {
                            foreach (var btn in _loginButtons.Where(b => b != senderBtn))
                                btn.IsEnabled = true;
                           // _isAuthenticated = false;
                        }

                        break;
                    default:
                        _hintLabel.Text = "Failed: " + loginResult.ErrorString;
                        foreach(var btn in _loginButtons.Where(b => b != senderBtn))
                            btn.IsEnabled = true;
                        break;
                }
            }
        }

        Task<LoginResult> Login(string providerName)
        {
            switch (providerName.ToLower())
            {
                case "facebook":
                    return DependencyService.Get<IFacebookService>().Login();
                default:
                    return DependencyService.Get<IFacebookService>().Login();
            }
        }

        void Logout(string providerName)
        {
            switch (providerName.ToLower())
            {
                case "facebook":
                    DependencyService.Get<IFacebookService>().Logout();
                    return;
                default:
                    DependencyService.Get<IFacebookService>().Logout();
                    return;
            }
        }
    }
}