namespace Frames.UI.Components
{
    #region Usings

    using Frames.DataStructures;
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

            return new Size(this.MaxWidth, (int)textDimensions.Y);
        }

        /// <summary>
        /// Draws the component.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initialises the component.
        /// </summary>
        public override void Initialise(Rectangle parent)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
