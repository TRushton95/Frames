namespace Frames.UI.Components
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.PositionProfiles;
    using Frames.Enums;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    #endregion

    /// <summary>
    /// Simple text that can expand to a maximum width and then either wraps and shrinks based on a font flow. 
    /// </summary>
    public class TextGraphics : BaseComponent
    {
        #region Constructors


        /// <summary>
        /// Initialises a new instance of the <see cref="TextGraphics"/> class.
        /// </summary>
        public TextGraphics(string text, SpriteFont font, Color color, IPositionProfile positionProfile)
            : base(positionProfile)
        {
            this.Text = text;
            this.Font = font;
            this.Color = color;

            this.Scale = 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text
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
        /// Gets the maximum width.
        /// </summary>
        public int MaxWidth
        {
            get;
        }

        /// <summary>
        /// Gets the scale.
        /// </summary>
        public int Scale
        {
            get;
        }

        /// <summary>
        /// Gets the font flow.
        /// </summary>
        public FontFlow FontFlow
        {
            get;
        }

        /// <summary>
        /// Gets the justification.
        /// </summary>
        public Justify Justify
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
            Vector2 textDimensions = this.Font.MeasureString(this.Text);

            // return new Size(this.MaxWidth, (int)textDimensions.Y); // TODO: Use this once MaxWidth is fully implemented
            return new Size((int)textDimensions.X, (int)textDimensions.Y);
        }

        /// <summary>
        /// Draws the component.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.Font, this.Text, this.GetPosition(), this.Color, 0, default(Vector2), this.Scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Initialises the component.
        /// </summary>
        public override void Initialise(Rectangle parent)
        {
            this.UpdatePosition(parent);
        }

        #endregion
    }
}
