namespace Frames.DataStructures.Transitions
{
    #region Usings

    using Microsoft.Xna.Framework;

    #endregion

    public class ColorTransition : Transition
    {
        #region Constructors

        public ColorTransition(Vector4 startColor, Vector4 endColor, int duration, Callback callback)
            : base(duration, callback)
        {
            this.StartColor = startColor;
            this.EndColor = endColor;
        }

        #endregion

        #region Properties

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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector4 colorDelta = this.EndColor - this.StartColor;
            Vector4 interpolatedColorDelta = Vector4.Multiply(colorDelta, this.progress);
            Vector4 currentColor = this.StartColor + interpolatedColorDelta;
            Color data = new Color(currentColor);

            this.Callback.Invoke(data);
        }

        #endregion
    }
}
