namespace Frames.DataStructures
{
    #region Usings

    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    #endregion

    #region Delegates

    public delegate void Callback(object data);
    public delegate void OnFinish();

    #endregion

    public abstract class Transition
    {
        #region Fields

        protected double timeStart, timeElapsedSinceLastUpdate, totalElapsedTime;

        #endregion

        #region Constructors

        public Transition(Callback callback)
        {
            this.Callback = callback;
        }

        #endregion

        #region Properties

        public bool Started
        {
            get;
            set;
        }

        public bool Done
        {
            get;
            set;
        }

        public Callback Callback
        {
            get;
            set;
        }

        public OnFinish OnFinish
        {
            get;
            set;
        }

        public bool Ready => !this.Started && !this.Done;

        public bool IsRunning => this.Started && !this.Done;

        #endregion

        #region Methods

        public void Start(GameTime gameTime)
        {
            this.Started = true;
            this.timeStart = gameTime.TotalGameTime.TotalMilliseconds;
        }

        public void Stop()
        {
            this.OnFinish?.Invoke();
        }

        public void Update(GameTime gameTime)
        {
            if (this.Done)
            {
                return;
            }

            this.timeElapsedSinceLastUpdate = gameTime.ElapsedGameTime.TotalMilliseconds;
            this.totalElapsedTime = gameTime.TotalGameTime.TotalMilliseconds - this.timeStart;

            this.InternalUpdate(gameTime);
        }

        protected virtual void InternalUpdate(GameTime gameTime) { }

        #endregion
    }
}
