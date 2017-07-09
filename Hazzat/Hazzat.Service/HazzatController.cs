using hazzat.com;
using Hazzat.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
