namespace Frames.Events.EventSystem
{
    #region Usings

    using Frames.EventSystem;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The event that is used to notify listeners of a change of state along with any necessary data regarding that change.
    /// </summary>
    public class Event
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="Event"/> class.
        /// </summary>
        public Event(string sender, string eventType, object data)
        {
            this.Sender = sender;
            this.EventType = eventType;
            this.Data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the sender name.
        /// </summary>
        public string Sender
        {
            get;
        }

        /// <summary>
        /// Gets the event type.
        /// </summary>
        public string EventType
        {
            get;
        }

        /// <summary>
        /// Gets the event data.
        /// </summary>
        public object Data
        {
            get;
        }

        #endregion
    }
}
