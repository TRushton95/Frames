﻿namespace Frames.UserInterface.Elements
{
    #region Usings

    using Frames.DataStructures.PositionProfiles;
    using Frames.Events.EventSystem;
    using Frames.EventSystem;
    using Frames.UserInterface.Components;
    using IronPython.Hosting;
    using Microsoft.Scripting.Hosting;
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using System.IO;

    #endregion

    /// <summary>
    /// The base structure of a user interface element.
    /// </summary>
    public abstract class BaseElement : BaseComponent
    {
        /// <summary>
        /// Initialises an instance of the <see cref="BaseElement"/> class.
        /// </summary>
        public BaseElement(string name, int width, int height, IPositionProfile positionProfile)
            : base(positionProfile)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
        }

        #region Properties

        /// <summary>
        /// Gets the name of the property.
        /// This is used for linking an element with its corresponding script file.
        /// </summary>
        public string Name
        {
            get;
        }

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
        }

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
        /// Sets the priority of the element and its children if it has any.
        /// </summary>
        public virtual void SetPriority(int priority)
        {
            this.Priority = priority;
        }

        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        /// <remarks>TODO: This has a very untidy integration with the inheritence of BaseComponent. See if this can be refactored.</remarks>
        protected override void InternalInitialise(Rectangle parent)
        {
            this.ExecuteScript();
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

        #endregion

        #region Interactions

        /// <summary>
        /// The mouse hover handler.
        /// </summary>
        public void Hover()
        {
            this.Hovered = true;
            this.HoverDetail();

            this.eventManager.Notify(new Event(EventTypes.Frames.ElementHover, null));
        }

        /// <summary>
        /// The mouse hover leave handler.
        /// </summary>
        public void HoverLeave()
        {
            this.Hovered = false;
            this.HoverLeaveDetail();

            this.eventManager.Notify(new Event(EventTypes.Frames.ElementHoverLeave, null));
        }

        /// <summary>
        /// The left click handler.
        /// </summary>
        public void LeftClick()
        {
            this.LeftClickDetail();

            this.eventManager.Notify(new Event(EventTypes.Frames.ElementClick, null));
        }

        #endregion

        #region Internal Interaction Handlers

        /// <summary>
        /// The implementation details for the <see cref="LeftClick"/> method.
        /// </summary>
        protected abstract void LeftClickDetail();

        /// <summary>
        /// The implementation details for the <see cref="Hover"/> method.
        /// </summary>
        protected abstract void HoverDetail();

        /// <summary>
        /// The implementation details for the <see cref="HoverLeave"/> method.
        /// </summary>
        protected abstract void HoverLeaveDetail();

        #endregion
    }
}
