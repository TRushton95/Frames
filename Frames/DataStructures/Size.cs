namespace Frames.DataStructures
{
    /// <summary>
    /// Stores a pair of integers which represent width and height.
    /// </summary>
    public struct Size
    {
        #region Fields

        /// <summary>
        /// The width.
        /// </summary>
        public int Width;

        /// <summary>
        /// The height.
        /// </summary>
        public int Height;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialises a new instance of the <see cref="Size"/> structure from the specified dimensions.
        /// </summary>
        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        #endregion
    }
}
