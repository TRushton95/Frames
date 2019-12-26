namespace Frames.DataStructures.PositionProfiles
{
    #region Usings

    using Frames.Enums;
    using Microsoft.Xna.Framework;

    #endregion

    /// <summary>
    /// Defines how a UI component should be positioned relative to its parent.
    /// </summary>
    public class PositionProfile
    {
        #region Constructors

        /// <summary>
        /// Initialises an instance of the <see cref="PositionProfile"/> class.
        /// </summary>
        public PositionProfile(HorizontalAlign horizontalAlign, VerticalAlign verticalAlign, int offsetX, int offsetY)
        {
            this.HorizontalAlign = horizontalAlign;
            this.VerticalAlign = verticalAlign;
            this.OffsetX = offsetX;
            this.OffsetY = offsetY;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the horizontal alignment relative to the parent component.
        /// </summary>
        public HorizontalAlign HorizontalAlign
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the horizontal alignment relative to the parent component.
        /// </summary>
        public VerticalAlign VerticalAlign
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the horizontal offset from the anchor point defined by <see cref="HorizontalAlign"/>.
        /// </summary>
        public int OffsetX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the vertical offset from the anchor point defined by <see cref="VerticalAlign"/>.
        /// </summary>
        public int OffsetY
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calculates the position of the component.
        /// </summary>
        public Vector2 CalculatePosition(Rectangle parentBounds, Size componentSize)
        {
            int resultX = parentBounds.X + OffsetX;

            switch (HorizontalAlign)
            {
                case HorizontalAlign.Left:
                    break;

                case HorizontalAlign.Middle:
                    resultX += (parentBounds.Width / 2) - (componentSize.Width / 2);
                    break;

                case HorizontalAlign.Right:
                    resultX += parentBounds.Width - componentSize.Width;
                    break;
            }

            int resultY = parentBounds.Y + OffsetY;

            switch (VerticalAlign)
            {
                case VerticalAlign.Top:
                    break;

                case VerticalAlign.Middle:
                    resultY += (parentBounds.Height / 2) - (componentSize.Height / 2);
                    break;

                case VerticalAlign.Bottom:
                    resultY += parentBounds.Height - componentSize.Height;
                    break;
            }

            return new Vector2(resultX, resultY);
        }

        #endregion
    }
}
