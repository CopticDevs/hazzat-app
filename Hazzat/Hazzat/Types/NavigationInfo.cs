namespace Hazzat.Types
{
    public class NavigationInfo
    {
        /// <summary>
        /// Gets/Sets Navigation method.
        /// </summary>
        public NavigationMethod Method { get; set; }

        /// <summary>
        /// Gets/Sets the item id.
        /// </summary>
        /// <remarks>
        /// The Item id is the season/tune/type id based on the navigation method.
        /// </remarks>
        public int ItemId { get; set; }

        /// <summary>
        /// Instantiates a new NavigationInfo with the given parameters.
        /// </summary>
        /// <param name="method">Navigation method.</param>
        /// <param name="itemId">Season/Tune/Type id based on navigation method.</param>
        public NavigationInfo(NavigationMethod method, int itemId)
        {
            Method = method;
            ItemId = itemId;
        }
    }
}
