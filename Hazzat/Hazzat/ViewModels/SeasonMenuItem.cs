using Hazzat.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazzat.ViewModels
{
    public class SeasonMenuItem : ObservableObject
    {
        int id = -1;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        int serviceHymnsCount = 0;
        public int ServiceHymnsCount
        {
            get { return serviceHymnsCount; }
            set { SetProperty(ref serviceHymnsCount, value); }
        }
    }
}
