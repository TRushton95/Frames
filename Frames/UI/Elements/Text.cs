namespace Frames.UI.Elements
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.PositionProfiles;
    using Frames.Enums;
    using Frames.UI.Components;
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
        public Text(string value, SpriteFont font, Color color, int maxWidth, FontFlow fontFlow, IPositionProfile positionProfile)
            : base(0, 0, positionProfile)
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
            this.textGraphics.Draw(spriteBatch);
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public override Size GetSize()
        {
            Vector2 textDimensions = this.Font.MeasureString(this.Value);

            // return new Size(this.MaxWidth, (int)textDimensions.Y); // TODO: Use this once MaxWidth is fully implemented
            return new Size(textDimensions);
        }

        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected override void InternalInitialise(Rectangle parent)
        {
            this.UpdatePosition(parent);
            this.BuildComponents();
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
        public override void HoverDetail()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The implementation details for the <see cref="HoverLeave"/> method.
        /// </summary>
        public override void HoverLeaveDetail()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The implementation details for the <see cref="LeftClick"/> method.
        /// </summary>
        public override void LeftClickDetail()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
