namespace Frames.Deserialisation.Converters
{
    #region Usings

    using Frames.DataStructures;
    using Microsoft.Xna.Framework.Graphics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Resources;
    using System;

    #endregion

    public class ImageConverter : JsonConverter
    {
        #region Methods

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Texture2D));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jToken = JToken.Load(reader);

            string imageFileName = jToken.Value<string>();
            Texture2D result = Resources.Instance.Textures[imageFileName];

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
