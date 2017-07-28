
using Hazzat.Helpers;
using Hazzat.Service.Providers.DataProviders.WebServiceProvider;
using System.Collections.ObjectModel;

namespace Hazzat.Models
{
    /// <summary>
    /// Represents a collection of hymns grouped by a service or a season
    /// </summary>
    public class HymnGroup : ObservableGroupedCollection<ServiceHymnMenuItem>
    {
        string name = string.Empty;

        /// <summary>
        /// Gets/Sets group Name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        /// <summary>
        /// Initialize a hymn group with the given name
        /// </summary>
        /// <param name="name">Group name.</param>
        public HymnGroup(string name) : base()
        {
            Name = name;
        }
    }
}
