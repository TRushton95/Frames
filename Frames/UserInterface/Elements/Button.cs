namespace Frames.UserInterface.Elements
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.Transitions;
    using Frames.Enums;
    using Frames.Factories;
    using Frames.UserInterface.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    /// <summary>
    /// The button element.
    /// </summary>
    public class Button : BaseElement
    {
        #region Constants

        /// <summary>
        /// The gutter placed between the edge of the text and the button.
        /// </summary>
        /// <remarks>Consider placing this in a variable and passed in.</remarks>
        private const int Gutter = 10;

        #endregion

        #region Fields

        private Frame frame;
        private TextGraphics textGraphics;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises an instance of the <see cref="Button"/> class.
        /// </summary>
        public Button(string name, int width, int height, Border border, PositionProfile positionProfile, string text, SpriteFont font, Color frameColor, Color textColor, Color frameHoverColor, Color textHoverColor)
            : base(name, width, height, border, positionProfile)
        {
            this.Text = text;
            this.Font = font;
            this.FrameColor = frameColor;
            this.TextColor = textColor;
            this.FrameHoverColor = frameHoverColor;
            this.TextHoverColor = textHoverColor;
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
        /// Gets the frame color.
        /// </summary>
        public Color FrameColor
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color TextColor
        {
            get;
        }

        /// <summary>
        /// Gets the frame color when hovered.
        /// </summary>
        public Color FrameHoverColor
        {
            get;
        }

        /// <summary>
        /// Gets the text color when hovered.
        /// </summary>
        public Color TextHoverColor
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
        /// Sets the position of the element and composite components.
        /// </summary>
        public override void SetPosition(Rectangle parentBounds)
        {
            base.SetPosition(parentBounds);

            this.frame?.SetPosition(this.GetBounds());
            this.textGraphics?.SetPosition(this.GetBounds());
        }

        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected override void InternalInitialise()
        {
            this.BuildComponents();
        }

        /// <summary>
        /// Draws the element.
        /// </summary>
        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            this.frame.Draw(spriteBatch);
            this.textGraphics.Draw(spriteBatch);
        }

        /// <summary>
        /// Builds the components.
        /// </summary>
        private void BuildComponents()
        {
            this.frame = new Frame(this.Width, this.Height, this.FrameColor, PositionFactory.Center(), this.Border);
            this.frame.Initialise(this.GetBounds());
            this.textGraphics = new TextGraphics(this.Text, this.Font, this.TextColor, this.Width - (Gutter * 2), FontFlow.Scale, PositionFactory.Center());
            this.textGraphics.Initialise(this.GetContentBounds());
        }

        #endregion

        #region Internal Interaction Handlers

        /// <summary>
        /// The implementation details for the <see cref="Hover"/> method.
        /// </summary>
        protected override void HoverDetail()
        {
            if (this.FrameHoverColor != null)
            {
                ColorTransition backgroundTransition = new ColorTransition(this.frame.Color.ToVector4(), this.FrameHoverColor.ToVector4(), 200, SetBackgroundColor);
                this.activeTransitions.Add(backgroundTransition);
            }

            if (this.TextHoverColor != null)
            {
                ColorTransition textTransition = new ColorTransition(this.textGraphics.Color.ToVector4(), this.TextHoverColor.ToVector4(), 200, SetTextColor);
                this.activeTransitions.Add(textTransition);
            }
        }

        /// <summary>
        /// The implementation details for the <see cref="HoverLeave"/> method.
        /// </summary>
        protected override void HoverLeaveDetail()
        {
            if (this.FrameHoverColor != null)
            {
                ColorTransition backgroundTransition = new ColorTransition(this.frame.Color.ToVector4(), this.FrameColor.ToVector4(), 200, SetBackgroundColor);
                this.activeTransitions.Add(backgroundTransition);
            }

            if (this.TextHoverColor != null)
            {
                ColorTransition textTransition = new ColorTransition(this.textGraphics.Color.ToVector4(), this.TextColor.ToVector4(), 200, SetTextColor);
                this.activeTransitions.Add(textTransition);
            }
        }

        /// <summary>
        /// The implementation details for the <see cref="LeftClick"/> method.
        /// </summary>
        protected override void LeftClickDetail()
        {
        }

        #endregion

        #region Transition Callbacks

        public void SetBackgroundColor(object color)
        {
            this.frame.SetColor((Color)color);
        }

        public void SetTextColor(object color)
        {
            this.textGraphics.SetColor((Color)color);
        }

        #endregion
    }
}
