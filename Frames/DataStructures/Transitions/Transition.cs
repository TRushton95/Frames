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
            this.Done = false;
        }

        public void Update(GameTime gameTime)
        {
            this.timeElapsedSinceLastUpdate = gameTime.ElapsedGameTime.TotalMilliseconds;
            this.totalElapsedTime = gameTime.TotalGameTime.TotalMilliseconds - this.timeStart;

            this.InternalUpdate(gameTime);
        }

        protected virtual void InternalUpdate(GameTime gameTime) { }

        #endregion
    }
}
