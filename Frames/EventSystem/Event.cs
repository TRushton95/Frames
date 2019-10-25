namespace Frames.Events.EventSystem
{
    #region Usings

    using Frames.EventSystem;

    #endregion

    /// <summary>
    /// The event that is used to notify listeners of a change of state along with any necessary data regarding that change.
    /// </summary>
    public class Event
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BaseEvent"/> class.
        /// </summary>
        public Event(string name, object data)
        {
            this.Name = name;
            this.Data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the event name.
        /// </summary>
        public string Name
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
