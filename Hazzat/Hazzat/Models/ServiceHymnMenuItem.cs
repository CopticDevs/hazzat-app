using Hazzat.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazzat.Models
{
    public class ServiceHymnMenuItem : ObservableObject
    {
        int itemId = -1;

        /// <summary>
        /// Gets/Sets item Id
        /// </summary>
        public int ItemId
        {
            get { return itemId; }
            set { SetProperty(ref itemId, value); }
        }

        string title = string.Empty;

        /// <summary>
        /// Gets/Sets hymn title
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string structure_Name = string.Empty;

        /// <summary>
        /// Gets/Sets hymn structure name
        /// </summary>
        public string Structure_Name
        {
            get { return structure_Name; }
            set { SetProperty(ref structure_Name, value); }
        }
    }
}
