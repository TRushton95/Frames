namespace Frames.EventSystem
{
    #region Usings

    using System.Collections.Generic;
    using System.Diagnostics;
    using Frames.Events.EventSystem;
    using Frames.Utilities;

    #endregion

    /// <summary>
    /// The event manager that is responsible for relaying events to subscribed listeners.
    /// </summary>
    public class EventManager
    {
        #region Fields

        private static EventManager _instance;
        private SortedDictionary<string, List<Listener>> eventListenerLookup = new SortedDictionary<string, List<Listener>>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static EventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventManager();
                }

                return _instance;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Notify the event manager of a new event
        /// </summary>
        public void Notify(Event e)
        {
            string eventId = EventHelper.GetEventId(e.Sender, e.EventType);

            List<Listener> eventListeners;
            bool eventTypeFound = eventListenerLookup.TryGetValue(eventId, out eventListeners);

            Debug.WriteLine(string.Format("Event received: {0} ({1} listeners)", eventId, eventListeners == null ? 0 : eventListeners.Count)); //TODO: Remove me

            if (!eventTypeFound)
            {
                return;
            }

            foreach (Listener eventListener in eventListeners)
            {
                eventListener.OnEventReceived(e);
            }
        }

        /// <summary>
        /// Register a new listener for a given event type.
        /// </summary>
        public void AddEventListener(string sender, string eventType, Listener listener)
        {
            string eventId = EventHelper.GetEventId(sender, eventType);

            List<Listener> eventListeners;
            bool eventTypeFound = eventListenerLookup.TryGetValue(eventId, out eventListeners);

            if (!eventTypeFound)
            {
                eventListeners = new List<Listener>();
                eventListenerLookup.Add(eventId, eventListeners);
            }

            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        /// <summary>
        /// Deregister an event listener from a given event type.
        /// </summary>
        /// <remarks>If no listeners remain for a given event type, remove the empty list entry.</remarks>
        public void RemoveEventListener(string sender, string eventType, Listener listener)
        {
            string eventId = EventHelper.GetEventId(sender, eventType);

            List<Listener> eventListeners;
            bool eventTypeFound = eventListenerLookup.TryGetValue(eventId, out eventListeners);

            if (!eventTypeFound)
            {
                return;
            }

            eventListeners.Remove(listener);

            if (eventListeners.Count == 0)
            {
                eventListenerLookup.Remove(eventId);
            }
        }

        #endregion
    }
}
