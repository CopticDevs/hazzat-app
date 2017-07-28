using Hazzat.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazzat.Models
{
    /// <summary>
    /// Mirrors StructureInfo but only includes the fields used by the app and makes them observables
    /// TODO: Probably not needed since nothing binds to it.  It's just used to populate the service list in order
    /// </summary>
    public class SimplifiedStructureInfo : ObservableObject
    {
        int itemId = -1;

        /// <summary>
        /// Gets/sets item id
        /// </summary>
        public int ItemId
        {
            get { return itemId; }
            set { SetProperty(ref itemId, value); }
        }

        int service_Order = -1;

        /// <summary>
        /// Gets/sets the service order
        /// </summary>
        public int Service_Order
        {
            get { return service_Order; }
            set { SetProperty(ref service_Order, value); }
        }
    }
}
