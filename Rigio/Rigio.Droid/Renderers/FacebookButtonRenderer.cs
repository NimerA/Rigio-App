using Rigio.Droid.Renderers;
using Rigio.Renderers;
using Xamarin.Facebook.Login.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FacebookButton), typeof(FacebookButtonRenderer))]
namespace Rigio.Droid.Renderers
{
    public class FacebookButtonRenderer : ButtonRenderer
    {
        private LoginButton _loginButton;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            _loginButton = new LoginButton(Forms.Context);
            SetNativeControl(_loginButton);
        }
    }
}