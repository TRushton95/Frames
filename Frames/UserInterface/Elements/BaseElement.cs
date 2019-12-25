namespace Frames.UserInterface.Elements
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.PositionProfiles;
    using Frames.Events.EventSystem;
    using Frames.EventSystem;
    using IronPython.Hosting;
    using Microsoft.Scripting.Hosting;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.IO;

    #endregion

    /// <summary>
    /// The base structure of a user interface element.
    /// </summary>
    public abstract class BaseElement : BaseBody
    {
        #region Fields

        private Logger logger = LogManager.GetCurrentClassLogger(); 

        #endregion

        /// <summary>
        /// Initialises an instance of the <see cref="BaseElement"/> class.
        /// </summary>
        public BaseElement(string name, int width, int height, Border border, IPositionProfile positionProfile)
            : base(positionProfile)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Border = border == null ? Border.Default : border;
        }

        #region Properties

        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get;
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get;
        }

        /// <summary>
        /// Gets the border.
        /// </summary>
        public Border Border
        {
            get;
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        public int Priority
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the element is visible.
        /// </summary>
        public bool Visible
        {
            get;
            set;
        } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the element is interactive.
        /// </summary>
        public bool Interactive
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the element blocks the ui.
        /// </summary>
        public bool Blocker
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the element is hovered.
        /// </summary>
        public bool Hovered
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Recursively searches the element tree and creates a flat list of the element and its children if it has any.
        /// </summary>
        public virtual List<BaseElement> BuildFlattenedSubTree()
        {
            return new List<BaseElement> { this };
        }

        /// <summary>
        /// Initialise the element.
        /// </summary>
        public void Initialise(Rectangle parent, int priority)
        {
            this.Priority = priority;
            this.logger.Debug($"{this.GetType().Name} - {this.Priority}");

            this.ExecuteScript();
            this.InternalInitialise(parent);
        }

        /// <summary>
        /// Executes the corresponding python script, if it exists.
        /// </summary>
        private void ExecuteScript()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                return;
            }

            string scriptFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", $"{this.Name}.py");

            if (!File.Exists(scriptFilePath))
            {
                return;
            }

            ScriptEngine scriptEngine = Python.CreateEngine();
            ScriptScope scope = scriptEngine.CreateScope();

            ScriptSource source = scriptEngine.CreateScriptSourceFromFile(scriptFilePath);
            scope.SetVariable("this", this);
            source.Execute(scope);
        }

        /// <summary>
        /// Draws the element.
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.Visible)
            {
                this.InternalDraw(spriteBatch);
            }
        }

        #endregion

        #region Internal Implementations

        protected abstract void InternalDraw(SpriteBatch spriteBatch);


        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected abstract void InternalInitialise(Rectangle parent);

        #endregion

        #region Interactions

        /// <summary>
        /// The mouse hover handler.
        /// </summary>
        public void Hover()
        {
            this.Hovered = true;
            this.HoverDetail();

            this.eventManager.Notify(new Event(this.Name, EventTypeConstants.Frames.ElementHover, null));
        }

        /// <summary>
        /// The mouse hover leave handler.
        /// </summary>
        public void HoverLeave()
        {
            this.Hovered = false;
            this.HoverLeaveDetail();

            this.eventManager.Notify(new Event(this.Name, EventTypeConstants.Frames.ElementHoverLeave, null));
        }

        /// <summary>
        /// The left click handler.
        /// </summary>
        public void LeftClick()
        {
            this.LeftClickDetail();

            this.eventManager.Notify(new Event(this.Name, EventTypeConstants.Frames.ElementClick, null));
        }

        /// <summary>
        /// The mouse wheel scroll up handler.
        /// </summary>
        public void MouseWheelScrollUp()
        {
            this.MouseWheelScrollUpDetail();

            this.eventManager.Notify(new Event(this.Name, EventTypeConstants.Frames.ElementMouseWheelScrollUp, null));
        }

        /// <summary>
        /// The mouse wheel scroll down handler.
        /// </summary>
        public void MouseWheelScrollDown()
        {
            this.MouseWheelScrollDownDetail();

            this.eventManager.Notify(new Event(this.Name, EventTypeConstants.Frames.ElementMouseWheelScrollDown, null));
        }

        #endregion

        #region Internal Interaction Handlers

        /// <summary>
        /// The implementation details for the <see cref="LeftClick"/> method.
        /// </summary>
        protected virtual void LeftClickDetail() { }

        /// <summary>
        /// The implementation details for the <see cref="Hover"/> method.
        /// </summary>
        protected virtual void HoverDetail() { }

        /// <summary>
        /// The implementation details for the <see cref="HoverLeave"/> method.
        /// </summary>
        protected virtual void HoverLeaveDetail() { }

        /// <summary>
        /// The implementation details for the <see cref="MouseWheelScrollUp"/> method.
        /// </summary>
        protected virtual void MouseWheelScrollUpDetail() { }

        /// <summary>
        /// The implementation details for the <see cref="MouseWheelScrollDown"/> method.
        /// </summary>
        protected virtual void MouseWheelScrollDownDetail() { }

        #endregion

        #region Api

        public void Show(Event e)
        {
            this.Visible = true;
        }

        public void Hide(Event e)
        {
            this.Visible = false;
        }

        public void ToggleVisibility(Event e)
        {
            this.Visible = !this.Visible;
        }

        #endregion
    }
}
