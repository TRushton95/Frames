namespace Frames.Factories
{
    #region Usings

    using Frames.DataStructures;
    using Frames.Enums;
    using Microsoft.Xna.Framework;

    #endregion

    /// <summary>
    /// Creates preset <see cref="PositionProfile"/> implementations.
    /// </summary>
    public static class PositionFactory
    {
        #region Relative Position

        /// <summary>
        /// Creates a position profile anchored to the top left.
        /// </summary>
        public static PositionProfile TopLeft()
        {
            return new PositionProfile(HorizontalAlign.Left, VerticalAlign.Top, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the top center.
        /// </summary>
        public static PositionProfile TopCenter()
        {
            return new PositionProfile(HorizontalAlign.Middle, VerticalAlign.Top, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the top right.
        /// </summary>
        public static PositionProfile TopRight()
        {
            return new PositionProfile(HorizontalAlign.Right, VerticalAlign.Top, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the center left.
        /// </summary>
        public static PositionProfile CenterLeft()
        {
            return new PositionProfile(HorizontalAlign.Left, VerticalAlign.Middle, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the center.
        /// </summary>
        public static PositionProfile Center()
        {
            return new PositionProfile(HorizontalAlign.Middle, VerticalAlign.Middle, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the center right.
        /// </summary>
        public static PositionProfile CenterRight()
        {
            return new PositionProfile(HorizontalAlign.Right, VerticalAlign.Middle, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the bottom left.
        /// </summary>
        public static PositionProfile BottomLeft()
        {
            return new PositionProfile(HorizontalAlign.Left, VerticalAlign.Bottom, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the bottom center.
        /// </summary>
        public static PositionProfile BottomCenter()
        {
            return new PositionProfile(HorizontalAlign.Middle, VerticalAlign.Bottom, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the bottom right.
        /// </summary>
        public static PositionProfile BottomRight()
        {
            return new PositionProfile(HorizontalAlign.Right, VerticalAlign.Bottom, 0, 0);
        }

        /// <summary>
        /// Creates a position profile that is absolutely positioned by its (x,y) coordinates.
        /// </summary>
        public static PositionProfile Absolute(Vector2 position)
        {
            return new PositionProfile(HorizontalAlign.Left, VerticalAlign.Top, (int)position.X, (int)position.Y);
        }

        #endregion
    }
}
