﻿namespace Frames.UserInterface
{
    #region Usings

    using Frames.Deserialisation.Converters;
    using Frames.UserInterface.Elements;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Newtonsoft.Json;
    using NLog;
    using Resources;
    using System.Collections.Generic;
    using System.IO;

    #endregion

    public class UserInterface
    {
        #region Fields

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private List<BaseElement> elements = new List<BaseElement>();

        private readonly JsonConverter[] converters = {
                new PositionProfileConverter(),
                new SpriteFontConverter(),
                new ColorConverter(),
                new ElementConverter(),
                new DimensionConverter()
            };

        #endregion

        #region Methods

        /// <summary>
        /// Loads the elements into the user interface from the json file at the given path.
        /// </summary>
        public void Load(string path)
        {
            if (!File.Exists(path))
            {
                logger.Warn("Failed to load file - file does not exist at {0}", path);
            }

            string json = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(json))
            {
                logger.Warn("No content read from file.");
            }

            elements = JsonConvert.DeserializeObject<List<BaseElement>>(json, new JsonSerializerSettings() { Converters = converters });
        }

        /// <summary>
        /// Initialises the elements.
        /// </summary>
        public void Initialise()
        {
            Rectangle viewportBounds = Resources.Instance.GraphicsDevice.Viewport.Bounds;

            foreach (BaseElement element in this.elements)
            {
                element.Initialise(viewportBounds);
            }
        }

        /// <summary>
        /// Draws the elements.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (BaseElement element in this.elements)
            {
                element.Draw(spriteBatch);
            }
        }

        #endregion
    }
}
