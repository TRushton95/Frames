namespace Frames.EventSystem
{
    #region Usings

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Represents a unique event type by mapping an id to a human-readable event name.<br/>
    /// An EventType must be requested through <see cref="GetEventType(string)"/> as strict control is maintained over the id mappings to event names.
    /// </summary>
    public class EventType
    {
        #region Fields

        private static int nextId = 0;
        private static Dictionary<string, EventType> types = new Dictionary<string, EventType>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="EventType"/> class.
        /// </summary>
        private EventType(string name, int id)
        {
            this.Name = name;
            this.Id = id;
        }

        #endregion

        #region Properties

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

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets the event type of the given event name. If one does not exist, create one first, assign it an id and then return that.
        /// </summary>
        public static EventType GetEventType(string eventName)
        {
            EventType result;
            bool found = types.TryGetValue(eventName, out result);

            if (!found)
            {
                result = new EventType(eventName, nextId++);
            }

            return result;
        }

        #endregion
    }
}
