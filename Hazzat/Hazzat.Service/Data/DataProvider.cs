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
                // TODO: get actual assembly name
                const string assembly = "Hazzat.Modules.Hymns.Data.SqlDataprovider,Hazzat.Modules.Hymns";
                Type objectType = Type.GetType(assembly, true);

                provider = (DataProvider)Activator.CreateInstance(objectType);
            }

            return provider;
        }

        #endregion

        #region Abstract Methods

        // TODO: fix return type
        public abstract object GetSeason(int seasonId);

        #endregion
    }
}
