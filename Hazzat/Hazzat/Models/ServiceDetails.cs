
using Hazzat.Helpers;
using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
using System.Collections.ObjectModel;

namespace Hazzat.Models
{
    //TODO: rename service details to be hymnGroupdetails
    public class ServiceDetails : ObservableGroupedCollection<ServiceHymnMenuItem>
    {
        string serviceName = string.Empty;

        /// <summary>
        /// Gets/Sets Service Name
        /// </summary>
        public string ServiceName
        {
            get { return serviceName; }
            set { SetProperty(ref serviceName, value); }
        }

        int structureId = -1;

        /// <summary>
        /// Gets/sets the structure id
        /// </summary>
        public int StructureId
        {
            get { return structureId; }
            set { SetProperty(ref structureId, value); }
        }
    }
}
