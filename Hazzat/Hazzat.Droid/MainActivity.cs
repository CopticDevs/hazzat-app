using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Hazzat.Droid.WorkingWithWebview.Android;
using Hazzat.Abstract;
using Hazzat.Droid;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(BaseUrl_Android))]
[assembly: Dependency(typeof(ColorScheme))]
namespace Hazzat.Droid
{
    [Activity(Label = "Hazzat", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            LoadApplication(new App());
        }

    }
    namespace WorkingWithWebview.Android
    {
        public class BaseUrl_Android : IWebAssets
        {
            public string Get()
            {
                return "file:///android_asset/";
            }
        }
    }

    public class ColorScheme : IColorRender
    {
        public String GetAccent()
        {

            return $"33,150,243";

        }

        public string GetDefault()
        {
            return $"0,0,0";
        }

        public String GetBackground()
        {
            return $"245,245,245";
        }
    }
}

