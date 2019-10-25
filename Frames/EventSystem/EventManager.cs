namespace Frames.EventSystem
{
    #region Usings

    using System.Collections.Generic;
    using Frames.Events.EventSystem;

    #endregion

    /// <summary>
    /// The event manager that is responsible for relaying events to subscribed listeners.
    /// </summary>
    public class EventManager
    {
        #region Fields

        private static EventManager _instance;
        private SortedDictionary<int, List<Listener>> eventListenerLookup = new SortedDictionary<int, List<Listener>>();
        private int nextEventId = 0;

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

        /// <summary>
        /// Notify the event manager of a new event
        /// </summary>
        public void Notify(BaseEvent e)
        {
            List<Listener> eventListeners;
            bool eventTypeFound = eventListenerLookup.TryGetValue(e.EventType.Id, out eventListeners);

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
        public void AddEventListener(EventType eventType, Listener listener)
        {
            if (eventType.Id == -1)
            {
                eventType.Id = nextEventId++;
            }

            List<Listener> eventListeners;
            bool eventTypeFound = eventListenerLookup.TryGetValue(eventType.Id, out eventListeners);

            if (!eventTypeFound)
            {
                eventListeners = new List<Listener>();
                eventListenerLookup.Add(eventType.Id, eventListeners);
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
        public void RemoveEventListener(EventType eventType, Listener listener)
        {
            List<Listener> eventListeners;
            bool eventTypeFound = eventListenerLookup.TryGetValue(eventType.Id, out eventListeners);

            if (!eventTypeFound)
            {
                return;
            }

            eventListeners.Remove(listener);

            if (eventListeners.Count == 0)
            {
                eventListenerLookup.Remove(eventType.Id);
            }
        }

        #endregion
    }
}
