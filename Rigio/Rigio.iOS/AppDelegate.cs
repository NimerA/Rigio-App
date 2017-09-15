using Foundation;
using PCLAppConfig;
using UIKit;

namespace Rigio.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();

            ConfigurationManager.Initialise(PCLAppConfig.FileSystemStream.PortableStream.Current);

            LoadApplication(new App());

            Facebook.CoreKit.Profile.EnableUpdatesOnAccessTokenChange(true);
            Facebook.CoreKit.ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return Facebook.CoreKit.ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation)
                || base.OpenUrl(application, url, sourceApplication, annotation);
        }

        public override void OnActivated(UIApplication application)
        {
            Facebook.CoreKit.AppEvents.ActivateApp();
        }
    }
}
