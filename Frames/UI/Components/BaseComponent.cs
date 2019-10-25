namespace Frames.UI.Components
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.PositionProfiles;
    using Frames.EventSystem;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    /// <summary>
    /// The base structure of the component that is used to form elements.
    /// </summary>
    public abstract class BaseComponent : Listener
    {
        #region Fields

        private bool initialised = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises the default instance of the <see cref="BaseComponent"/> class.
        /// </summary>
        public BaseComponent()
        {
        }

        /// <summary>
        /// Initialises an instance of the <see cref="BaseComponent"/> class.
        /// </summary>
        public BaseComponent(IPositionProfile positionProfile)
        {
            this.PositionProfile = positionProfile;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the x coordinate.
        /// </summary>
        public int X
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the y coordinate.
        /// </summary>
        public int Y
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the position profile.
        /// </summary>
        public IPositionProfile PositionProfile
        {
            get;
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Gets the position.
        /// </summary>
        public Vector2 GetPosition()
        {
            return new Vector2(this.X, this.Y);
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public abstract Size GetSize();

        /// <summary>
        /// Gets the position and size as a rectangle.
        /// </summary>
        public Rectangle GetBounds()
        {
            Size size = this.GetSize();

            return new Rectangle(this.X, this.Y, size.Width, size.Height);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the position.
        /// </summary>
        public void SetPosition(Rectangle parentBounds)
        {
            Vector2 position = this.PositionProfile.CalculatePosition(parentBounds, this.GetSize());

            this.X = (int)position.X;
            this.Y = (int)position.Y;
        }

        /// <summary>
        /// Draws the component.
        /// </summary>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Initialises the component if not already initialised.
        /// </summary>
        public void Initialise(Rectangle parent)
        {
            if (this.initialised)
            {
                return;
            }

            this.InternalInitialise(parent);
            this.initialised = true;
        }

        #endregion

        #region Internal Implementations

        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected abstract void InternalInitialise(Rectangle parent);

        #endregion
    }
}
