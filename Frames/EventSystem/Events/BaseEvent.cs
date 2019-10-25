namespace Frames.Events.EventSystem
{
    #region Usings

    using Frames.EventSystem;

    #endregion

    /// <summary>
    /// The base structure of the event that is used to notify listeners of a change of state.
    /// </summary>
    public abstract class BaseEvent
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BaseEvent"/> class.
        /// </summary>
        public BaseEvent(EventType eventType)
        {
            this.EventType = eventType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the event type.
        /// </summary>
        public EventType EventType
        {
            get;
        }

        #endregion
    }
}
