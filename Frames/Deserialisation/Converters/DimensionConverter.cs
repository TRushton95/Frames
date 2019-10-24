namespace Frames.Deserialisation.Converters
{
    #region Usings

    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Resources;

    #endregion

    public class DimensionConverter : JsonConverter
    {
        #region Methods

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(int));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jToken = JToken.Load(reader);

            int result = -1;

            if (jToken.Value<string>() == "WindowWidth")
            {
                result = Resources.Instance.GraphicsDevice.Viewport.Width;
            }
            else if (jToken.Value<string>() == "WindowHeight")
            {
                result = Resources.Instance.GraphicsDevice.Viewport.Height;
            }
            else
            {
                result = jToken.Value<int>();
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
