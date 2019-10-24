namespace Frames.Deserialisation.Converters
{
    #region Usings

    using System;
    using System.IO;
    using Microsoft.Xna.Framework.Graphics;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Resources;

    #endregion

    /// <summary>
    /// Deserializes the <see cref="SpriteFont"/> class from json.
    /// </summary>
    public class SpriteFontConverter : JsonConverter
    {
        #region Methods

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(SpriteFont));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jToken = JToken.Load(reader);

            string fontFileName = jToken.Value<string>();
            SpriteFont result = Resources.Instance.Fonts[fontFileName];

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
