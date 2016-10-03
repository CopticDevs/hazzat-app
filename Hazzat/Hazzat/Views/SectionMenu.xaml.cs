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
        private ObservableCollection<ServiceDetails> serviceList { get; set; }

        public SectionMenu(string Season, int SeasonId)
        {
            Title = Season;

            serviceList = new ObservableCollection<ServiceDetails>();

            InitializeComponent();

            SubscribeMessage();

            App.NameViewModel.createViewModelBySeason(SeasonId);
        }

        private void SubscribeMessage()
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

                foreach (var hymnInfo in fetchedHymns)
                {
                    serviceInfo.Add(hymnInfo);
                }
            }
        }

        protected async void ServiceHymnTapped(object sender, ItemTappedEventArgs e)
        {
            ServiceHymnInfo item = (ServiceHymnInfo)e.Item;

            await Navigation.PushAsync(new HymnPage(item.Structure_Name, item.Title, item.ItemId));
        }
    }
}
