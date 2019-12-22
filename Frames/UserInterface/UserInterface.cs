namespace Frames.UserInterface
{
    #region Usings

    using Frames.Deserialisation.Converters;
    using Frames.UserInterface.Elements;
    using Frames.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Newtonsoft.Json;
    using NLog;
    using Resources;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    #endregion

    public class UserInterface
    {
        #region Constants

        private int InitialPriority = 0;

        #endregion

        #region Fields

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private List<BaseElement> elements = new List<BaseElement>();
        private BaseElement hoveredElement, previousHoveredElement;

        private readonly JsonConverter[] converters = {
                new PositionProfileConverter(),
                new SpriteFontConverter(),
                new ColorConverter(),
                new ElementConverter(),
                new DimensionConverter(),
                new ImageConverter()
            };

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path of the ui file.
        /// </summary>
        private string Path
        {
            get;
            set;
        }

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

            this.elements = JsonConvert.DeserializeObject<List<BaseElement>>(json, new JsonSerializerSettings() { Converters = converters });
            this.Path = path;

            logger.Debug($"Loaded user interface from: {path}");
        }

        /// <summary>
        /// Reloads the ui with the last given path.
        /// </summary>
        public void Reload()
        {
            logger.Debug($"Reloading ui from {this.Path}");

            this.Load(this.Path);
        }

        /// <summary>
        /// Initialises the elements.
        /// </summary>
        public void Initialise()
        {
            Rectangle viewportBounds = Resources.Instance.GraphicsDevice.Viewport.Bounds;

            foreach (BaseElement element in this.elements)
            {
                element.Initialise(viewportBounds, InitialPriority);
            }
        }

        /// <summary>
        /// Updates the elements.
        /// </summary>
        public void Update(Vector2 mousePosition)
        {
            MouseInfo.Update();
            KeyboardInfo.Update();

            this.UpdateHoveredElement();
            if (this.hoveredElement != null)
            {
                if (MouseInfo.LeftMouseClicked)
                {
                    this.hoveredElement.LeftClick();
                }

                if (MouseInfo.MouseWheelScrolledUp)
                {
                    this.hoveredElement.MouseWheelScrollUp();
                }
                if (MouseInfo.MouseWheelScrolledDown)
                {
                    this.hoveredElement.MouseWheelScrollDown();
                }
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

        /// <summary>
        /// Gets all the elements in the user interface.
        /// </summary>
        private List<BaseElement> GetAllElements()
        {
            List<BaseElement> result = new List<BaseElement>();

            foreach (BaseElement element in this.elements)
            {
                result.AddRange(element.BuildFlattenedSubTree());
            }

            return result;
        }

        /// <summary>
        /// Updates the hovered element
        /// </summary>
        private void UpdateHoveredElement()
        {
            List<BaseElement> hoveredElements = this.GetAllElements()
                                                    .Where(element => element.GetBounds().Contains(MouseInfo.Position))
                                                    .ToList();
            
            BaseElement nextHoveredElement = null;

            if (hoveredElements.Count > 0)
            {
                // If multiple hovered elements share priority, leave it to fate
                nextHoveredElement = hoveredElements.OrderByDescending(element => element.Priority).First();
            }

            this.previousHoveredElement = this.hoveredElement;
            this.hoveredElement = nextHoveredElement;

            if (this.hoveredElement == this.previousHoveredElement)
            {
                return;
            }

            if (this.previousHoveredElement != null)
            {
                this.previousHoveredElement.HoverLeave();
            }

            if (this.hoveredElement != null)
            {
                this.hoveredElement.Hover();
            }
        }

        #endregion
    }
}
