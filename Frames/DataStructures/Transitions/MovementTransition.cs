namespace Frames.DataStructures.Transitions
{
    #region Usings

    using Microsoft.Xna.Framework;
    using System;

    #endregion

    public class MovementTransition : Transition
    {
        #region Fields

        private float runtime; // In seconds
        private PositionProfile projectedCurrentProfile; // Adds the (x,y) distance required to move to the destination profile offset to arrive at correct anchor.

        #endregion

        #region Constructors

        public MovementTransition(
            Vector2 startPosition,
            Vector2 finalPosition,
            PositionProfile destinationProfile,
            float distancePerSeconds,
            Callback callback)
            : base(callback)
        {
            this.StartPosition = startPosition;
            this.FinalPosition = finalPosition;
            this.DestinationProfile = destinationProfile;
            this.Speed = distancePerSeconds;

            Vector2 delta = this.FinalPosition - this.StartPosition;

            float distance = (float)Math.Sqrt((delta.X * delta.X) + (delta.Y * delta.Y));
            this.runtime = distance / this.Speed;

            this.projectedCurrentProfile = new PositionProfile(
                this.DestinationProfile.HorizontalAlign,
                this.DestinationProfile.VerticalAlign,
                this.DestinationProfile.OffsetX - (int)delta.X,
                this.DestinationProfile.OffsetY - (int)delta.Y);
        }

        #endregion

        #region Properties

        public float Speed
        {
            get;
        }

        public Vector2 StartPosition
        {
            get;
        }

        public Vector2 FinalPosition
        {
            get;
        }

        public PositionProfile DestinationProfile
        {
            get;
        }

        #endregion

        #region Methods

        protected override void InternalUpdate(GameTime gameTime)
        {
            if (this.totalElapsedTime >= this.runtime * 1000)
            {
                this.Done = true;
                this.Callback.Invoke(this.DestinationProfile);

                return;
            }

            Vector2 delta = this.FinalPosition - this.StartPosition;
            Vector2 speedComponents = delta / this.runtime;

            float elapsedSeconds = (float)this.totalElapsedTime / 1000;
            Vector2 offset = this.projectedCurrentProfile.Offset + Vector2.Multiply(speedComponents, elapsedSeconds);

            PositionProfile data = new PositionProfile(this.DestinationProfile.HorizontalAlign, this.DestinationProfile.VerticalAlign, (int)offset.X, (int)offset.Y);

            this.Callback.Invoke(data);
        }

        #endregion
    }
}
