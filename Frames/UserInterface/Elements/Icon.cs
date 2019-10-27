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

        private ImageGraphics imageGraphics;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="Icon"/> class.
        /// </summary>
        public Icon(string name, Texture2D texture, IPositionProfile positionProfile)
            : base (name, texture.Width, texture.Height, positionProfile)
        {
            this.Texture = texture;
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

        #endregion

        #region Methods

        /// <summary>
        /// Draws the element.
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            this.imageGraphics.Draw(spriteBatch);
        }

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
            this.BuildComponents(parent);
        }

        /// <summary>
        /// Builds the components.
        /// </summary>
        private void BuildComponents(Rectangle parent)
        {
            this.imageGraphics = new ImageGraphics(this.Texture, PositionFactory.CenteredRelative());
            this.imageGraphics.Initialise(parent);
        }

        #endregion

        #region Internal Interaction Handlers 

        /// <summary>
        /// The implementation details for the <see cref="Hover"/> method.
        /// </summary>
        protected override void HoverDetail()
        {
        }

        /// <summary>
        /// The implementation details for the <see cref="HoverLeave"/> method.
        /// </summary>
        protected override void HoverLeaveDetail()
        {
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
