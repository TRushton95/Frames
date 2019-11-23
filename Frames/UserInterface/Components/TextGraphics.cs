namespace Frames.UserInterface.Components
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.PositionProfiles;
    using Frames.Enums;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Text;

    #endregion

    /// <summary>
    /// Simple text that can expand to a maximum width and then either wraps and shrinks based on a font flow. 
    /// </summary>
    public class TextGraphics : BaseComponent
    {
        #region Fields

        private string displayText;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="TextGraphics"/> class.
        /// </summary>
        public TextGraphics(string text, SpriteFont font, Color color, int maxWidth, FontFlow fontFlow, IPositionProfile positionProfile)
            : base(positionProfile)
        {
            this.Text = text;
            this.Font = font;
            this.Color = color;
            this.MaxWidth = maxWidth;
            this.FontFlow = fontFlow;

            this.displayText = text;
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

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        public float Scale
        {
            get;
            private set;
        } = 1;

        #endregion

        #region Methods

        /// <summary>
        /// Gets the size.
        /// </summary>
        public override Size GetSize()
        {
            Vector2 textDimensions = this.Font.MeasureString(this.Text);
            textDimensions = Vector2.Multiply(textDimensions, this.Scale);

            return new Size(this.MaxWidth, (int)textDimensions.Y);
        }

        /// <summary>
        /// Draws the component.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.Font, this.displayText, this.GetPosition(), this.Color, 0, default(Vector2), this.Scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected override void InternalInitialise(Rectangle parent)
        {
            this.InitialiseFontFlow();
            this.SetPosition(parent);
        }

        /// <summary>
        /// Initialises how the text handles exceeding its maximum width.
        /// </summary>
        private void InitialiseFontFlow()
        {
            switch (FontFlow)
            {
                case FontFlow.Scale:
                    this.ScaleText();
                    break;

                case FontFlow.Wrap:
                    this.WrapText();
                    break;

                case FontFlow.None:
                    break;
            }
        }

        #endregion

        #region FontFlow Methods

        /// <summary>
        /// Scales the text size down if it exceeds the maximum width.
        /// </summary>
        private void ScaleText()
        {
            int width = (int)this.Font.MeasureString(this.Text).X;

            if (width > this.MaxWidth)
            {
                this.Scale = (float)this.MaxWidth / width;
            }
            else
            {
                this.Scale = 1;
            }
        }

        /// <summary>
        /// Rewrites the display text to wrap if it exceeds the maximum width.
        /// </summary>
        private void WrapText()
        {
            string[] words = this.Text.Split(' ');

            StringBuilder result = new StringBuilder();
            StringBuilder line = new StringBuilder();

            foreach (string word in words)
            {
                if (this.Font.MeasureString(line).X + this.Font.MeasureString(word).X > this.MaxWidth)
                {
                    result.Append(line);
                    result.Append("\n");

                    line.Clear();
                    line.Append(word);

                    continue;
                }

                if (!string.IsNullOrEmpty(line.ToString()))
                {
                    line.Append(" ");
                }

                line.Append(word);
            }

            if (!string.IsNullOrEmpty(line.ToString()))
            {
                result.Append(line);
            }

            this.displayText = result.ToString();
        }

        #endregion
    }
}
