using Hazzat.Helpers;
using Hazzat.Models;
using Hazzat.Service;
using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hazzat.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        /// <summary>
        /// List of Seasons
        /// </summary>
        public ObservableRangeCollection<SeasonMenuItem> Seasons { get; set; }

        public MenuViewModel() : base()
        {
            Title = "hazzat.com";
            Seasons = new ObservableRangeCollection<SeasonMenuItem>();
        }

        public void LoadSeasons()
        {
            HazzatController.GetSeasons(true, OnGetSeasonsCompleted);
        }

        private void OnGetSeasonsCompleted(object sender, GetSeasonsCompletedEventArgs e)
        {
            List<SeasonMenuItem> seasonsList = new List<SeasonMenuItem>();

            foreach (var item in e.Result)
            {
                if (item?.ServiceHymnsCount != null)
                {
                    var currentSeason = new SeasonMenuItem()
                    {
                        Id = item.ItemId,
                        Name = item.Name,
                        ServiceHymnsCount = item.ServiceHymnsCount
                    };
                    seasonsList.Add(currentSeason);
                }
            }

            Seasons.ReplaceRange(seasonsList);
        }
    }
}
