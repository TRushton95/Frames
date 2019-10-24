namespace Frames.UI.Elements
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.PositionProfiles;
    using Frames.Factories;
    using Frames.Structure.Components;
    using Frames.UI.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    /// <summary>
    /// The button element.
    /// </summary>
    public class Button : BaseElement
    {
        #region Fields

        private Frame frame, defaultFrame, hoverFrame;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises an instance of the <see cref="Button"/> class.
        /// </summary>
        public Button(int width, int height, IPositionProfile positionProfile, string text, SpriteFont font, Color frameColor, Color textColor, Color frameHoverColor, Color textHoverColor)
            : base(width, height, positionProfile)
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
        /// Draws the element.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            frame.Draw(spriteBatch);
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public override Size GetSize()
        {
            return new Size(this.Width, this.Height);
        }

        /// <summary>
        /// Initialises the element.
        /// </summary>
        public override void Initialise(Rectangle parent)
        {
            this.UpdatePosition(parent);
            this.BuildComponents();
        }
        
        /// <summary>
        /// Builds the components.
        /// </summary>
        private void BuildComponents()
        {
            defaultFrame = new Frame(this.Width, this.Height, this.FrameColor, PositionFactory.CenteredRelative());
            TextGraphics defaultTextGraphics = new TextGraphics(this.Text, this.Font, this.TextColor, PositionFactory.CenteredRelative());
            defaultFrame.Children.Add(defaultTextGraphics);
            defaultFrame.Initialise(this.GetBounds());


            hoverFrame = new Frame(this.Width, this.Height, this.FrameHoverColor, PositionFactory.CenteredRelative());
            TextGraphics hoverTextGraphics = new TextGraphics(this.Text, this.Font, this.TextHoverColor, PositionFactory.CenteredRelative());
            hoverFrame.Children.Add(hoverTextGraphics);
            hoverFrame.Initialise(this.GetBounds());

            frame = defaultFrame;
        }

        #endregion
    }
}
