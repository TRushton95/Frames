namespace Frames.Deserialisation.Converters
{
    #region Usings

    using Microsoft.Xna.Framework;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Reflection;

    #endregion

    /// <summary>
    /// Deserializes the <see cref="Color"/> class from json.
    /// </summary>
    public class ColorConverter : JsonConverter
    {
        #region Methods

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Color));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken jToken = JToken.Load(reader);

            // If ui specification used a preset colour string
            if (jToken.Type == JTokenType.String)
            {
                Color result = Color.Transparent;

                Color color = new Color();
                Type colorType = color.GetType();

                string[] colorDetails = jToken.Value<string>().Split('-');
                string presetColor = colorDetails[0];

                PropertyInfo propertyInfo = colorType.GetProperty(presetColor);

                if (propertyInfo != null)
                {
                    result = (Color)propertyInfo.GetValue(color, null);

                    // If second value given for color, try to get an alpha value from it
                    if (colorDetails.Length == 2 && byte.TryParse(colorDetails[1], out byte alpha))
                    {
                        result.A = alpha;
                    }
                }

                return result;
            }

            // If an RGBA object in supplied, exctract an build a color from the values
            byte r = jToken["R"].Value<byte>();
            byte g = jToken["G"].Value<byte>();
            byte b = jToken["B"].Value<byte>();
            byte a = jToken["A"].Value<byte>();

            return new Color(r, g, b, a);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
