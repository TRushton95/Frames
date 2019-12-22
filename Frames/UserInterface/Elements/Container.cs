﻿namespace Frames.UserInterface.Elements
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.PositionProfiles;
    using Frames.UserInterface.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    #endregion

    public class Container : BaseElement
    {
        #region Fields

        private Frame frame;

        #endregion

        #region Constructors

        public Container(string name, int width, int height, Color color, IPositionProfile positionProfile, List<BaseElement> children)
            : base(name, width, height, positionProfile)
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
        /// Draws the element.
        /// </summary>
        public override Size GetSize()
        {
            return new Size(this.Width, this.Height);
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            frame.Draw(spriteBatch);

            foreach (BaseElement child in this.Children)
            {
                child.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Recursively searches the element tree and creates a flat list of the element and its children if it has any.
        /// </summary>
        public override List<BaseElement> BuildFlattenedSubTree()
        {
            List<BaseElement> result = new List<BaseElement> { this };
            result.AddRange(this.Children);

            return result;
        }

        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected override void InternalInitialise(Rectangle parent)
        {
            this.SetPosition(parent); // TODO: Could this be refactored into the base Initialise() call
            this.BuildComponents();

            foreach (BaseElement child in this.Children)
            {
                child.Initialise(this.GetBounds(), this.Priority + 1);
            }
        }

        /// <summary>
        /// Builds the components.
        /// </summary>
        private void BuildComponents()
        {
            this.frame = new Frame(this.Width, this.Height, this.Color, this.PositionProfile);
            this.frame.Initialise(this.GetBounds());
        }

        #endregion
    }
}
