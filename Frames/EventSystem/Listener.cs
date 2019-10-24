namespace Frames.EventSystem
{
    #region Usings

    using System.Collections.Generic;

    #endregion

    #region Delegates

    public delegate void EventHandler(BaseEvent e);

    #endregion

    /// <summary>
    /// The listener base class to notify and receive messages from the <see cref="EventManager"/>.
    /// </summary>
    public abstract class Listener
    {
        #region Fields

        protected EventManager eventManager = EventManager.Instance;
        private SortedDictionary<string, List<EventHandler>> eventHandlerLookup;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="Listener"/> class.
        /// </summary>
        public Listener()
        {
            eventHandlerLookup = new SortedDictionary<string, List<EventHandler>>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoke the relevant event handlers on a received event.
        /// TO-DO: Using string name comparison for events in Listeners, ids get set int <see cref="EventManager"/> but not here.
        /// </summary>
        public void OnEventReceived(BaseEvent e)
        {
            string eventType = e.EventType.Name;

            if (string.IsNullOrWhiteSpace(eventType))
            {
                return;
            }

            List<EventHandler> eventHandlers;
            bool eventTypeFound = eventHandlerLookup.TryGetValue(eventType, out eventHandlers);

            if (!eventTypeFound)
            {
                return;
            }

            foreach (EventHandler eventHandler in eventHandlers)
            {
                eventHandler.Invoke(e);
            }
        }

        /// <summary>
        /// Add a new event handler to handle a given event type.
        /// </summary>
        public void AddEventHandler(EventType eventType, EventHandler eventHandlerToAdd)
        {
            List<EventHandler> eventHandlers;
            bool eventTypeFound = eventHandlerLookup.TryGetValue(eventType.Name, out eventHandlers);

            if (!eventTypeFound)
            {
                eventHandlers = new List<EventHandler>();
                eventHandlerLookup.Add(eventType.Name, eventHandlers);
            }

            eventHandlers.Add(eventHandlerToAdd);

            eventManager.AddEventListener(eventType, this);
        }

        /// <summary>
        /// Remove an event handler.
        /// </summary>
        public void RemoveEventHandler(EventType eventType, EventHandler eventHandlerToRemove)
        {
            List<EventHandler> eventHandlers;
            bool eventTypeFound = eventHandlerLookup.TryGetValue(eventType.Name, out eventHandlers);

            if (!eventTypeFound)
            {
                return;
            }

            eventHandlers.Remove(eventHandlerToRemove);

            if (eventHandlers.Count == 0)
            {
                eventManager.RemoveEventListener(eventType, this);
            }
        }

        #endregion
    }
}
