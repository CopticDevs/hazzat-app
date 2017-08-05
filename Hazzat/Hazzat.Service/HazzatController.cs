using Hazzat.Service.Data;
using System;

namespace Hazzat.Service
{
    public class HazzatController
    {
        /// <summary>
        /// Gets Seasons list, then calls the callback method upon completion.
        /// </summary>
        /// <param name="isDateSpecific">Indicates if the seasons list retrieved should be date specific or not.</param>
        /// <param name="callback">Call back method upon completion</param>
        public void GetSeasons(bool isDateSpecific, Action<object, GetSeasonsCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetSeasons(isDateSpecific, callback);
        }

        /// <summary>
        /// Gets the services list in the season, then calls the callback method upon completion.
        /// </summary>
        /// <param name="seasonId">The season id to retrieve its services.</param>
        /// <param name="callback">Call back method upon completion</param>
        public void GetSeasonServices(int seasonId, Action<object, GetSeasonServicesCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetSeasonServices(seasonId, callback);
        }

        /// <summary>
        /// Gets the list of hymns in a particular Structure (Service in a Season), then calls the callback method upon completion.
        /// </summary>
        /// <param name="structureId">The structure id to retrieve its hymns.</param>
        /// <param name="callback">Call back method upon completion.</param>
        public void GetSeasonServiceHymns(int structureId, Action<object, GetSeasonServiceHymnsCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetSeasonServiceHymns(structureId, callback);
        }

        /// <summary>
        /// Gets the text associated with a hymn, then calls the callback method upon completion.
        /// </summary>
        /// <param name="itemId">The hymn id.</param>
        /// <param name="callback">Call back method upon completion.</param>
        public void GetSeasonServiceHymnText(int itemId, Action<object, GetSeasonServiceHymnTextCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetSeasonServiceHymnText(itemId, callback);
        }

        /// <summary>
        /// Gets the hazzat associated with a hymn, then calls the callback method upon completion.
        /// </summary>
        /// <param name="itemId">The hymn id.</param>
        /// <param name="callback">Call back method upon completion.</param>
        public void GetSeasonServiceHymnHazzat(int itemId, Action<object, GetSeasonServiceHymnHazzatCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetSeasonServiceHymnHazzat(itemId, callback);
        }

        /// <summary>
        /// Gets the vertical hazzat associated with a hymn, then calls the callback method upon completion.
        /// </summary>
        /// <param name="itemId">The hymn id.</param>
        /// <param name="callback">Call back method upon completion.</param>
        public void GetSeasonServiceHymnVerticalHazzat(int itemId, Action<object, GetSeasonServiceHymnVerticalHazzatCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetSeasonServiceHymnVerticalHazzat(itemId, callback);
        }

        /// <summary>
        /// Gets the list of hymn types, then calls the callback method upon completion.
        /// </summary>
        /// <param name="callback">Call back method upon completion.</param>
        public void GetTypeList(Action<object, GetTypeListCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetTypeList(callback);
        }

        /// <summary>
        /// Gets the list of seasons containing hymns of the given type id, then calls the callback method upon completion.
        /// </summary>
        /// <param name="typeId">The type id.</param>
        /// <param name="callback">Call back method upon completion.</param>
        public void GetSeasonsByTypeId(int typeId, Action<object, GetSeasonsByTypeIDCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetSeasonsByTypeId(typeId, callback);
        }

        /// <summary>
        /// Gets the list of hymns in a given season that match a given type id, then calls the callback method upon completion.
        /// </summary>
        /// <param name="seasonId">The season id.</param>
        /// <param name="typeId">The hymn type id.</param>
        /// <param name="callback">Call back method upon completion.</param>
        public void GetServiceHymnListBySeasonIdAndTypeId(int seasonId, int typeId, Action<object, GetServiceHymnListBySeasonIdAndTypeIdCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetServiceHymnListBySeasonIdAndTypeId(seasonId, typeId, callback);
        }

        /// <summary>
        /// Gets the list of hymns in a given season that match a given tune id, then calls the callback method upon completion.
        /// </summary>
        /// <param name="seasonId">The season id.</param>
        /// <param name="tuneId">The tune id.</param>
        /// <param name="callback">Call back method upon completion.</param>
        public void GetServiceHymnListBySeasonIdAndTuneId(int seasonId, int tuneId, Action<object, GetServiceHymnListBySeasonIdAndTuneIdCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetServiceHymnListBySeasonIdAndTuneId(seasonId, tuneId, callback);
        }

        /// <summary>
        /// Gets the list of tunes, then calls the callback method upon completion.
        /// </summary>
        /// <param name="callback">Call back method upon completion.</param>
        public void GetTuneList(Action<object, GetTuneListCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetTuneList(callback);
        }

        /// <summary>
        /// Gets the list of seasons containing hymns of the given tune id, then calls the callback method upon completion.
        /// </summary>
        /// <param name="tuneId">The tune id.</param>
        /// <param name="callback">Call back method upon completion.</param>
        public void GetSeasonsByTuneId(int tuneId, Action<object, GetSeasonsByTuneIDCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetSeasonsByTuneId(tuneId, callback);
        }

        public void GetTextRowDelimiterToken(Action<object, GetTextRowDelimiterTokenCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetTextRowDelimiterToken(callback);
        }
    }
}
