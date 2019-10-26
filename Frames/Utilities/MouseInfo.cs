namespace Frames.Utilities
{
    #region Usings

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    #endregion

    /// <summary>
    /// A wrapper class for <see cref="MouseState"/> that provides easy access to the changing of mouse states.
    /// </summary>
    public static class MouseInfo
    {
        #region Fields

        private static MouseState currentState, previousState;

        #endregion

        #region Methods

        /// <summary>
        /// Updates the mouse state.
        /// </summary>
        public static void Update()
        {
            previousState = currentState;
            currentState = Mouse.GetState();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the x position of the mouse.
        /// </summary>

        public static int X => currentState.X;

        /// <summary>
        /// Gets the y position of the mouse.
        /// </summary>
        public static int Y => currentState.Y;

        /// <summary>
        /// Gets the position of the mouse.
        /// </summary>
        public static Vector2 Position => currentState.Position.ToVector2();

        /// <summary>
        /// Gets a value indicating whether the left mouse button is currently down.
        /// </summary>

        public static bool LeftMouseDown => currentState.LeftButton == ButtonState.Pressed;

        /// <summary>
        /// Gets a value indicating whether the right mouse button is currently down.
        /// </summary>
        public static bool RightMouseDown => currentState.RightButton == ButtonState.Pressed;

        /// <summary>
        /// Checks if the left mouse button has just been clicked.
        /// </summary>
        public static bool LeftMouseClicked => currentState.LeftButton == ButtonState.Pressed &&
                                                previousState.LeftButton == ButtonState.Released;

        /// <summary>
        /// Checks if the right mouse button has just been clicked.
        /// </summary>
        public static bool RightMouseClicked => currentState.LeftButton == ButtonState.Pressed &&
                                                previousState.LeftButton == ButtonState.Released;

        #endregion
    }
}
