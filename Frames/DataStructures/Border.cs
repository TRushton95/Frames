namespace Frames.DataStructures
{
    #region Usings

    using Microsoft.Xna.Framework;

    #endregion

    /// <summary>
    /// Represents a border for a ui element.
    /// </summary>
    public class Border
    {
        #region Constants

        private const int DefaultWidth = 2;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a default border.
        /// </summary>
        public static Border Default => new Border
        {
            Width = DefaultWidth,
            Color = Color.Black
        };

        public static Border None => new Border
        {
            Width = 0,
            Color = Color.Transparent
        };

        #endregion
    }
}
