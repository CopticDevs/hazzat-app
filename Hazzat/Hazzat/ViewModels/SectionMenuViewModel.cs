using Hazzat.Models;
using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
using Hazzat.Types;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Hazzat.ViewModels
{
    /// <summary>
    /// View model for SectionMenu
    /// </summary>
    public class SectionMenuViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets/sets navigation info.
        /// </summary>
        public NavigationInfo NavigationInfo { get; set; }

        /// <summary>
        /// Hymn group list
        /// </summary>
        public ObservableCollection<HymnGroup> HymnGroups { get; set; }

        /// <summary>
        /// Instantiates a new SectionMenuViewModel with the given parameters.
        /// </summary>
        /// <param name="title">Item title.</param>
        /// <param name="navigationType">Navigation type.</param>
        public SectionMenuViewModel(string title, NavigationInfo navigationInfo)
        {
            Title = title;
            NavigationInfo = navigationInfo;
            HymnGroups = new ObservableCollection<HymnGroup>();

            LoadContentBasedOnNavigationType();
        }

        public void LoadContentBasedOnNavigationType()
        {
            switch (NavigationInfo.Method)
            {
                case NavigationMethod.Season:
                    GetSeasonServices(NavigationInfo.ItemId);
                    break;
                case NavigationMethod.Type:
                    GetSeasonsByTypeId(NavigationInfo.ItemId);
                    break;
                case NavigationMethod.Tune:
                    GetSeasonsByTuneId(NavigationInfo.ItemId);
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
                // filter out seasons that have no supported content
                hymnsBySeason = hymnsBySeason.Where(s => s.Has_Text || s.Has_Hazzat || s.Has_VerticalHazzat).ToArray();
                LoadServiceHymns(hymnsBySeason);
            }

            MessagingCenter.Send(this, "GetHymnGroupsCompleted");

            IsBusy = false;
        }

        private void LoadServiceHymns(StructureInfo[] hymnsBySeason)
        {
            foreach (var structInfo in hymnsBySeason.OrderBy(s => s.Service_Order))
            {
                var groupDetails = new HymnGroup(structInfo.Service_Name);

                HazzatController.GetSeasonServiceHymns(
                    structInfo.ItemId,
                    (sender, e) => GetCompletedHymnsBySeason(e.Result, groupDetails));
            }
        }

        private void GetCompletedHymnsBySeason(ServiceHymnInfo[] fetchedHymns, HymnGroup groupDetails)
        {
            if (fetchedHymns.Length != 0)
            {
                // filter out hymns that have no supported content
                fetchedHymns = fetchedHymns.Where(h => h.Has_Text || h.Has_Hazzat || h.Has_VerticalHazzat).ToArray();

                foreach (var hymnInfo in fetchedHymns)
                {
                    groupDetails.Add(new ServiceHymnMenuItem()
                    {
                        ItemId = hymnInfo.ItemId,
                        Title = hymnInfo.Title,
                        Structure_Name = hymnInfo.Structure_Name,
                        HasSupportedContent = true
                    });
                }

                // Adding a lock since multiple services could be modifying the collection concurrently
                lock (HymnGroups)
                {
                    HymnGroups.Add(groupDetails);
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

            MessagingCenter.Send(this, "GetHymnGroupsCompleted");

            IsBusy = false;
        }

        private void LoadServiceHymnsByType(SeasonInfo[] filteredSeasons)
        {
            foreach (var seasonInfo in filteredSeasons.OrderBy(s => s.Season_Order))
            {
                var groupDetails = new HymnGroup(seasonInfo.Name);

                HazzatController.GetServiceHymnListBySeasonIdAndTypeId(
                    seasonInfo.ItemId,
                    NavigationInfo.ItemId,
                    (sender, e) => GetCompletedHymnsBySeasonAndTypeOrTune(e.Result, groupDetails));
            }
        }

        private void GetCompletedHymnsBySeasonAndTypeOrTune(ServiceHymnInfo[] fetchedHymns, HymnGroup groupDetails)
        {
            if (fetchedHymns == null)
            {
                return;
            }

            foreach (var hymnInfo in fetchedHymns)
            {
                groupDetails.Add(new ServiceHymnMenuItem()
                {
                    ItemId = hymnInfo.ItemId,
                    Title = hymnInfo.Title,
                    Structure_Name = hymnInfo.Structure_Name,
                    HasSupportedContent = hymnInfo.Has_Text || hymnInfo.Has_Hazzat || hymnInfo.Has_VerticalHazzat
                });
            }

            // Adding a lock since multiple services could be modifying the collection concurrently
            lock (HymnGroups)
            {
                HymnGroups.Add(groupDetails);
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

            MessagingCenter.Send(this, "GetHymnGroupsCompleted");

            IsBusy = false;
        }

        private void LoadServiceHymnsByTune(SeasonInfo[] filteredSeasons)
        {
            foreach (var seasonInfo in filteredSeasons.OrderBy(s => s.Season_Order))
            {
                var groupDetails = new HymnGroup(seasonInfo.Name);

                HazzatController.GetServiceHymnListBySeasonIdAndTuneId(seasonInfo.ItemId,
                    NavigationInfo.ItemId,
                    (sender, e) => GetCompletedHymnsBySeasonAndTypeOrTune(e.Result, groupDetails));
            }
        }
    }
}
