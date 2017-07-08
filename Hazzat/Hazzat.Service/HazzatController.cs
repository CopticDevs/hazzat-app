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
        public object GetSeason(int seasonId)
        {
            return DataProvider.Instance().GetSeason(seasonId);
        }
    }
}
