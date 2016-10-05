using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Hazzat.Droid.WorkingWithWebview.Android;
using Hazzat.Abstract;
using static Hazzat.Droid.MainActivity;
using Hazzat.Droid;

[assembly: Dependency(typeof(BaseUrl_Android))]
[assembly: Dependency(typeof(ColorScheme))]
namespace Hazzat.Droid
{
    [Activity(Label = "Hazzat", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
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

            return $"92,92,255";

        }

        public string GetDefault()
        {
            return $"76,76,76";
        }

        public String GetBackground()
        {
            return $"0,0,0";
        }
    }
}

