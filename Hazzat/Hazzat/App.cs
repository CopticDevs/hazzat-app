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



        public App()
        {
            Resources = new ResourceDictionary();

            //Define global app styles here
            Resources.Add(new Style(typeof(ContentPage))
            {
                Setters = {
                    new Setter
                    {
                        Property = Page.PaddingProperty,
                        Value = Device.OnPlatform(new Thickness(10), new Thickness(10), new Thickness(10))
                    }
                }
            });
            Resources.Add("accent", Color.Accent);
            Resources.Add("default", Color.Default);
            MainPage = new NavigationPage(new MainMenu()); //Temp fix for insulation. NavigationPage -> TabbedPage 
        }

        protected override void OnStart()
        {
            InitializeDefaultAppSettings();

            App.CurrentLanguage = (App.HymnLanguage)Enum.Parse(typeof(App.HymnLanguage), Application.Current.Properties["AppLanguage"] as string);
        }

        /// <summary>
        /// Sets default app settings
        /// </summary>
        private void InitializeDefaultAppSettings()
        {
            if (!Application.Current.Properties.ContainsKey("AppLanguage"))
            {
                Application.Current.Properties["AppLanguage"] = App.HymnLanguage.English.ToString();
            }
        }

        protected override void OnSleep()
        {
            Application.Current.Properties["AppLanguage"] = CurrentLanguage.ToString();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
