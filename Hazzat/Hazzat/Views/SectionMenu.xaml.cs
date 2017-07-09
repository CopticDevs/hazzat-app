using hazzat.com;
using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
using Hazzat.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hazzat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SectionMenu : ContentPage
    {
        private static ObservableCollection<ServiceDetails> serviceList;
        private static NavigationType navigationType;
        private static int itemId;
        private static string itemName;

        public SectionMenu(string ItemName, int ItemId, NavigationType navType)
        {
            InitializeComponent();

            switch(Device.RuntimePlatform)
            {
                case Device.Windows:
                    TBI.Order = ToolbarItemOrder.Secondary;
                    TBI.Text = "settings";
                    break;
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                overlay.IsVisible = true;
            });
            Title = ItemName;

            serviceList = new ObservableCollection<ServiceDetails>();
            itemId = 0;
            itemName = ItemName;
            navigationType = navType;

            SubscribeMessages();

            switch (navType)
            {
                case NavigationType.Season:
                    App.NameViewModel.GetSeasonServices(ItemId);
                    break;
                case NavigationType.Type:
                    App.NameViewModel.GetSeasonsByTypeId(ItemId);
                    itemId = ItemId;
                    break;
                case NavigationType.Tune:
                    App.NameViewModel.ByTuneGetSeasons(ItemId);
                    itemId = ItemId;
                    break;
                default:
                    break;
            }
        }

        public async void SectionMenuInit(string ItemName, int ItemId, NavigationType navType)
        {
            SectionMenu newMenu = new SectionMenu(ItemName, ItemId, navType);
            await Navigation.PopAsync();
            await Navigation.PushAsync(newMenu, true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<MainViewModel>(this, "DoneSeason");
        }

        public void SubscribeMessages()
        {
            MessagingCenter.Subscribe<MainViewModel>(this, "DoneSeason", (sender) =>
            {
                if (App.NameViewModel?.HymnsBySeason != null)
                {
                    LoadServiceHymns(App.NameViewModel.HymnsBySeason);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        StructList.ItemsSource = serviceList;
                    });
                }
            });

            MessagingCenter.Subscribe<MainViewModel>(this, "DoneWithSeasonsListByType", (sender) =>
            {
                if (App.NameViewModel?.TypeSeasons != null)
                {
                    LoadServiceHymnsByType(App.NameViewModel.TypeSeasons);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        StructList.ItemsSource = serviceList;
                    });
                }
            });

            MessagingCenter.Subscribe<MainViewModel>(this, "DoneWithSeasonsListByTune", (sender) =>
            {
                if (App.NameViewModel?.TuneSeasons != null)
                {
                    LoadServiceHymnsByTune(App.NameViewModel.TuneSeasons);
    
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        StructList.ItemsSource = serviceList;
                    });
                }
            });
        }

        private void LoadServiceHymns(StructureInfo[] hymnsBySeason)
        {
            foreach (var structInfo in hymnsBySeason.OrderBy(s => s.Service_Order))
            {
                serviceList.Add(new ServiceDetails()
                {
                    ServiceName = structInfo.Service_Name,
                    StructureId = structInfo.ItemId
                });

                App.NameViewModel.GetSeasonServiceHymns(structInfo.ItemId, GetCompletedHymnsBySeason);
            }
        }

        private void LoadServiceHymnsByType(SeasonInfo[] filteredSeasons)
        {
            //BUGBUG: For some reason this method is being called multiple times.  As a temporary
            //hack clear the serviceList first to avoid redundancy
            serviceList.Clear();

            foreach (var seasonInfo in filteredSeasons.OrderBy(s => s.Season_Order))
            {
                // TODO: change the member names to be more generic
                var serviceInfo = new ServiceDetails()
                {
                    ServiceName = seasonInfo.Name,
                    StructureId = seasonInfo.ItemId
                };

                serviceList.Add(serviceInfo);

                App.NameViewModel.GetServiceHymnListBySeasonIdAndTypeId(
                    seasonInfo.ItemId,
                    itemId,
                    (sender, e) => GetCompletedHymnsBySeasonAndTypeOrTune(e.Result, serviceInfo));
            }
        }

        private void LoadServiceHymnsByTune(SeasonInfo[] hymnsBySeason)
        {
            //BUGBUG: For some reason this method is being called multiple times.  As a temporary
            //hack clear the serviceList first to avoid redundancy
            serviceList.Clear();

            foreach (var seasonInfo in hymnsBySeason.OrderBy(s => s.Season_Order))
            {
                // TODO: change the member names to be more generic
                var serviceInfo = new ServiceDetails()
                {
                    ServiceName = seasonInfo.Name,
                    StructureId = seasonInfo.ItemId
                };

                serviceList.Add(serviceInfo);

                App.NameViewModel.GetServiceHymnListBySeasonIdAndTuneId(
                    seasonInfo.ItemId,
                    itemId,
                    (sender, e) => GetCompletedHymnsBySeasonAndTypeOrTune(e.Result, serviceInfo));
            }
        }

        private void GetCompletedHymnsBySeason(object sender, GetSeasonServiceHymnsCompletedEventArgs e)
        {
            var fetchedHymns = e.Result;

            if (fetchedHymns.Length != 0)
            {
                var serviceInfo = serviceList.First(s => s.StructureId == fetchedHymns[0].Structure_ID);

                // Adding a lock on serviceList since multiple services could be modifying the collection
                lock (serviceList)
                {
                    foreach (var hymnInfo in fetchedHymns)
                    {
                        serviceInfo.Add(hymnInfo);
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        overlay.IsVisible = false;
                    });
                }
            }
        }

        private void GetCompletedHymnsBySeasonAndTypeOrTune(ServiceHymnInfo[] fetchedHymns, ServiceDetails serviceInfo)
        {
            if (fetchedHymns.Length != 0)
            {
                lock (serviceList)
                {
                    foreach (var hymnInfo in fetchedHymns)
                    {
                        serviceInfo.Add(hymnInfo);
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        overlay.IsVisible = false;
                    });
                }
            }
        }

        protected async void ServiceHymnTapped(object sender, ItemTappedEventArgs e)
        {
            ServiceHymnInfo item = (ServiceHymnInfo)e.Item;

            string breadcrumbName = navigationType == NavigationType.Season ? item.Structure_Name : itemName;
            HymnPage HymnPage = new HymnPage(breadcrumbName, item.Title, item.ItemId);

            await Navigation.PushAsync(HymnPage, true);

            HymnPage.SubscribeMessage();

            App.NameViewModel.GetHymnContent(item.ItemId);
        }

		protected async Task OnToolbarItemClicked(object sender, EventArgs args)
		{
			var result = await DisplayAlert("Notice", "This is a beta version for a proof of concept.", "Okay", "Link");

			if (!result)
			{
				Device.OpenUri(new Uri("http://www.hazzat.com/"));
			}
		}
    }
}
