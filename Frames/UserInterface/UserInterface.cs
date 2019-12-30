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
        private BaseElement hoveredElement, previousHoveredElement, heldElement;

        private readonly JsonConverter[] converters = {
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
        public void Update(GameTime gameTime, Vector2 mousePosition)
        {
            MouseInfo.Update();
            KeyboardInfo.Update();

            foreach (BaseElement element in this.elements)
            {
                element.Update(gameTime);
            }

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

                if (MouseInfo.LeftMouseClicked)
                {
                    this.heldElement = this.GetDraggableElement();
                }
            }

            if (MouseInfo.LeftMouseDragged)
            {
                if (this.heldElement != null && this.heldElement.Draggable)
                {
                    Vector2 delta = MouseInfo.Position - MouseInfo.PreviousPosition;
                    Vector2 newPosition = this.heldElement.GetPosition() + delta;

                    this.heldElement?.Move(newPosition);
                }
            }

            if (MouseInfo.LeftMouseReleased)
            {
                this.heldElement = null;
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
        private List<BaseElement> GetAllElements(bool onlyVisibleElements = false)
        {
            List<BaseElement> result = new List<BaseElement>();

            foreach (BaseElement element in this.elements)
            {
                result.AddRange(element.BuildFlattenedSubTree(onlyVisibleElements));
            }

            return result;
        }

        /// <summary>
        /// Updates the hovered element.
        /// </summary>
        private void UpdateHoveredElement()
        {
            this.previousHoveredElement = this.hoveredElement;
            this.hoveredElement = this.GetHoveredElement();

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

        /// <summary>
        /// Gets the top draggable element that is currently hovered.
        /// </summary>
        private BaseElement GetDraggableElement()
        {
            List<BaseElement> blockers = this.GetAllElements(true).Where(element => element.Blocker).ToList();

            List<BaseElement> hoveredDraggableElements = this.GetAllElements(true)
                                                    .Where(element => element.Draggable && element.GetBounds().Contains(MouseInfo.Position))
                                                    .ToList();
            hoveredDraggableElements.Reverse(); // Ensures that elements rendered last are selected first when priorities are equal

            if (hoveredDraggableElements.Count <= 0)
            {
                return null;
            }

            BaseElement result = hoveredDraggableElements.OrderByDescending(element => element.Priority).First();

            if (!blockers.Any())
            {
                return result;
            }

            // Get the highest priority blocker and check whether the hovered element is that blocker or any of it's children
            BaseElement topBlocker = blockers.OrderByDescending(element => element.Priority).First();

            if (!topBlocker.BuildFlattenedSubTree(true).Contains(result))
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Get the currently hovered element.
        /// </summary>
        private BaseElement GetHoveredElement()
        {
            List<BaseElement> blockers = this.GetAllElements(true).Where(element => element.Blocker).ToList();

            List<BaseElement> hoveredElements = this.GetAllElements(true)
                                                    .Where(element => element.GetBounds().Contains(MouseInfo.Position))
                                                    .ToList();
            hoveredElements.Reverse(); // Ensures that elements rendered last are selected first when priorities are equal

            if (hoveredElements.Count <= 0)
            {
                return null;
            }

            BaseElement result = hoveredElements.OrderByDescending(element => element.Priority).First();

            if (!blockers.Any())
            {
                return result;
            }

            // Get the highest priority blocker and check whether the hovered element is that blocker or any of it's children
            BaseElement topBlocker = blockers.OrderByDescending(element => element.Priority).First();

            if (!topBlocker.BuildFlattenedSubTree(true).Contains(result))
            {
                result = null;
            }

            return result;
        }

        #endregion
    }
}
