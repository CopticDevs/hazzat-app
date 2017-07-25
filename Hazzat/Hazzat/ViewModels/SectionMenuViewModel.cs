using Hazzat.Models;
using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
using Hazzat.Types;
using System.Collections.ObjectModel;
using System.Linq;

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
        /// Hymn group list
        /// </summary>
        public ObservableCollection<HymnGroup> HymnGroups { get; set; }

        /// <summary>
        /// Instantiates a new SectionMenuViewModel with the given parameters.
        /// </summary>
        /// <param name="itemId">Item id.</param>
        /// <param name="title">Item title.</param>
        /// <param name="navigationType">Navigation type.</param>
        public SectionMenuViewModel(int itemId, string title, NavigationType navigationType)
        {
            ItemId = itemId;
            Title = title;
            NavigationType = navigationType;
            HymnGroups = new ObservableCollection<HymnGroup>();

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
                var groupDetails = new HymnGroup(structInfo.Service_Name);

                HymnGroups.Add(groupDetails);

                HazzatController.GetSeasonServiceHymns(
                    structInfo.ItemId,
                    (sender, e) => GetCompletedHymnsBySeason(e.Result, groupDetails));
            }
        }

        private void GetCompletedHymnsBySeason(ServiceHymnInfo[] fetchedHymns, HymnGroup groupDetails)
        {
            if (fetchedHymns.Length != 0)
            {
                // Adding a lock on serviceList since multiple services could be modifying the collection
                lock (HymnGroups)
                {
                    foreach (var hymnInfo in fetchedHymns)
                    {
                        groupDetails.Add(new ServiceHymnMenuItem()
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
                var groupDetails = new HymnGroup(seasonInfo.Name);

                HymnGroups.Add(groupDetails);

                HazzatController.GetServiceHymnListBySeasonIdAndTypeId(
                    seasonInfo.ItemId,
                    itemId,
                    (sender, e) => GetCompletedHymnsBySeasonAndTypeOrTune(e.Result, groupDetails));
            }
        }

        private void GetCompletedHymnsBySeasonAndTypeOrTune(ServiceHymnInfo[] fetchedHymns, HymnGroup groupDetails)
        {
            if (fetchedHymns != null)
            {
                lock (HymnGroups)
                {
                    foreach (var hymnInfo in fetchedHymns)
                    {
                        groupDetails.Add(new ServiceHymnMenuItem()
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
                var groupDetails = new HymnGroup(seasonInfo.Name);

                HymnGroups.Add(groupDetails);

                HazzatController.GetServiceHymnListBySeasonIdAndTuneId(seasonInfo.ItemId,
                    itemId,
                    (sender, e) => GetCompletedHymnsBySeasonAndTypeOrTune(e.Result, groupDetails));
            }
        }
    }
}
