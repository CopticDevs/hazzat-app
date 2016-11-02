using hazzat.com;
using Hazzat.HazzatService;
using HazzatService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hazzat.Views
{
    public partial class SectionMenu : ContentPage
    {
        private static ObservableCollection<ServiceDetails> serviceList;

        public SectionMenu(string ItemName, int ItemId, string By)
        {
            InitializeComponent();

            Title = ItemName;

            serviceList = new ObservableCollection<ServiceDetails>();

            SubscribeMessages();

            if (By == "Season")
            {
                App.NameViewModel.createViewModelBySeason(ItemId);
            }

            if (By == "Type")
            {
                App.NameViewModel.ByTypeGetSeasons(ItemId);
            }

            if (By == "Tune")
            {
                App.NameViewModel.ByTuneGetSeasons(ItemId);
            }
        }

        public async void SectionMenuInit(string ItemName, int ItemId, string By)
        {
            SectionMenu newMenu = new SectionMenu(ItemName, ItemId, By);
            await Navigation.PopAsync();
            await Navigation.PushAsync(newMenu, true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<ByNameMainViewModel>(this, "DoneSeason");
        }

        public void SubscribeMessages()
        {
            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneSeason", (sender) =>
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

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithSeasonsListByType", (sender) =>
            {
                if (App.NameViewModel?.HymnsBySeason != null)
                {
                    LoadServiceHymnsByType(App.NameViewModel.HymnsBySeason);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        StructList.ItemsSource = serviceList;
                    });
                }
            });

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithSeasonsListByTune", (sender) =>
            {
                if (App.NameViewModel?.HymnsBySeason != null)
                {
                    LoadServiceHymnsByTune(App.NameViewModel.HymnsBySeason);

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

                App.NameViewModel.FetchServiceHymns(structInfo.ItemId, GetCompletedHymnsBySeason);
            }
        }

        private void LoadServiceHymnsByType(StructureInfo[] hymnsBySeason)
        {
            foreach (var structInfo in hymnsBySeason.OrderBy(s => s.Service_Order))
            {
                serviceList.Add(new ServiceDetails()
                {
                    ServiceName = structInfo.Service_Name,
                    StructureId = structInfo.ItemId
                });

                //GetServiceHymnListBySeasonIdAndTypeId
            }
        }

        private void LoadServiceHymnsByTune(StructureInfo[] hymnsBySeason)
        {
              foreach (var structInfo in hymnsBySeason.OrderBy(s => s.Service_Order))
            {
                serviceList.Add(new ServiceDetails()
                {
                    ServiceName = structInfo.Service_Name,
                    StructureId = structInfo.ItemId
                });

                //GetServiceHymnListBySeasonIdAndTuneId
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
                }
            }
        }


        protected async void ServiceHymnTapped(object sender, ItemTappedEventArgs e)
        {
            ServiceHymnInfo item = (ServiceHymnInfo)e.Item;

            HymnPage HymnPage = new HymnPage(item.Structure_Name, item.Title, item.ItemId);

            await Navigation.PushAsync(HymnPage, true);

            HymnPage.SubscribeMessage();

            App.NameViewModel.CreateHymnTextViewModel(item.ItemId);
        }
    }
}
