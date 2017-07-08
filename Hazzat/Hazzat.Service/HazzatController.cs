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
        /// Gets Seasons list, then calls the callback methos upon completion.
        /// </summary>
        /// <param name="isDateSpecific">Inddicates if the seasons list retrieved should be date specific or not.</param>
        /// <param name="callback">Call back method upon completion</param>
        public void GetSeasons(bool isDateSpecific, Action<object, GetSeasonsCompletedEventArgs> callback)
        {
            DataProvider.Instance().GetSeasons(isDateSpecific, callback);
        }
    }
}
