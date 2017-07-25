using Hazzat.Service;
using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
using System;
using Xamarin.Forms;

namespace Hazzat.ViewModels
{
    public class MainViewModel
    {
        /// <summary>
        /// A collection of hazzat.com objects
        /// </summary>
        public ServiceHymnsContentInfo[] TextHymnContentInfo { get; private set; }
        public ServiceHymnsContentInfo[] HazzatHymnContentInfo { get; private set; }
        public ServiceHymnsContentInfo[] VerticalHazzatHymnContent { get; private set; }

        public SeasonInfo[] TypeSeasons { get; private set; }

        public TuneInfo[] TuneList { get; private set; }
        public SeasonInfo[] TuneSeasons { get; private set; }

        private HazzatController hazzatController = new HazzatController();

        public void GetHymnContent(int itemId)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            hazzatController.GetSeasonServiceHymnText(itemId, OnGetSeasonServiceHymnTextCompleted);
            hazzatController.GetSeasonServiceHymnHazzat(itemId, OnGetSeasonServiceHymnHazzatCompleted);
            hazzatController.GetSeasonServiceHymnVerticalHazzat(itemId, OnGetSeasonServiceHymnVerticalHazzatCompleted);
        }

        public void OnGetSeasonServiceHymnTextCompleted(object sender, GetSeasonServiceHymnTextCompletedEventArgs e)
        {
            TextHymnContentInfo = e.Result;
            MessagingCenter.Send(this, "DoneWithHymnText");
            App.IsLoaded = true;
        }

        public void OnGetSeasonServiceHymnHazzatCompleted(object sender, GetSeasonServiceHymnHazzatCompletedEventArgs e)
        {
            HazzatHymnContentInfo = e.Result;
            MessagingCenter.Send(this, "DoneWithHazzat");
            App.IsLoaded = true;
        }

        private void OnGetSeasonServiceHymnVerticalHazzatCompleted(object sender, GetSeasonServiceHymnVerticalHazzatCompletedEventArgs e)
        {
            VerticalHazzatHymnContent = e.Result;
            MessagingCenter.Send(this, "DoneWithVerticalHazzat");
            App.IsLoaded = true;
        }

        #region byType
        public void GetSeasonsByTypeId(int typeId)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            hazzatController.GetSeasonsByTypeId(typeId, OnGetSeasonsByTypeIdCompleted);
        }

        private void OnGetSeasonsByTypeIdCompleted(object sender, GetSeasonsByTypeIDCompletedEventArgs e)
        {
            TypeSeasons = e.Result;
            MessagingCenter.Send(this, "DoneWithSeasonsListByType");
            App.IsLoaded = true;
        }

        public void GetServiceHymnListBySeasonIdAndTypeId(int seasonId, int typeId, Action<object, GetServiceHymnListBySeasonIdAndTypeIdCompletedEventArgs> getCompletedHymnsBySeasonAndType)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            hazzatController.GetServiceHymnListBySeasonIdAndTypeId(seasonId, typeId, getCompletedHymnsBySeasonAndType);
        }

        public void GetServiceHymnListBySeasonIdAndTuneId(int seasonId, int tuneId, Action<object, GetServiceHymnListBySeasonIdAndTuneIdCompletedEventArgs> getCompletedHymnsBySeasonAndTune)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            hazzatController.GetServiceHymnListBySeasonIdAndTuneId(seasonId, tuneId, getCompletedHymnsBySeasonAndTune);
        }

        #endregion

        #region byTune
        public void GetSeasonsByTuneId(int tuneId)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            hazzatController.GetSeasonsByTuneId(tuneId, OnGetSeasonsByTuneIdCompleted);
        }

        private void OnGetSeasonsByTuneIdCompleted(object sender, GetSeasonsByTuneIDCompletedEventArgs e)
        {
            TuneSeasons = e.Result;
            MessagingCenter.Send(this, "DoneWithSeasonsListByTune");
            App.IsLoaded = true;
        }

        #endregion
    }
}
