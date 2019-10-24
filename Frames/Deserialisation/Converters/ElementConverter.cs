namespace Frames.Deserialisation.Converters
{
    #region Usings

    using System;
    using Frames.UI.Elements;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// Deserializes the <see cref="BaseElement"/> class from json.
    /// </summary>
    public class ElementConverter : JsonConverter
    {
        #region Methods

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(BaseElement));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            object result = null;

            string type = jObject["Type"].Value<string>();
            switch (type)
            {
                case "Button":
                    result = jObject.ToObject<Button>(serializer);
                    break;

                case "Text":
                    result = jObject.ToObject<Text>(serializer);
                    break;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
