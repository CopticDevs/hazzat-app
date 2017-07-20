using Hazzat.Models;
using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
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

            SubscribeMessages();

            App.NameViewModel.GetTypeList();
            App.NameViewModel.GetTuneList();
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
        }

        private void SubscribeMessages()
        {
            MessagingCenter.Subscribe<MainViewModel>(this, "DoneWithTypeList", (sender) =>
            {
                if (App.NameViewModel?.TypeList != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        TypesMenu.ItemsSource = App.NameViewModel.TypeList;
                    });
                }
            });

            MessagingCenter.Subscribe<MainViewModel>(this, "DoneWithTuneList", (sender) =>
            {
                if (App.NameViewModel?.TuneList != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        TunesMenu.ItemsSource = App.NameViewModel.TuneList;
                    });
                }
            });
        }

        protected void SeasonSelected(object sender, ItemTappedEventArgs e)
        {
            SeasonMenuItem item = (SeasonMenuItem)e.Item;
            MessagingCenter.Send(this,"MenuItemSelected");
            MasterDetailMenu.Menu.SectionMenuInit(item.Name, item.Id, NavigationType.Season);
        }

        protected void TypeSelected(object sender, ItemTappedEventArgs e)
        {
            TypeInfo item = (TypeInfo)e.Item;
            MessagingCenter.Send(this, "MenuItemSelected");
            MasterDetailMenu.Menu.SectionMenuInit(item.Name, item.ItemId, NavigationType.Type);
        }
        protected void TuneSelected(object sender, ItemTappedEventArgs e)
        {
            TuneInfo item = (TuneInfo)e.Item;
            MessagingCenter.Send(this, "MenuItemSelected");
            MasterDetailMenu.Menu.SectionMenuInit(item.Name, item.ItemId, NavigationType.Tune);
        }
    }
}

