namespace Frames.DataStructures.Transitions
{
    #region Usings

    using Microsoft.Xna.Framework;

    #endregion

    public class MovementTransition : Transition
    {
        #region Fields

        private PositionProfile projectedCurrentProfile; // Adds the (x,y) distance required to move to the destination profile offset to arrive at correct anchor.

        #endregion

        #region Constructors

        public MovementTransition(Vector2 startPosition, Vector2 finalPosition, PositionProfile destinationProfile, int duration, Callback callback)
            : base(duration, callback)
        {
            this.StartPosition = startPosition;
            this.FinalPosition = finalPosition;
            this.DestinationProfile = destinationProfile;

            Vector2 delta = this.FinalPosition - this.StartPosition;

            this.projectedCurrentProfile = new PositionProfile(
                this.DestinationProfile.HorizontalAlign,
                this.DestinationProfile.VerticalAlign,
                this.DestinationProfile.OffsetX - (int)delta.X,
                this.DestinationProfile.OffsetY - (int)delta.Y);
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

            Vector2 totalPositionDelta = this.FinalPosition - this.StartPosition;
            Vector2 interpolatedPositionDelta = Vector2.Multiply(totalPositionDelta, this.progress);

            Vector2 offset = this.projectedCurrentProfile.Offset + interpolatedPositionDelta;
            PositionProfile data = new PositionProfile(this.DestinationProfile.HorizontalAlign, this.DestinationProfile.VerticalAlign, (int)offset.X, (int)offset.Y);

            this.Callback.Invoke(data);
        }

        #endregion
    }
}
