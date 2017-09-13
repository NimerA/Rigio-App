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
        Label _hintLabel;
        readonly List<Button> _loginButtons = new List<Button>();
        bool _isAuthenticated;

        public LoginPage()
        {
            InitUi();
        }

        private void InitUi()
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
                Children = {_hintLabel}
            };

            var providers = new[] {"Facebook"};
            foreach (var provider in providers)
            {
                InitLoginButton(provider, stackLayout);
            }

            Content = stackLayout;
        }

        private void InitLoginButton(string provider, StackLayout stackLayout)
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

        async void LoginButtonOnClicked(object sender, EventArgs e)
        {
            if (_isAuthenticated)
            {
                RedirectToMainPage(sender);
            }
            else
            {
                await Login(sender);
            }
        }

        private void RedirectToMainPage(object sender)
        {
            _hintLabel.Text = "Unauthenticated";

            var senderBtn = sender as Button;
            if (senderBtn == null) return;

            //Logout(senderBtn.AutomationId);

            _isAuthenticated = false;
            foreach (var btn in _loginButtons)
            {
                btn.IsEnabled = true;
                btn.Text = $"Login {btn.AutomationId}";
            }

            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        private async Task Login(object sender)
        {
            var senderBtn = sender as Button;
            if (senderBtn == null) return;

            _hintLabel.Text = "Login. Please wait";
            var loginResult = await LoginToProvider(senderBtn.AutomationId);
        
            foreach (var btn in _loginButtons.Where(b => b != senderBtn))
                btn.IsEnabled = false;

            await ProccessLoginResult(loginResult, senderBtn);
        }

        private async Task ProccessLoginResult(LoginResult loginResult, Button senderBtn)
        {
            switch (loginResult.LoginState)
            {
                case LoginState.Canceled:
                    _hintLabel.Text = "Canceled";
                    foreach (var btn in _loginButtons.Where(b => b != senderBtn))
                        btn.IsEnabled = true;
                    break;
                case LoginState.Success:
                    var account = await App.AccountManager.GetAccountAsync(loginResult.Token);

                    ValidateAccount(account, senderBtn);
                    break;
                default:
                    _hintLabel.Text = "Failed: " + loginResult.ErrorString;
                    foreach (var btn in _loginButtons.Where(b => b != senderBtn))
                        btn.IsEnabled = true;
                    break;
            }
        }

        private void ValidateAccount(Account account, Button senderBtn)
        {
            if (account != null)
            {
                App.Account.Access_Token = account.Access_Token;
                App.Account.UserId = account.UserId;
                _isAuthenticated = true;
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                _hintLabel.Text = "Failed try again";
                foreach (var btn in _loginButtons.Where(b => b != senderBtn))
                    btn.IsEnabled = true;
            }
        }
        
        Task<LoginResult> LoginToProvider(string providerName)
        {

            // get rid of switch
            switch (providerName.ToLower())
            {
                case "facebook":
                    return DependencyService.Get<IFacebookService>().Login();
                default:
                    return DependencyService.Get<IFacebookService>().Login();
            }
        }
    }
}