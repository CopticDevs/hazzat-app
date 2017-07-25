using Hazzat.Models;
using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazzat.ViewModels
{
    /// <summary>
    /// View model for SectionMenu
    /// </summary>
    public class SectionMenuViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets/sets navigation type
        /// </summary>
        public NavigationType NavigationType { get; set; }

        int itemId = -1;
        /// <summary>
        /// Gets/sets the item id
        /// </summary>
        public int ItemId
        {
            get { return itemId; }
            set { SetProperty(ref itemId, value); }
        }

        public StructureInfo[] HymnsBySeason { get; private set; }

        /// <summary>
        /// Service List
        /// </summary>
        public ObservableCollection<ServiceDetails> ServiceList { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemId">Item id.</param>
        /// <param name="title">Item title.</param>
        /// <param name="navigationType">Navigation type.</param>
        public SectionMenuViewModel(int itemId, string title, NavigationType navigationType)
        {
            ItemId = itemId;
            Title = title;
            NavigationType = navigationType;
            ServiceList = new ObservableCollection<ServiceDetails>();

            LoadContentBasedOnNavigationType();
        }

        private void LoadContentBasedOnNavigationType()
        {
            switch (NavigationType)
            {
                case NavigationType.Season:
                    GetSeasonServices(ItemId);
                    break;
                case NavigationType.Type:
                    App.NameViewModel.GetSeasonsByTypeId(ItemId);
                    itemId = ItemId;
                    break;
                case NavigationType.Tune:
                    App.NameViewModel.GetSeasonsByTuneId(ItemId);
                    itemId = ItemId;
                    break;
                default:
                    break;
            }
        }

        public void GetSeasonServices(int seasonId)
        {
            IsBusy = true;
            HazzatController.GetSeasonServices(seasonId, OnGetSeasonServicesCompleted);
        }

        public void OnGetSeasonServicesCompleted(object sender, GetSeasonServicesCompletedEventArgs e)
        {
            HymnsBySeason = e.Result;

            if (HymnsBySeason != null)
            {
                LoadServiceHymns(HymnsBySeason);
            }

            IsBusy = false;
        }

        private void LoadServiceHymns(StructureInfo[] hymnsBySeason)
        {
            foreach (var structInfo in hymnsBySeason.OrderBy(s => s.Service_Order))
            {
                ServiceList.Add(new ServiceDetails()
                {
                    ServiceName = structInfo.Service_Name,
                    StructureId = structInfo.ItemId
                });

                HazzatController.GetSeasonServiceHymns(structInfo.ItemId, GetCompletedHymnsBySeason);
            }
        }
        
        private void GetCompletedHymnsBySeason(object sender, GetSeasonServiceHymnsCompletedEventArgs e)
        {
            var fetchedHymns = e.Result;

            if (fetchedHymns.Length != 0)
            {
                // Adding a lock on serviceList since multiple services could be modifying the collection
                lock (ServiceList)
                {
                    var serviceInfo = ServiceList.First(s => s.StructureId == fetchedHymns[0].Structure_ID);

                    foreach (var hymnInfo in fetchedHymns)
                    {
                        serviceInfo.Add(new ServiceHymnMenuItem()
                        {
                            ItemId = hymnInfo.ItemId,
                            Title = hymnInfo.Title,
                            Structure_Name = hymnInfo.Structure_Name
                        });
                    }
                }
            }
        }
    }
}
