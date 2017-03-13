using Android.App;
using Android.Content.PM;
using Android.OS;
using Hazzat.Abstract;
using Hazzat.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(BaseUrl_Android))]
[assembly: Xamarin.Forms.Dependency(typeof(ColorScheme))]
namespace Hazzat.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
    public class BaseUrl_Android : IWebAssets
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }

    public class ColorScheme : IColorRender
    {
        public string GetAccent()
        {
            return $"33,150,243";
        }

        public string GetDefault()
        {
            return $"0,0,0";
        }

        public string GetBackground()
        {
            return $"245,245,245";
        }
    }
}

