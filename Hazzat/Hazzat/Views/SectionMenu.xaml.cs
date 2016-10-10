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
        private ObservableCollection<ServiceDetails> serviceList;

        public SectionMenu(string Season, int SeasonId)
        {
            InitializeComponent();

            Title = Season;

            serviceList = new ObservableCollection<ServiceDetails>();
        }

        public void SubscribeMessage()
        {
            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "Done", (sender) =>
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

        private void GetCompletedHymnsBySeason(object sender, GetSeasonServiceHymnsCompletedEventArgs e)
        {
            var fetchedHymns = e.Result;

            if (fetchedHymns.Length != 0)
            {
                var serviceInfo = this.serviceList.First(s => s.StructureId == fetchedHymns[0].Structure_ID);                    

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
