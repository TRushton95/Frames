namespace Frames.DataStructures.Transitions
{
    #region Usings

    using Microsoft.Xna.Framework;

    #endregion

    public class MovementTransition : Transition
    {
        #region Constructors

        public MovementTransition(Vector2 startPosition, Vector2 finalPosition, int duration, Callback callback)
            : base(duration, callback)
        {
            this.StartPosition = startPosition;
            this.FinalPosition = finalPosition;
        }

        #endregion

        #region Properties

        public Vector2 StartPosition
        {
            get;
            set;
        }

        public Vector2 FinalPosition
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            double updateRatio = this.timeElapsedSinceLastUpdate / this.Duration;
            Vector2 positionDelta = this.FinalPosition - this.StartPosition;
            double deltaX = positionDelta.X * updateRatio;
            double deltaY = positionDelta.Y * updateRatio;
            Vector2 positionStep = new Vector2((int)deltaX, (int)deltaY);

            this.Callback.Invoke(positionStep);
        }

        #endregion
    }
}
