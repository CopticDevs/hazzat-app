using System.Collections.ObjectModel;

namespace Hazzat.Service.Providers.DataProviders.WebServiceProvider
{
    public class ServiceDetails : ObservableCollection<ServiceHymnInfo>
    {
        public string ServiceName { get; set; }
        public int StructureId { get; set; }
    }
}
