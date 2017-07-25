using Hazzat.Helpers;
using Hazzat.Models;
using Hazzat.Service;
using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Hazzat.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        /// <summary>
        /// List of Seasons
        /// </summary>
        public ObservableRangeCollection<MainMenuItem> Seasons { get; set; }

        /// <summary>
        /// List of Types
        /// </summary>
        public ObservableRangeCollection<MainMenuItem> Types { get; set; }

        /// <summary>
        /// List of Tunes
        /// </summary>
        public ObservableRangeCollection<MainMenuItem> Tunes { get; set; }

        public MenuViewModel() : base()
        {
            Title = "hazzat.com";
            Seasons = new ObservableRangeCollection<MainMenuItem>();
            Types = new ObservableRangeCollection<MainMenuItem>();
            Tunes = new ObservableRangeCollection<MainMenuItem>();
        }

        public void LoadSeasons()
        {
            MessagingCenter.Send(this, "Loading");
            HazzatController.GetSeasons(true, OnGetSeasonsCompleted);
        }

        public void LoadTypes()
        {
            MessagingCenter.Send(this, "Loading");
            HazzatController.GetTypeList(OnGetTypeListCompleted);
        }

        public void LoadTunes()
        {
            MessagingCenter.Send(this, "Loading");
            HazzatController.GetTuneList(OnGetTuneListCompleted);
        }

        private void OnGetSeasonsCompleted(object sender, GetSeasonsCompletedEventArgs e)
        {
            List<MainMenuItem> seasonsList = new List<MainMenuItem>();

            foreach (var item in e.Result)
            {
                if (item?.ServiceHymnsCount != null)
                {
                    seasonsList.Add(new MainMenuItem()
                    {
                        ItemId = item.ItemId,
                        Name = item.Name,
                        ServiceHymnsCount = item.ServiceHymnsCount
                    });
                }
            }

            Seasons.ReplaceRange(seasonsList);
        }

        private void OnGetTypeListCompleted(object sender, GetTypeListCompletedEventArgs e)
        {
            List<MainMenuItem> typesList = new List<MainMenuItem>();

            foreach (var item in e.Result)
            {
                if (item?.ServiceHymnsCount != null)
                {
                    typesList.Add(new MainMenuItem()
                    {
                        ItemId = item.ItemId,
                        Name = item.Name,
                        ServiceHymnsCount = item.ServiceHymnsCount
                    });
                }
            }

            Types.ReplaceRange(typesList);
        }

        private void OnGetTuneListCompleted(object sender, GetTuneListCompletedEventArgs e)
        {
            List<MainMenuItem> tunesList = new List<MainMenuItem>();

            foreach (var item in e.Result)
            {
                if (item?.ServiceHymnsCount != null)
                {
                    tunesList.Add(new MainMenuItem()
                    {
                        ItemId = item.ItemId,
                        Name = item.Name,
                        ServiceHymnsCount = item.ServiceHymnsCount
                    });
                }
            }

            Tunes.ReplaceRange(tunesList);
        }
    }
}
