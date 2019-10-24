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
        public TextGraphics(string text, SpriteFont font, Color color, int maxWidth, IPositionProfile positionProfile)
            : base(positionProfile)
        {
            this.Text = text;
            this.Font = font;
            this.Color = color;
            this.MaxWidth = maxWidth;
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
        /// Gets or sets the scale.
        /// </summary>
        public float Scale
        {
            get;
            private set;
        } = 1;

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
            return new Size(
                (int)(textDimensions.X * this.Scale),
                (int)(textDimensions.Y * this.Scale)
            );
        }

        /// <summary>
        /// Draws the component.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.Font, this.Text, this.GetPosition(), this.Color, 0, default(Vector2), this.Scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected override void InternalInitialise(Rectangle parent)
        {
            this.InitialiseFontFlow();
            this.UpdatePosition(parent);
        }

        /// <summary>
        /// Initialises how the text handles exceeding its maximum width.
        /// </summary>
        private void InitialiseFontFlow()
        {
            // TODO: Alter based on font flow
            this.ScaleText();
        }

        /// <summary>
        /// Scales the text size down if it exceeds the maximum width.
        /// </summary>
        private void ScaleText()
        {
            if (this.MaxWidth == 0) // TODO: This should be removed, it's here for low effort initialising of components for testing.
            {
                return;
            }

            int width = this.GetSize().Width;

            if (width > this.MaxWidth)
            {
                this.Scale = (float)this.MaxWidth / width;
            }
            else
            {
                this.Scale = 1;
            }
        }

        #endregion
    }
}
