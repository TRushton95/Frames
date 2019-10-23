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

    public class Button : BaseElement
    {
        #region Fields

        private Frame frame, defaultFrame, hoverFrame;
        private TextGraphics defaultTextGraphics, hoverTextGraphics;

        #endregion

        #region Constructors

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

        public override void Draw(SpriteBatch spriteBatch)
        {
            frame.Draw(spriteBatch);
        }

        public override Size GetSize()
        {
            return new Size(this.Width, this.Height);
        }

        public override void Initialise(Rectangle parent)
        {
            this.UpdatePosition(parent);
            this.BuildComponents();
        }

        private void BuildComponents()
        {
            defaultFrame = new Frame(this.Width, this.Height, this.FrameColor, PositionFactory.CenteredRelative());
            defaultTextGraphics = new TextGraphics(this.Text, this.Font, this.TextColor, PositionFactory.CenteredRelative());
            defaultFrame.Children.Add(defaultTextGraphics);
            defaultFrame.Initialise(this.GetBounds());


            hoverFrame = new Frame(this.Width, this.Height, this.FrameHoverColor, PositionFactory.CenteredRelative());
            hoverTextGraphics = new TextGraphics(this.Text, this.Font, this.TextHoverColor, PositionFactory.CenteredRelative());
            hoverFrame.Children.Add(hoverTextGraphics);
            hoverFrame.Initialise(this.GetBounds());

            frame = defaultFrame;
        }

        #endregion
    }
}
