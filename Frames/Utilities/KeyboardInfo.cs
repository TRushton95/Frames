namespace Frames.Utilities
{
    #region Usings

    using Microsoft.Xna.Framework.Input;

    #endregion

    /// <summary>
    /// A wrapper class for <see cref="KeyboardState"/> that provides easy access to the changing of mouse states.
    /// </summary>
    public static class KeyboardInfo
    {
        #region Fields

        private static KeyboardState currentState, previousState;

        #endregion

        #region Methods

        /// <summary>
        /// Updates the keyboard state.
        /// </summary>
        public static void Update()
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }

        /// <summary>
        /// Gets a value indicating whether the key is down.
        /// </summary>
        public static bool IsKeyDown(Keys key)
        {
            return currentState.IsKeyDown(key);
        }

        /// <summary>
        /// Gets a value indicating whether the key has just been pressed.
        /// </summary>
        public static bool IsKeyPressed(Keys key)
        {
            return currentState.IsKeyDown(key) && !previousState.IsKeyDown(key);
        }

        #endregion
    }
}
