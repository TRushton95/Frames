namespace Frames.Deserialisation.Converters
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using Frames.Factories;
    using Frames.UserInterface.Elements;
    using Microsoft.Xna.Framework;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NLog;
    using Resources;

    #endregion

    /// <summary>
    /// Deserializes the <see cref="BaseElement"/> class from json.
    /// </summary>
    public class ElementConverter : JsonConverter
    {
        #region Constants

        private const string BlockerTypeName = "Blocker";

        #endregion

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

            if (typeName == BlockerTypeName)
            {
                return this.ConvertToBlocker(jObject, serializer);
            }

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

        /// <summary>
        /// Converts the jObject to a container blocker.
        /// </summary>
        /// <remarks>
        /// A blocker is just a transparent container that matches the window bounds in order to prevent interaction with the rest of the ui.
        /// </remarks>
        private Container ConvertToBlocker(JObject jObject, JsonSerializer serializer)
        {
            string name = jObject["Name"].Value<string>();
            List<BaseElement> children = jObject["Children"].ToObject<List<BaseElement>>(serializer);

            Container result =  new Container(name, Resources.Instance.Screen.Width, Resources.Instance.Screen.Height, null, Color.Transparent, PositionFactory.Center(), children);
            result.Visible = jObject["Visible"] == null ? true : jObject["Visible"].Value<bool>();

            return result;
        }

        #endregion
    }
}
