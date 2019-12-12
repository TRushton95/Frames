﻿namespace Frames.UserInterface.Elements
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.PositionProfiles;
    using Frames.Enums;
    using Frames.Factories;
    using Frames.UserInterface.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Resources;

    #endregion

    public class Textbox : BaseElement
    {
        #region Constants

        private const int Gutter = 20;

        #endregion

        #region Fields

        private Frame frame;
        private TextGraphics textGraphics;
        private RasterizerState rasterizerState;
        private int scrollHeight;

        #endregion

        #region Constructors

        public Textbox(string name, string text, SpriteFont font, int width, int height, IPositionProfile positionProfile)
            : base(name, width, height, positionProfile)
        {
            this.Text = text;
            this.Font = font;
        }

        #endregion

        #region Properties

        public string Text
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the font.
        /// </summary>
        public SpriteFont Font
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draws the element.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            RasterizerState origRasterizerState = Resources.Instance.GraphicsDevice.RasterizerState;
            Rectangle origScissorRect = spriteBatch.GraphicsDevice.ScissorRectangle;

            spriteBatch.End();
            spriteBatch.GraphicsDevice.ScissorRectangle = this.GetBounds();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, this.rasterizerState);
            this.frame.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.GraphicsDevice.ScissorRectangle = origScissorRect;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, origRasterizerState);
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
            this.SetPosition(parent); // TODO: Could this be refactored into the base Initialise() call
            this.BuildComponents();
            this.rasterizerState = new RasterizerState();
            rasterizerState.ScissorTestEnable = true;
            this.scrollHeight = this.GetBounds().Y;
        }

        /// <summary>
        /// Builds the components.
        /// </summary>
        private void BuildComponents()
        {
            this.frame = new Frame(this.Width, this.Height, Color.Cyan, PositionFactory.CenteredRelative());
            this.textGraphics = new TextGraphics(this.Text, this.Font, Color.Yellow, this.Width - (Gutter * 2), FontFlow.Wrap, PositionFactory.TopCenterRelative());
            frame.Children.Add(this.textGraphics);
            this.frame.Initialise(this.GetBounds());
        }

        #endregion

        #region Internal Interaction Handlers


        /// <summary>
        /// The implementation details for the <see cref="MouseWheelScrollUp"/> method.
        /// </summary>
        protected override void MouseWheelScrollUpDetail()
        {
            scrollHeight += 50;

            if (scrollHeight >= 0)
            {
                scrollHeight = 0;
            }

            this.SetTextOffsetY(scrollHeight);
        }

        /// <summary>
        /// The implementation details for the <see cref="MouseWheelScrollDown"/> method.
        /// </summary>
        protected override void MouseWheelScrollDownDetail()
        {
            scrollHeight -= 50;
            if (scrollHeight <= -this.textGraphics.GetBounds().Height + this.Height)
            {
                scrollHeight = -this.textGraphics.GetBounds().Height + this.Height;
            }

            this.SetTextOffsetY(scrollHeight);
        }

        private void SetTextOffsetY(int offsety)
        {
            RelativePositionProfile positionProfile = PositionFactory.TopCenterRelative();
            positionProfile.OffsetY = offsety;
            this.textGraphics.PositionProfile = positionProfile;
            this.frame.Initialise(this.GetBounds());
        }

        #endregion
    }
}
