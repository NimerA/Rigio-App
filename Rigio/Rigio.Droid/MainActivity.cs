using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Xamarin.Facebook;
using Xamarin.Facebook.AppEvents;
using Xamarin.Forms;

namespace Rigio.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            Instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            FacebookSdk.SdkInitialize(ApplicationContext);
            Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            AppEventsLogger.ActivateApp(Application);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AndroidFacebookService.Instance.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }
}

