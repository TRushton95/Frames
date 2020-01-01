namespace Frames.UserInterface.Elements
{
    #region Usings

    using Frames.DataStructures;
    using Frames.Factories;
    using Frames.UserInterface.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The container element that defines a background frame and can hold children elements.
    /// </summary>
    public class Container : BaseElement
    {
        #region Fields

        private Frame frame;

        #endregion

        #region Constructors

        public Container(string name, int width, int height, Border border, Color color, PositionProfile positionProfile, List<BaseElement> children)
            : base(name, width, height, border, positionProfile)
        {
            this.Color = color;
            this.Children = children;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public List<BaseElement> Children
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the size.
        /// </summary>
        public override Size GetSize()
        {
            return new Size(this.Width, this.Height);
        }

        /// <summary>
        /// Sets the position of the element and composite components.
        /// </summary>
        public override void SetPosition(Rectangle parentBounds)
        {
            base.SetPosition(parentBounds);

            this.frame?.SetPosition(this.GetBounds());
            this.Children?.ForEach(child => child.SetPosition(this.GetBounds()));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (BaseElement child in this.Children)
            {
                child.Update(gameTime);
            }
        }

        /// <summary>
        /// Recursively searches the element tree and creates a flat list of the element and its children if it has any.
        /// </summary>
        /// <remarks>
        /// TODO: This could result in a stack overflow error
        /// </remarks>
        public override List<BaseElement> BuildFlattenedSubTree(bool onlyVisibleElements)
        {
            List<BaseElement> result = new List<BaseElement> { };

            if (onlyVisibleElements && !this.Visible)
            {
                return result;
            }

            result.Add(this);
            foreach (BaseElement child in this.Children)
            {
                result.AddRange(child.BuildFlattenedSubTree(onlyVisibleElements));
            }

            return result;
        }

        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected override void InternalInitialise()
        {
            this.BuildComponents();

            foreach (BaseElement child in this.Children)
            {
                child.Initialise(this.GetBounds(), this.Priority + 1);
            }
        }

        /// <summary>
        /// Draws the element.
        /// </summary>
        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            this.frame.Draw(spriteBatch);

            foreach (BaseElement child in this.Children)
            {
                child.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Builds the components.
        /// </summary>
        private void BuildComponents()
        {
            this.frame = new Frame(this.Width, this.Height, this.Color, PositionFactory.Center(), this.Border);
            this.frame.Initialise(this.GetBounds());
        }

        #endregion
    }
}
