namespace Frames.Utilities
{
    #region Usings

    using Frames.Events.EventSystem;
    using NLog;
    using System.Text;

    #endregion

    public static class EventHelper
    {
        #region Fields

        private static Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Methods

        /// <summary>
        /// Gets the event id string that represents a unique event/sender combination.
        /// </summary>
        public static string GetEventId(string sender, string eventTypeName)
        {
            return $"{sender}.{eventTypeName}";
        }

        /// <summary>
        /// Safely attempts to convert event data to an output type.
        /// </summary>
        public static bool TryConvertData<T>(Event e, out T data)
        {
            bool result = false;
            data = default(T);

            try
            {
                data = (T)e.Data;
                result = true;
            }
            catch
            {
                logger.Error(GetEventHandlerErrorMessage(e, typeof(T).Name));
            }

            return result;
        }

        /// <summary>
        /// Gets the error message for a failed event data conversion.
        /// </summary>
        private static string GetEventHandlerErrorMessage(Event e, string dataCastType)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Error attempting to cast event data to type {dataCastType}");
            sb.AppendLine($"Sender: {e.Sender}");
            sb.AppendLine($"EventType: {e.EventType}");
            sb.AppendLine($"Data: {e.Data}");

            return sb.ToString();
        }

        #endregion
    }
}
