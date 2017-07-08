using hazzat.com;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazzat.Service.Providers.DataProviders.WebServiceProvider
{
    public class ServiceDetails : ObservableCollection<ServiceHymnInfo>
    {
        public string ServiceName { get; set; }
        public int StructureId { get; set; }
    }
}
