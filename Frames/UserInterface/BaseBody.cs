﻿namespace Frames.UserInterface
{
    #region Usings

    using Frames.DataStructures;
    using Frames.EventSystem;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    /// <summary>
    /// The base structure of a any composite part of user interface that has dimensions and can be displayed.
    /// </summary>
    /// <remarks>TODO: This will work for now, but the seprataion between elements and component could probably be cleaner/></remarks>
    public abstract class BaseBody : Listener
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BaseBody"/> class.
        /// </summary>
        public BaseBody(PositionProfile positionProfile)
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
        public PositionProfile PositionProfile
        {
            get;
            set;
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
        public virtual void SetPosition(Rectangle parentBounds)
        {
            Vector2 position = this.PositionProfile.CalculatePosition(parentBounds, this.GetSize());

            this.X = (int)position.X;
            this.Y = (int)position.Y;
        }

        /// <summary>
        /// Draws the body.
        /// </summary>
        public abstract void Draw(SpriteBatch spriteBatch);

        #endregion
    }
}
