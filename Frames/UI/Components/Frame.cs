namespace Frames.Structure.Components
{
    #region Usings

    using System.Collections.Generic;
    using Frames.DataStructures;
    using Frames.DataStructures.PositionProfiles;
    using Frames.UI.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Resources;

    #endregion

    /// <summary>
    /// A box with a background colour that can hold additional <see cref="BaseComponent"/> classes.
    /// </summary>
    public class Frame : BaseComponent
    {
        #region Fields

        private Texture2D texture;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises an instance of the <see cref="Frame"/> class.
        /// </summary>
        public Frame(int width, int height, Color color, IPositionProfile positionProfile)
            : base(positionProfile)
        {
            this.Width = width;
            this.Height = height;
            this.Color = color;

            this.Children = new List<BaseComponent>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get;
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
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
        /// Gets the children components.
        /// </summary>
        public List<BaseComponent> Children
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
        /// Draws the component.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.GetPosition(), Color.White);

            foreach (BaseComponent child in this.Children)
            {
                child.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Initialises the component.
        /// </summary>
        protected override void InternalInitialise(Rectangle parent)
        {
            this.UpdatePosition(parent);

            this.texture = this.BuildTexture();

            foreach (BaseComponent child in this.Children)
            {
                child.Initialise(this.GetBounds());
            }
        }

        private Texture2D BuildTexture()
        {
            Texture2D result = new Texture2D(Resources.Instance.GraphicsDevice, this.Width, this.Height);

            Color[] data = new Color[this.Width * this.Height];
            for (int pixel = 0; pixel < data.Length; pixel++)
            {
                data[pixel] = this.Color;
            }

            result.SetData(data);


            return result;
        }

        #endregion
    }
}
