namespace Frames.Deserialisation.Converters
{
    #region Usings

    using Microsoft.Xna.Framework;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;

    #endregion

    public class ColorConverter : JsonConverter
    {
        #region Methods

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Color));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            byte r = jObject["R"].Value<byte>();
            byte g = jObject["G"].Value<byte>();
            byte b = jObject["B"].Value<byte>();
            byte a = jObject["A"].Value<byte>();

            return new Color(r, g, b, a);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
