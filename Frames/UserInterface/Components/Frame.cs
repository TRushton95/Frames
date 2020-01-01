namespace Frames.UserInterface.Components
{
    #region Usings

    using System.Collections.Generic;
    using Frames.DataStructures;
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
        public Frame(int width, int height, Color color, PositionProfile positionProfile, Border border = null)
            : base(positionProfile)
        {
            this.Width = width;
            this.Height = height;
            this.Color = color;
            this.Border = border;
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
        /// Gets the border.
        /// </summary>
        public Border Border
        {
            get;
        }

        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color
        {
            get;
            private set;
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
        }

        public void SetColor(Color color)
        {
            this.Color = color;
            this.texture = this.BuildTexture();
        }

        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected override void InternalInitialise(Rectangle parent)
        {
            this.SetPosition(parent);
            this.texture = this.BuildTexture();
        }

        /// <summary>
        /// Builds the texture
        /// </summary>
        /// <returns>TODO: This method exists as a test and elements do not compensate content with for borders</returns>
        private Texture2D BuildTexture()
        {
            Texture2D result = new Texture2D(Resources.Instance.GraphicsDevice, this.Width, this.Height);

            Color[] data = new Color[this.Width * this.Height];

            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    Color color = this.Color;

                    if (this.IsPixelInBorder(x, y))
                    {
                        color = Color.Black;
                    }

                    int index = (y * this.Width) + x;
                    data[index] = color;
                }
            }

            result.SetData(data);

            return result;
        }

        private bool IsPixelInBorder(int x, int y)
        {
            if (this.Border == null)
            {
                return false;
            }

            return
                y < this.Border.Width ||
                y >= this.Height - this.Border.Width ||
                x < this.Border.Width ||
                x >= this.Width - this.Border.Width;
        }

        #endregion
    }
}
