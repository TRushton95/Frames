namespace Frames.EventSystem
{
    #region Usings

    using System.Collections.Generic;
    using Frames.Events.EventSystem;
    using Frames.Utilities;

    #endregion

    #region Delegates

    public delegate void EventHandler(Event e);

    #endregion

    /// <summary>
    /// The listener base class to notify and receive messages from the <see cref="EventManager"/>.
    /// </summary>
    public abstract class Listener
    {
        #region Fields

        protected EventManager eventManager = EventManager.Instance;
        private SortedDictionary<string, List<EventHandler>> eventHandlerLookup = new SortedDictionary<string, List<EventHandler>>();

        #endregion

        #region Properties

        /// <summary> 
        /// Gets the name of the property.
        /// </summary>
        /// <remarks>
        /// This is used for the routing of events and linking an element with its corresponding script file.
        /// Must be unique.
        /// </remarks>
        public string Name
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoke the relevant event handlers on a received event.
        /// TO-DO: Using string name comparison for events in Listeners, ids get set int <see cref="EventManager"/> but not here.
        /// </summary>
        public void OnEventReceived(Event e)
        {
            if (e == null || string.IsNullOrWhiteSpace(e.EventType))
            {
                return;
            }
            string eventId = EventHelper.GetEventId(e.Sender, e.EventType);

            List<EventHandler> eventHandlers;
            bool eventTypeFound = eventHandlerLookup.TryGetValue(eventId, out eventHandlers);

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
        /// <remarks>
        /// To be called from python scripts.
        /// </remarks>
        public void AddEventHandler(string sender, string eventType, EventHandler eventHandlerToAdd)
        {
            string eventId = EventHelper.GetEventId(sender, eventType);

            List<EventHandler> eventHandlers;
            bool eventTypeFound = eventHandlerLookup.TryGetValue(eventId, out eventHandlers);

            if (!eventTypeFound)
            {
                eventHandlers = new List<EventHandler>();
                eventHandlerLookup.Add(eventId, eventHandlers);
            }

            eventHandlers.Add(eventHandlerToAdd);

            eventManager.AddEventListener(sender, eventType, this);
        }

        /// <summary>
        /// Remove an event handler.
        /// </summary>
        /// <remarks>
        /// To be called from python scripts.
        /// </remarks>
        public void RemoveEventHandler(string sender, string eventType, EventHandler eventHandlerToRemove)
        {
            string eventId = EventHelper.GetEventId(sender, eventType);

            List<EventHandler> eventHandlers;
            bool eventTypeFound = eventHandlerLookup.TryGetValue(eventId, out eventHandlers);

            if (!eventTypeFound)
            {
                return;
            }

            eventHandlers.Remove(eventHandlerToRemove);

            if (eventHandlers.Count == 0)
            {
                eventManager.RemoveEventListener(sender, eventType, this);
            }
        }

        #endregion
    }
}
