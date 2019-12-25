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

        public Textbox(string name, string text, SpriteFont font, int width, int height, Border border, IPositionProfile positionProfile)
            : base(name, width, height, border, positionProfile)
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
        /// Draws the element.
        /// </summary>
        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            this.frame.Draw(spriteBatch);
            RasterizerState origRasterizerState = Resources.Instance.GraphicsDevice.RasterizerState;
            Rectangle origScissorRect = spriteBatch.GraphicsDevice.ScissorRectangle;

            spriteBatch.End();
            spriteBatch.GraphicsDevice.ScissorRectangle = this.GetContentBounds();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, this.rasterizerState);
            this.textGraphics.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.GraphicsDevice.ScissorRectangle = origScissorRect;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, origRasterizerState);
        }

        /// <summary>
        /// Builds the components.
        /// </summary>
        private void BuildComponents()
        {
            int textMaxWidth = this.Width - (Gutter * 2);
            if (this.Border != null && this.Border.Width > 0)
            {
                textMaxWidth -= this.Border.Width * 2;
            }

            this.frame = new Frame(this.Width, this.Height, Color.Cyan, PositionFactory.CenteredRelative(), this.Border);
            this.frame.Initialise(this.GetBounds());
            this.textGraphics = new TextGraphics(this.Text, this.Font, Color.Yellow, textMaxWidth, FontFlow.Wrap, PositionFactory.TopCenterRelative());
            this.textGraphics.Initialise(this.GetContentBounds());
        }

        /// <summary>
        /// Gets the boundaries of the content within the frame border.
        /// </summary>
        private Rectangle GetContentBounds()
        {
            if (this.Border == null)
            {
                return this.GetBounds();
            }

            int x = this.X + this.Border.Width;
            int y = this.Y + this.Border.Width;
            int width = this.Width - (this.Border.Width * 2);
            int height = this.Height - (this.Border.Width * 2);

            return new Rectangle(x, y, width, height);
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
            this.textGraphics.Initialise(this.GetContentBounds());
        }

        #endregion
    }
}
