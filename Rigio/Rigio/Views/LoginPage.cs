using System;
using System.Threading.Tasks;
using Rigio.Data;
using Rigio.Models;
using Xamarin.Forms;

namespace Rigio.Views
{
    public class LoginPage : ContentPage
    {
        private Label _hintLabel;
        private Button _loginButton = new Button();
        private bool _isAuthenticated;

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
            
            InitLoginButton("Facebook", stackLayout);

            Content = stackLayout;
        }

        private void InitLoginButton(string provider, StackLayout stackLayout)
        {
            _loginButton = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = $"Login {provider}",
                AutomationId = provider,
                HeightRequest = 35
            };

            _loginButton.Clicked += LoginButtonOnClicked;

            stackLayout.Children.Add(_loginButton);
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
            
            _isAuthenticated = false;
            _loginButton.IsEnabled = true;
            _loginButton.Text = $"Login {_loginButton.AutomationId}";

            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        private async Task Login(object sender)
        {
            var senderBtn = sender as Button;
            if (senderBtn == null) return;

            _hintLabel.Text = "Login. Please wait";
            var loginResult = await DependencyService.Get<IFacebookService>().Login();

            _loginButton.IsEnabled = false;

            await ProccessLoginResult(loginResult);
        }

        private async Task ProccessLoginResult(LoginResult loginResult)
        {
            switch (loginResult.LoginState)
            {
                case LoginState.Canceled:
                    _hintLabel.Text = "Canceled";
                    _loginButton.IsEnabled = true;
                    break;
                case LoginState.Success:
                    var account = await App.AccountManager.GetAccountAsync(loginResult.Token);
                    ValidateAccount(account);
                    break;
                default:
                    _hintLabel.Text = "Failed: " + loginResult.ErrorString;
                    _loginButton.IsEnabled = true;
                    break;
            }
        }

        private void ValidateAccount(Account account)
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
                _loginButton.IsEnabled = true;
            }
        }
    }
}