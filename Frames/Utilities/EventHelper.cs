namespace Frames.Utilities
{
    public class EventHelper
    {
        /// <summary>
        /// Gets the event id string that represents a unique event/sender combination.
        /// </summary>
        public static string GetEventId(string sender, string eventTypeName)
        {
            return $"{sender}.{eventTypeName}";
        }
    }
}
