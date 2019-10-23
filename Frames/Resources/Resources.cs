namespace Frames.Resources
{
    #region Usings

    using Microsoft.Xna.Framework.Graphics;

    #endregion

    public class Resources
    {
        #region Fields

        private static Resources _instance;

        private GraphicsDevice graphicsDevice;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static Resources Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Resources();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Gets or sets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice => graphicsDevice;

        #endregion

        #region Methods

        /// <summary>
        /// Initialise the resources.
        /// </summary>
        public void Initialise(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        #endregion
    }
}
