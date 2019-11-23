namespace Frames.UserInterface.Elements
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
        #region Fields

        private Frame frame;
        private TextGraphics textGraphics;
        private RasterizerState rasterizerState;

        #endregion

        #region Constructors

        public Textbox(string name, string text, SpriteFont font, int width, int height, IPositionProfile positionProfile)
            : base(name, width, height, positionProfile)
        {
            this.Text = text;
            this.Font = font;
        }

        #endregion

        #region Methods

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

        /*
        public override void Update()
        {
            if (MouseInfo.LeftMouseClicked)
            {
                this.scrollHeight -= 10;
            }
            
            if (MouseInfo.RightMouseClicked)
            {
                this.scrollHeight += 10;
            }
        }
        */

        /// <summary>
        /// Draws the element.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            RasterizerState origRasterizerState = Resources.Instance.GraphicsDevice.RasterizerState;
            Rectangle origScissorRect = spriteBatch.GraphicsDevice.ScissorRectangle;

            spriteBatch.End();
            spriteBatch.GraphicsDevice.ScissorRectangle = this.GetBounds();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, this.rasterizerState);
            this.frame.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.GraphicsDevice.ScissorRectangle = origScissorRect;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, origRasterizerState);

            // this.frame.Draw(spriteBatch);
        }

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
        }

        /// <summary>
        /// Builds the components.
        /// </summary>
        private void BuildComponents()
        {
            this.frame = new Frame(this.Width, this.Height, Color.Cyan, PositionFactory.CenteredRelative());
            this.textGraphics = new TextGraphics(this.Text, this.Font, Color.Yellow, this.Width, FontFlow.Wrap, PositionFactory.TopCenterRelative());
            frame.Children.Add(this.textGraphics);
            this.frame.Initialise(this.GetBounds());
        }

        #endregion

        #region Internal Interaction Handlers

        /// <summary>
        /// The implementation details for the <see cref="Hover"/> method.
        /// </summary>
        protected override void HoverDetail()
        {
        }

        /// <summary>
        /// The implementation details for the <see cref="HoverLeave"/> method.
        /// </summary>
        protected override void HoverLeaveDetail()
        {
        }

        /// <summary>
        /// The implementation details for the <see cref="LeftClick"/> method.
        /// </summary>
        protected override void LeftClickDetail()
        {
        }

        #endregion
    }
}
