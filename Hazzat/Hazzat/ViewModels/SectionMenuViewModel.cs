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
        /// Gets/sets navigation type.
        /// </summary>
        public NavigationType NavigationType { get; set; }

        int itemId = -1;
        /// <summary>
        /// Gets/sets the id of the season/tune/type depending on the Navigation type.
        /// </summary>
        public int ItemId
        {
            get { return itemId; }
            set { SetProperty(ref itemId, value); }
        }

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
                    GetSeasonsByTypeId(ItemId);
                    break;
                case NavigationType.Tune:
                    GetSeasonsByTuneId(ItemId);
                    break;
                default:
                    break;
            }
        }

        private void GetSeasonServices(int seasonId)
        {
            IsBusy = true;
            HazzatController.GetSeasonServices(seasonId, OnGetSeasonServicesCompleted);
        }

        private void OnGetSeasonServicesCompleted(object sender, GetSeasonServicesCompletedEventArgs e)
        {
            var hymnsBySeason = e.Result;

            if (hymnsBySeason != null)
            {
                LoadServiceHymns(hymnsBySeason);
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

        private void GetSeasonsByTypeId(int typeId)
        {
            IsBusy = true;
            HazzatController.GetSeasonsByTypeId(typeId, OnGetSeasonsByTypeIdCompleted);
        }

        private void OnGetSeasonsByTypeIdCompleted(object sender, GetSeasonsByTypeIDCompletedEventArgs e)
        {
            var typeSeasons = e.Result;

            if (typeSeasons != null)
            {
                LoadServiceHymnsByType(typeSeasons);
            }

            IsBusy = false;
        }

        private void LoadServiceHymnsByType(SeasonInfo[] filteredSeasons)
        {
            foreach (var seasonInfo in filteredSeasons.OrderBy(s => s.Season_Order))
            {
                var serviceInfo = new ServiceDetails()
                {
                    ServiceName = seasonInfo.Name,
                    StructureId = seasonInfo.ItemId
                };

                ServiceList.Add(serviceInfo);

                HazzatController.GetServiceHymnListBySeasonIdAndTypeId(
                    seasonInfo.ItemId,
                    itemId,
                    (sender, e) => GetCompletedHymnsBySeasonAndTypeOrTune(e.Result, serviceInfo));
            }
        }

        private void GetCompletedHymnsBySeasonAndTypeOrTune(ServiceHymnInfo[] fetchedHymns, ServiceDetails serviceInfo)
        {
            if (fetchedHymns != null)
            {
                lock (ServiceList)
                {
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

        private void GetSeasonsByTuneId(int tuneId)
        {
            IsBusy = true;
            HazzatController.GetSeasonsByTuneId(tuneId, OnGetSeasonsByTuneIdCompleted);
        }

        private void OnGetSeasonsByTuneIdCompleted(object sender, GetSeasonsByTuneIDCompletedEventArgs e)
        {
            var tuneSeasons = e.Result;

            if (tuneSeasons != null)
            {
                LoadServiceHymnsByTune(tuneSeasons);
            }

            IsBusy = false;
        }

        private void LoadServiceHymnsByTune(SeasonInfo[] hymnsBySeason)
        {
            foreach (var seasonInfo in hymnsBySeason.OrderBy(s => s.Season_Order))
            {
                var serviceInfo = new ServiceDetails()
                {
                    ServiceName = seasonInfo.Name,
                    StructureId = seasonInfo.ItemId
                };

                ServiceList.Add(serviceInfo);

                HazzatController.GetServiceHymnListBySeasonIdAndTuneId(seasonInfo.ItemId,
                    itemId,
                    (sender, e) => GetCompletedHymnsBySeasonAndTypeOrTune(e.Result, serviceInfo));
            }
        }
    }
}
