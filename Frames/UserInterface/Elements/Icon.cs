namespace Frames.UserInterface.Elements
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.PositionProfiles;
    using Frames.Factories;
    using Frames.UserInterface.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    /// <summary>
    /// The icon element.
    /// </summary>
    public class Icon : BaseElement
    {
        #region Fields

        private ImageGraphics imageGraphics, defaultImageGraphics, hoverImageGraphics;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="Icon"/> class.
        /// </summary>
        public Icon(string name, Border border, IPositionProfile positionProfile, Texture2D texture, Texture2D hoverTexture = null)
            : base (name, texture.Width, texture.Height, border, positionProfile)
        {
            this.Texture = texture;
            this.HoverTexture = hoverTexture;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the texture.
        /// </summary>
        public Texture2D Texture
        {
            get;
        }

        /// <summary>
        /// Gets the texture to use when hovered.
        /// </summary>
        public Texture2D HoverTexture
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
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected override void InternalInitialise(Rectangle parent)
        {
            this.SetPosition(parent);
            this.BuildComponents();
        }

        /// <summary>
        /// Draws the element.
        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            this.imageGraphics.Draw(spriteBatch);
        }

        /// <summary>
        /// Builds the components.
        /// </summary>
        private void BuildComponents()
        {
            this.defaultImageGraphics = new ImageGraphics(this.Texture, PositionFactory.CenteredRelative());
            this.defaultImageGraphics.Initialise(this.GetBounds());

            if (this.HoverTexture != null)
            {
                this.hoverImageGraphics = new ImageGraphics(this.HoverTexture, PositionFactory.CenteredRelative());
                this.hoverImageGraphics.Initialise(this.GetBounds());
            }

            this.imageGraphics = this.defaultImageGraphics;
        }

        #endregion

        #region Internal Interaction Handlers 

        /// <summary>
        /// The implementation details for the <see cref="Hover"/> method.
        /// </summary>
        protected override void HoverDetail()
        {
            if (this.hoverImageGraphics != null)
            {
                this.imageGraphics = this.hoverImageGraphics;
            }
        }

        /// <summary>
        /// The implementation details for the <see cref="HoverLeave"/> method.
        /// </summary>
        protected override void HoverLeaveDetail()
        {
            if (this.hoverImageGraphics != null) // This isn't necessary, just convention
            {
                this.imageGraphics = this.defaultImageGraphics;
            }
        }

        /// <summary>
        /// The implementation details for the <see cref="LeftClick"/> method.
        /// </summary>
        protected override void LeftClickDetail()
        {
        }

        #endregion


    }
}
