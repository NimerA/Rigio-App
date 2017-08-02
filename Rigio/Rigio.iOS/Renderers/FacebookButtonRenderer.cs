using System;
using System.Collections.Generic;
using System.Text;
using Facebook.LoginKit;
using Rigio.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Rigio.iOS.Renderers
{
    public class FacebookButtonRenderer : ButtonRenderer
    {
        private LoginButton _loginButton;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            _loginButton = new LoginButton
            {
                LoginBehavior = LoginBehavior.Native,
            };
          
            SetNativeControl(_loginButton);
        }
    }
}
