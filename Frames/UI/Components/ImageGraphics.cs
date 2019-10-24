namespace Frames.Structure.Components
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.PositionProfiles;
    using Frames.UI.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    /// <summary>
    /// An image that scales to the provided dimensions.
    /// </summary>
    public class ImageGraphics : BaseComponent
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="ImageGraphics"/> class.
        /// </summary>
        public ImageGraphics(Texture2D image, IPositionProfile positionProfile)
            : base(positionProfile)
        {
            this.Image = image;
            this.Width = image.Width;
            this.Height = image.Height;
        }

        #endregion

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
            spriteBatch.Draw(this.Image, this.GetBounds(), Color.White);
        }

        /// <summary>
        /// Initialises the component.
        /// </summary>
        protected override void InternalInitialise(Rectangle parent)
        {
            this.UpdatePosition(parent);
        }

        #endregion
    }
}
