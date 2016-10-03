using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Hazzat.Abstract;
using static Hazzat.iOS.Application;

[assembly: Dependency(typeof(BaseUrl_iOS))]
namespace Hazzat.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }

        public class BaseUrl_iOS : IWebAssets
        {
            public string Get()
            {
                return NSBundle.MainBundle.BundlePath;
            }
        }
    }
}
