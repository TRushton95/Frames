namespace Frames.UserInterface.Components
{
    #region Usings

    using Frames.DataStructures;
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
        public ImageGraphics(Texture2D texture, PositionProfile positionProfile)
            : base(positionProfile)
        {
            this.Texture = texture;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the image.
        /// </summary>
        public Texture2D Texture
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
            return new Size(this.Texture.Width, this.Texture.Height);
        }

        /// <summary>
        /// Draws the component.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.GetBounds(), Color.White);
        }

        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected override void InternalInitialise(Rectangle parent)
        {
            this.SetPosition(parent);
        }

        #endregion
    }
}
