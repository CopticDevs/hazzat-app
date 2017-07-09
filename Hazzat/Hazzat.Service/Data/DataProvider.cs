using hazzat.com;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazzat.Service.Data
{
    /// <summary>
    /// An abstract class for the Data Access Layer (DAL)
    /// </summary>
    public abstract class DataProvider
    {
        #region Shared/Static Methods

        /// <summary>
        /// A static singleton for the data provider
        /// </summary>
        private static DataProvider provider;

        /// <summary>
        /// Returns the current Data Provider instance
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {
            if (provider == null)
            {
                const string assembly = "Hazzat.Service.Providers.DataProviders.WebServiceProvider.WebServiceProvider,Hazzat.Service";
                Type objectType = Type.GetType(assembly, true);

                provider = (DataProvider)Activator.CreateInstance(objectType);
            }

            return provider;
        }

        #endregion

        #region Abstract Methods

        public abstract void GetSeasons(bool isDateSpecific, Action<object, GetSeasonsCompletedEventArgs> callback);
        public abstract void GetSeasonServices(int seasonId, Action<object, GetSeasonServicesCompletedEventArgs> callback);
        public abstract void GetSeasonServiceHymns(int structureId, Action<object, GetSeasonServiceHymnsCompletedEventArgs> callback);
        public abstract void GetSeasonServiceHymnText(int itemId, Action<object, GetSeasonServiceHymnTextCompletedEventArgs> callback);
        public abstract void GetSeasonServiceHymnHazzat(int itemId, Action<object, GetSeasonServiceHymnHazzatCompletedEventArgs> callback);
        public abstract void GetSeasonServiceHymnVerticalHazzat(int itemId, Action<object, GetSeasonServiceHymnVerticalHazzatCompletedEventArgs> callback);
        public abstract void GetTypeList(Action<object, GetTypeListCompletedEventArgs> callback);
        public abstract void GetSeasonsByTypeId(int typeId, Action<object, GetSeasonsByTypeIDCompletedEventArgs> callback);
        public abstract void GetServiceHymnListBySeasonIdAndTypeId(int seasonId, int typeId, Action<object, GetServiceHymnListBySeasonIdAndTypeIdCompletedEventArgs> callback);
        public abstract void GetServiceHymnListBySeasonIdAndTuneId(int seasonId, int tuneId, Action<object, GetServiceHymnListBySeasonIdAndTuneIdCompletedEventArgs> callback);

        #endregion
    }
}
