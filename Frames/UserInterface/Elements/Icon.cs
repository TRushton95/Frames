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

    public class Icon : BaseElement
    {
        #region Fields

        private ImageGraphics imageGraphics;

        #endregion

        #region Constructors

        public Icon(string name, Texture2D texture, IPositionProfile positionProfile)
            : base (name, texture.Width, texture.Height, positionProfile)
        {
            this.Texture = texture;
        }

        #endregion

        #region Properties

        public Texture2D Texture
        {
            get;
        }

        #endregion

        #region Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.imageGraphics.Draw(spriteBatch);
        }

        public override Size GetSize()
        {
            return new Size(this.Width, this.Height);
        }

        protected override void InternalInitialise(Rectangle parent)
        {
            this.BuildComponents(parent);
        }

        private void BuildComponents(Rectangle parent)
        {
            this.imageGraphics = new ImageGraphics(this.Texture, PositionFactory.CenteredRelative());
            this.imageGraphics.Initialise(parent);
        }

        #endregion

        #region Internal Interaction Handlers 

        protected override void HoverDetail()
        {
        }

        protected override void HoverLeaveDetail()
        {
        }

        protected override void LeftClickDetail()
        {
        }

        #endregion


    }
}
