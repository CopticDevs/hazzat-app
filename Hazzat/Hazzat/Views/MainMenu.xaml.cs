using Hazzat.Models;
using Hazzat.Types;
using Hazzat.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hazzat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : TabbedPage
    {
        public MainMenu()
        {
            InitializeComponent();

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Icon = "menu-1.png";
                    Seasons.Icon = "worldwide.png";
                    Tunes.Icon = "music-player-1";
                    Types.Icon = "windows-1";
					Seasons.Padding = new Thickness(0, 24.5, 0, 0);
                    Types.Padding = new Thickness(0, 24.5, 0, 0);
                    Tunes.Padding = new Thickness(0, 24.5, 0, 0);
                    break;
            }

            BindingContext = App.MenuViewModel;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<MainViewModel>(this, "Done");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.MenuViewModel.Seasons.Count == 0)
            {
                App.MenuViewModel.LoadSeasons();
            }

            if (App.MenuViewModel.Types.Count == 0)
            {
                App.MenuViewModel.LoadTypes();
            }

            if (App.MenuViewModel.Tunes.Count == 0)
            {
                App.MenuViewModel.LoadTunes();
            }
        }

        protected void SeasonSelected(object sender, ItemTappedEventArgs e)
        {
            MainMenuItem item = (MainMenuItem)e.Item;
            MessagingCenter.Send(this,"MenuItemSelected");
            MasterDetailMenu.Menu.SectionMenuInit(item.Name, new NavigationInfo(NavigationMethod.Season, item.ItemId));
        }

        protected void TypeSelected(object sender, ItemTappedEventArgs e)
        {
            MainMenuItem item = (MainMenuItem)e.Item;
            MessagingCenter.Send(this, "MenuItemSelected");
            MasterDetailMenu.Menu.SectionMenuInit(item.Name, new NavigationInfo(NavigationMethod.Type, item.ItemId));
        }
        protected void TuneSelected(object sender, ItemTappedEventArgs e)
        {
            MainMenuItem item = (MainMenuItem)e.Item;
            MessagingCenter.Send(this, "MenuItemSelected");
            MasterDetailMenu.Menu.SectionMenuInit(item.Name, new NavigationInfo(NavigationMethod.Tune, item.ItemId));
        }
    }
}

