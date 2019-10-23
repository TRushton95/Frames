namespace Frames.Factories
{
    #region Usings

    using Frames.DataStructures.PositionProfiles;
    using Frames.Enums;

    #endregion

    /// <summary>
    /// Creates preset <see cref="IPositionProfile"/> implementations.
    /// </summary>
    public static class PositionFactory
    {
        #region Relative Position

        /// <summary>
        /// Creates a position profile anchored to the top left.
        /// </summary>
        public static RelativePositionProfile TopLeftRelative()
        {
            return new RelativePositionProfile(HorizontalAlign.Left, VerticalAlign.Top, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the top center.
        /// </summary>
        public static RelativePositionProfile TopCenterRelative()
        {
            return new RelativePositionProfile(HorizontalAlign.Middle, VerticalAlign.Top, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the top right.
        /// </summary>
        public static RelativePositionProfile TopRightRelative()
        {
            return new RelativePositionProfile(HorizontalAlign.Right, VerticalAlign.Top, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the center left.
        /// </summary>
        public static RelativePositionProfile CenterLeftRelative()
        {
            return new RelativePositionProfile(HorizontalAlign.Left, VerticalAlign.Middle, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the center.
        /// </summary>
        public static RelativePositionProfile CenteredRelative()
        {
            return new RelativePositionProfile(HorizontalAlign.Middle, VerticalAlign.Middle, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the center right.
        /// </summary>
        public static RelativePositionProfile CenterRightRelative()
        {
            return new RelativePositionProfile(HorizontalAlign.Right, VerticalAlign.Middle, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the bottom left.
        /// </summary>
        public static RelativePositionProfile BottomLeftRelative()
        {
            return new RelativePositionProfile(HorizontalAlign.Left, VerticalAlign.Bottom, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the bottom center.
        /// </summary>
        public static RelativePositionProfile BottomCenterRelative()
        {
            return new RelativePositionProfile(HorizontalAlign.Middle, VerticalAlign.Bottom, 0, 0);
        }

        /// <summary>
        /// Creates a position profile anchored to the bottom right.
        /// </summary>
        public static RelativePositionProfile BottomRightRelative()
        {
            return new RelativePositionProfile(HorizontalAlign.Right, VerticalAlign.Bottom, 0, 0);
        }

        #endregion
    }
}
