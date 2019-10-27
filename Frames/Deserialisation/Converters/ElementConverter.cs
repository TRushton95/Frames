namespace Frames.Deserialisation.Converters
{
    #region Usings

    using System;
    using Frames.UserInterface.Elements;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NLog;

    #endregion

    /// <summary>
    /// Deserializes the <see cref="BaseElement"/> class from json.
    /// </summary>
    public class ElementConverter : JsonConverter
    {
        #region Fields

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string elementsNamespace = typeof(BaseElement).Namespace;

        #endregion

        #region Methods

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(BaseElement));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            string typeName = jObject["Type"].Value<string>();

            Type type = Type.GetType($"{this.elementsNamespace}.{typeName}");

            if (type == null || !type.IsSubclassOf(typeof(BaseElement)))
            {
                logger.Warn($"Cannot convert type {typeName} to element.");
                return null;
            }

            return jObject.ToObject(type, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
