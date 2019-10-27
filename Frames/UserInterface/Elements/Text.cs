﻿namespace Frames.UserInterface.Elements
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.PositionProfiles;
    using Frames.Enums;
    using Frames.Events.EventSystem;
    using Frames.UserInterface.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    public class Text : BaseElement
    {
        #region Fields

        private TextGraphics textGraphics;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="Text"/> class.
        /// </summary>
        public Text(string name, string value, SpriteFont font, Color color, int maxWidth, FontFlow fontFlow, IPositionProfile positionProfile)
            : base(name, 0, 0, positionProfile)
        {
            this.Value = value;
            this.Font = font;
            this.Color = color;
            this.MaxWidth = maxWidth;
            this.FontFlow = fontFlow;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the text value.
        /// </summary>
        public string Value
        {
            get;
        }

        /// <summary>
        /// Gets the font.
        /// </summary>
        public SpriteFont Font
        {
            get;
        }

        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color
        {
            get;
        }

        /// <summary>
        /// Gets the max width. Use default value of 0 for no width restrictions.
        /// </summary>
        public int MaxWidth
        {
            get;
        } = 0;

        /// <summary>
        /// Gets the font flow.
        /// </summary>
        public FontFlow FontFlow
        {
            get;
        } = FontFlow.None;

        #endregion

        #region Methods

        /// <summary>
        /// Draws the element.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.Visible)
            {
                this.textGraphics.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public override Size GetSize()
        {
            Vector2 textDimensions = this.Font.MeasureString(this.Value);

            return new Size(this.MaxWidth, (int)textDimensions.Y);
        }

        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected override void InternalInitialise(Rectangle parent)
        {
            this.SetPosition(parent); // TODO: Could this be refactored into the base Initialise() call
            this.BuildComponents();
        }

        public void ToggleVisibility(Event e)
        {
            this.Visible = !this.Visible;
        }

        /// <summary>
        /// Builds the components.
        /// </summary>
        private void BuildComponents()
        {
            textGraphics = new TextGraphics(this.Value, this.Font, this.Color, this.MaxWidth, this.FontFlow, this.PositionProfile);
            textGraphics.Initialise(this.GetBounds());
        }

        #endregion

        #region Internal Interaction Handlers

        /// <summary>
        /// The implementation details for the <see cref="Hover"/> method.
        /// </summary>
        protected override void HoverDetail()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The implementation details for the <see cref="HoverLeave"/> method.
        /// </summary>
        protected override void HoverLeaveDetail()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The implementation details for the <see cref="LeftClick"/> method.
        /// </summary>
        protected override void LeftClickDetail()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
