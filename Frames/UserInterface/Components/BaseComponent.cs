namespace Frames.UserInterface.Components
{
    #region Usings

    using Frames.DataStructures;
    using Microsoft.Xna.Framework;

    #endregion

    /// <summary>
    /// The base structure of the component that is used to form elements.
    /// </summary>
    public abstract class BaseComponent : BaseBody
    {
        #region Fields

        private bool initialised = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BaseComponent"/> class.
        /// </summary>
        public BaseComponent(PositionProfile positionProfile)
            : base(positionProfile)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialises the component if not already initialised.
        /// </summary>
        public void Initialise(Rectangle parent)
        {
            if (this.initialised)
            {
                //TODO: Should this be reworked? This needed to be removed to allow for position reinitialisation of textboxes when scrolled.
                //return;
            }

            this.InternalInitialise(parent);
            this.initialised = true;
        }

        #endregion

        #region Internal Implementations

        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected abstract void InternalInitialise(Rectangle parent);

        #endregion
    }
}
