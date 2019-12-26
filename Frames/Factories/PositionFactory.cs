namespace Frames.Factories
{
    #region Usings

    using Frames.DataStructures.PositionProfiles;
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
        public static PositionProfile TopLeftRelative()
        {
            return new PositionProfile(HorizontalAlign.Left, VerticalAlign.Top, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the top center.
        /// </summary>
        public static PositionProfile TopCenterRelative()
        {
            return new PositionProfile(HorizontalAlign.Middle, VerticalAlign.Top, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the top right.
        /// </summary>
        public static PositionProfile TopRightRelative()
        {
            return new PositionProfile(HorizontalAlign.Right, VerticalAlign.Top, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the center left.
        /// </summary>
        public static PositionProfile CenterLeftRelative()
        {
            return new PositionProfile(HorizontalAlign.Left, VerticalAlign.Middle, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the center.
        /// </summary>
        public static PositionProfile CenteredRelative()
        {
            return new PositionProfile(HorizontalAlign.Middle, VerticalAlign.Middle, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the center right.
        /// </summary>
        public static PositionProfile CenterRightRelative()
        {
            return new PositionProfile(HorizontalAlign.Right, VerticalAlign.Middle, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the bottom left.
        /// </summary>
        public static PositionProfile BottomLeftRelative()
        {
            return new PositionProfile(HorizontalAlign.Left, VerticalAlign.Bottom, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the bottom center.
        /// </summary>
        public static PositionProfile BottomCenterRelative()
        {
            return new PositionProfile(HorizontalAlign.Middle, VerticalAlign.Bottom, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the bottom right.
        /// </summary>
        public static PositionProfile BottomRightRelative()
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
