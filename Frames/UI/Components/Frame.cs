namespace Frames.Structure.Components
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using Frames.DataStructures;
    using Frames.UI.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    /// <summary>
    /// A box with a background colour that can hold additional <see cref="BaseComponent"/> classes.
    /// </summary>
    public class Frame : BaseComponent
    {
        #region Fields

        private Texture2D texture;

        #endregion

        #region Constructors

        public Frame(Color color, int width, int height)
        {
            this.Color = color;
            this.Width = width;
            this.Height = height;
            this.Children = new List<BaseComponent>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color
        {
            get;
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get;
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get;
        }

        /// <summary>
        /// Gets the children components.
        /// </summary>
        public List<BaseComponent> Children
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
        /// Draws the component.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initialises the component.
        /// </summary>
        public override void Initialise()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
