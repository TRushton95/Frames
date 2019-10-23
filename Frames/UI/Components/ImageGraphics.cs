namespace Frames.Structure.Components
{
    #region Usings

    using Frames.DataStructures;
    using Frames.UI.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    #endregion

    /// <summary>
    /// An image that scales to the provided dimensions.
    /// </summary>
    public class ImageGraphics : BaseComponent
    {
        #region Properties

        /// <summary>
        /// Gets the height.
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
        /// Gets the image.
        /// </summary>
        public Texture2D Image
        {
            get;
        }

        /// <summary>
        /// Gets the colour filter.
        /// </summary>
        public Color Filter
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
        public override void Initialise(Rectangle parent)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
