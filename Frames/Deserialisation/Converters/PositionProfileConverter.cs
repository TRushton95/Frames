namespace Frames.Deserialisation.Converters
{
    #region Usings

    using System;
    using Frames.DataStructures.PositionProfiles;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    /// Deserializes the <see cref="IPositionProfile"/> class from json.
    /// </summary>
    public class PositionProfileConverter : JsonConverter
    {
        #region Methods

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IPositionProfile));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            if (jObject["Type"].Value<string>() == "Relative")
            {
                return jObject.ToObject<RelativePositionProfile>(serializer);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
