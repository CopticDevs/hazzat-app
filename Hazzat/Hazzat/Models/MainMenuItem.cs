using Hazzat.Helpers;

namespace Hazzat.Models
{
    /// <summary>
    /// Represents a menu item for the main menu
    /// </summary>
    public class MainMenuItem : ObservableObject
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

        string name = string.Empty;

        /// <summary>
        /// Gets/Sets item name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        int serviceHymnsCount = 0;

        /// <summary>
        /// Gets/Sets hymn count associated with item
        /// </summary>
        public int ServiceHymnsCount
        {
            get { return serviceHymnsCount; }
            set { SetProperty(ref serviceHymnsCount, value); }
        }
    }
}
