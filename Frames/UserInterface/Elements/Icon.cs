﻿namespace Frames.UserInterface.Elements
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
            this.SetPosition(parent);
            this.BuildComponents();
        }

        /// <summary>
        /// Builds the components.
        /// </summary>
        private void BuildComponents()
        {
            this.defaultImageGraphics = new ImageGraphics(this.Texture, PositionFactory.CenteredRelative());
            this.defaultImageGraphics.Initialise(this.GetBounds());

            if (this.hoverImageGraphics != null)
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
