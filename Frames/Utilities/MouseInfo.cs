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
        /// Gets the previous position of the mouse.
        /// </summary>
        public static Vector2 PreviousPosition => previousState.Position.ToVector2();

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
        /// <remarks>
        /// TODO: Misnoma - not a click, it's pressed
        /// </remarks>
        public static bool LeftMouseClicked => currentState.LeftButton == ButtonState.Pressed &&
                                                previousState.LeftButton == ButtonState.Released;

        /// <summary>
        /// Checks if the right mouse button has just been clicked.
        /// </summary>
        /// <remarks>
        /// TODO: Misnoma - not a click, it's pressed
        /// </remarks>
        public static bool RightMouseClicked => currentState.LeftButton == ButtonState.Pressed &&
                                                previousState.LeftButton == ButtonState.Released;

        /// <summary>
        /// Checks if the right mouse button has just been released.
        /// </summary>
        public static bool RightMouseReleased => currentState.LeftButton == ButtonState.Released &&
                                                previousState.LeftButton == ButtonState.Pressed;
        /// <summary>
        /// Checks if the left mouse button has just been released.
        /// </summary>
        public static bool LeftMouseReleased => currentState.LeftButton == ButtonState.Released &&
                                                previousState.LeftButton == ButtonState.Pressed;

        /// <summary>
        /// Checks if the left mouse has been held.
        /// </summary>
        public static bool LeftMouseHeld => currentState.LeftButton == ButtonState.Pressed &&
                                            previousState.LeftButton == ButtonState.Pressed;

        /// <summary>
        /// Checks if the right mouse has been held.
        /// </summary>
        public static bool RightMouseHeld => currentState.RightButton == ButtonState.Pressed &&
                                            previousState.RightButton == ButtonState.Pressed;

        /// <summary>
        /// Checks if the mouse has been moved.
        /// </summary>
        public static bool Moved => currentState.Position != previousState.Position;

        /// <summary>
        /// Checks if the left mouse has been dragged.
        /// </summary>
        public static bool LeftMouseDragged => LeftMouseHeld && Moved;

        /// <summary>
        /// Checks if the mouse wheel has just been scrolled up.
        /// </summary>
        public static bool MouseWheelScrolledUp => currentState.ScrollWheelValue > previousState.ScrollWheelValue;

        /// <summary>
        /// Checks if the mouse wheel has just been scrolled down.
        /// </summary>
        public static bool MouseWheelScrolledDown => currentState.ScrollWheelValue < previousState.ScrollWheelValue;

        #endregion
    }
}
