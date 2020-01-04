namespace Frames.DataStructures.Transitions
{
    #region Usings

    using Microsoft.Xna.Framework;

    #endregion

    public class ColorTransition : Transition
    {
        #region Fields

        private float progress;

        #endregion

        #region Constructors

        public ColorTransition(
            Vector4 startColor,
            Vector4 endColor,
            int duration,
            Callback callback)
            : base(callback)
        {
            this.Duration = duration;
            this.StartColor = startColor;
            this.EndColor = endColor;
        }

        #endregion

        #region Properties

        public int Duration
        {
            get;
            private set;
        }

        public Vector4 StartColor
        {
            get;
            private set;
        }

        public Vector4 EndColor
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        protected override void InternalUpdate(GameTime gameTime)
        {
            this.progress = (float)this.totalElapsedTime / this.Duration;

            if (this.totalElapsedTime >= this.Duration)
            {
                this.Done = true;
                return;
            }

            Vector4 colorDelta = this.EndColor - this.StartColor;
            Vector4 interpolatedColorDelta = Vector4.Multiply(colorDelta, this.progress);
            Vector4 currentColor = this.StartColor + interpolatedColorDelta;
            Color data = new Color(currentColor);

            this.Callback.Invoke(data);
        }

        #endregion
    }
}
