namespace Frames.EventSystem
{
    /// <summary>
    /// Represents a unique event type by mapping an id to a human-readable event name.
    /// </summary>
    public class EventType
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="EventType"/> class.
        /// </summary>
        public EventType(string name)
        {
            this.Name = name;
            this.Id = -1;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get;
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id
        {
            get;
            set;
        }
    }
}
