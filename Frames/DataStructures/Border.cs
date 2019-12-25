namespace Frames.DataStructures
{
    #region Usings

    using Microsoft.Xna.Framework;

    #endregion

    public class Border
    {
        #region Constants

        private const int DefaultWidth = 2;

        #endregion

        #region Properties

        public int Width
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        public static Border Default => new Border
        {
            Width = DefaultWidth,
            Color = Color.Black
        };

        #endregion
    }
}
