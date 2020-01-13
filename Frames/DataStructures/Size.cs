namespace Frames.DataStructures
{
    #region Usings

    using Microsoft.Xna.Framework;

    #endregion

    /// <summary>
    /// Stores a pair of integers which represent width and height.
    /// </summary>
    public struct Size
    {
        #region Constructor

        /// <summary>
        /// Initialises a new instance of the <see cref="Size"/> structure from the specified dimensions.
        /// </summary>
        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        /// <summary>
        /// Initialises a new instance of the <see cref="Size"/> structure from the specified dimensions.
        /// </summary>
        public Size(Vector2 dimensions)
        {
            this.Width = (int)dimensions.X;
            this.Height = (int)dimensions.Y;
        }

        #region Properties

        /// <summary>
        /// The width.
        /// </summary>
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// The height.
        /// </summary>
        public int Height
        {
            get;
            set;
        }

        #endregion


        #endregion
    }
}
