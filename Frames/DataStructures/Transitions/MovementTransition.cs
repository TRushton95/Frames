namespace Frames.DataStructures.Transitions
{
    #region Usings

    using Microsoft.Xna.Framework;

    #endregion

    public class MovementTransition : Transition
    {
        #region Fields

        private PositionProfile transitionProfile; // Adds the (x,y) distance required to move to the destination profile offset to arrive at correct anchor.

        #endregion

        #region Constructors

        public MovementTransition(Vector2 startPosition, Vector2 finalPosition, PositionProfile destinationProfile, int duration, Callback callback)
            : base(duration, callback)
        {
            this.StartPosition = startPosition;
            this.FinalPosition = finalPosition;
            this.DestinationProfile = destinationProfile;

            Vector2 delta = this.FinalPosition - this.StartPosition;
            this.transitionProfile = this.DestinationProfile;
            this.transitionProfile.Offset += delta;
            this.transitionProfile.Offset = Vector2.Multiply(this.transitionProfile.Offset, -1f);
        }

        #endregion

        #region Properties

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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            double updateRatio = this.totalElapsedTime / this.Duration;
            Vector2 totalPositionDelta = this.FinalPosition - this.StartPosition;
            Vector2 interpolatedPositionDelta = Vector2.Multiply(totalPositionDelta, (float)updateRatio);

            Vector2 offset = this.transitionProfile.Offset + interpolatedPositionDelta;
            PositionProfile data = new PositionProfile(this.DestinationProfile.HorizontalAlign, this.DestinationProfile.VerticalAlign, (int)offset.X, (int)offset.Y);

            System.Diagnostics.Debug.WriteLine($"({interpolatedPositionDelta.X},{interpolatedPositionDelta.Y}) => ({data.OffsetX},{data.OffsetY})");

            this.Callback.Invoke(data);
        }

        #endregion
    }
}
