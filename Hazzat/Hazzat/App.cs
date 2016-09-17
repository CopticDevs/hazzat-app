using HazzatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Hazzat
{
    public class App : Application
    {
        public enum HymnLanguage { Arabic, Coptic, English };
        public static HymnLanguage CurrentLanguage { get; set; }
        private static ByNameMainViewModel nameViewModel = null;
        internal static ByNameMainViewModel NameViewModel
        {
            get
            {
                if (nameViewModel == null)
                {
                    nameViewModel = new ByNameMainViewModel();
                }
                return nameViewModel;
            }
        }
        private static BySeasonHymnInfoHymnMainViewModel seasonViewModel = null;
        internal static BySeasonHymnInfoHymnMainViewModel SeasonViewModel
        {
            get
            {
                if (seasonViewModel == null)
                {
                    seasonViewModel = new BySeasonHymnInfoHymnMainViewModel();
                }
                return seasonViewModel;
            }
        }
        public App()
        {
            // The root page of your application
            MainPage = new MainMenu();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
