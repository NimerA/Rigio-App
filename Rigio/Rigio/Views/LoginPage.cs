using System;
using System.Threading.Tasks;
using Autofac;
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
                RedirectToMainPage();            
            else
                await Login();
        }

        private void RedirectToMainPage()
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        private async Task Login()
        {
            _hintLabel.Text = "Login. Please wait";
            var loginResult = await AppSetup.Instance.Resolve<IFacebookService>().Login();
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
                    var account = await ((AccountService) AppSetup.Instance.Resolve<IAccountService>()).GetAccounts(loginResult.Token);
                    if (ValidateAccount(account))
                        Application.Current.MainPage = new NavigationPage(new MainPage());
                    break;
                default:
                    _hintLabel.Text = "Failed: " + loginResult.ErrorString;
                    _loginButton.IsEnabled = true;
                    break;
            }
        }

        private bool ValidateAccount(Account account)
        {
            if (account != null)
            {
                App.Account.Loopback_Access_Token = account.Loopback_Access_Token;
                App.Account.UserId = account.UserId;
                _isAuthenticated = true;
                return true;
            }
            _hintLabel.Text = "Failed try again";
            _loginButton.IsEnabled = true;
            return false;
        }
    }
}