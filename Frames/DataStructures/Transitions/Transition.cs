namespace Frames.DataStructures
{
    #region Usings

    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    #endregion

    #region Delegates

    public delegate void Callback(object data);

    #endregion

    public abstract class Transition
    {
        #region Fields

        protected double timeStart, timeElapsedSinceLastUpdate, totalElapsedTime;
        protected float progress;

        #endregion

        #region Constructors

        public Transition(int duration, Callback callback)
        {
            this.Duration = duration;
            this.Callback = callback;
        }

        #endregion

        #region Properties

        public bool Started
        {
            get;
            private set;
        }

        public bool Done
        {
            get;
            private set;
        }

        public int Duration
        {
            get;
            private set;
        }

        public Callback Callback
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public void Start(GameTime gameTime)
        {
            this.timeStart = gameTime.TotalGameTime.TotalMilliseconds;
            this.Started = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            this.timeElapsedSinceLastUpdate = gameTime.ElapsedGameTime.TotalMilliseconds;
            this.totalElapsedTime = gameTime.TotalGameTime.TotalMilliseconds - this.timeStart;
            this.progress = (float)this.totalElapsedTime / this.Duration;

            //TODO: is the progress == 0 check needed here?
            if (this.totalElapsedTime >= this.Duration)
            {
                this.Done = true;
                return;
            }
        }

        public void Restart()
        {
            this.Started = false;
            this.Done = false;
        }

        #endregion
    }
}
