namespace Frames.UserInterface.Elements
{
    #region Usings

    using Frames.DataStructures;
    using Frames.DataStructures.Transitions;
    using Frames.Enums;
    using Frames.Events.EventSystem;
    using Frames.EventSystem;
    using Frames.Factories;
    using IronPython.Hosting;
    using Microsoft.Scripting.Hosting;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Timers;

    #endregion

    /// <summary>
    /// The base structure of a user interface element.
    /// </summary>
    public abstract class BaseElement : BaseBody
    {
        #region Constants

        /// <summary>
        /// TODO: These locations will not be the same in deployed environment - needs fixing
        /// </summary>
        private readonly string[] PyPaths = {
            ".",
            "C:\\Program Files\\IronPython 2.7\\Lib",
            "C:\\Program Files\\IronPython 2.7\\DLLs",
            "C:\\Program Files\\IronPython 2.7\\Lib\\site-packages"
        };

        #endregion

        #region Fields

        private Logger logger = LogManager.GetCurrentClassLogger();
        private Rectangle parentBounds;
        private List<Transition> activeTransitions = new List<Transition>();

        #endregion

        /// <summary>
        /// Initialises an instance of the <see cref="BaseElement"/> class.
        /// </summary>
        public BaseElement(string name, int width, int height, Border border, PositionProfile positionProfile)
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

        /// <summary>
        /// Gets or sets a value indicating whether the element can be dragged with the mouse.
        /// </summary>
        public bool Draggable
        {
            get;
            set;
        } = false;

        #endregion

        #region Methods

        public virtual void Update(GameTime gameTime)
        {
            foreach (Transition transition in this.activeTransitions)
            {
                if (!transition.Started && !transition.Done)
                {
                    transition.Start(gameTime);
                }

                transition.Update(gameTime);
            }

            List<Transition> finishedTransitions = this.activeTransitions.Where(transition => transition.Done).ToList();

            if (finishedTransitions.Any())
            {
                for (int i = 0; i < finishedTransitions.Count(); i++)
                {
                    finishedTransitions[i].Restart();
                    this.activeTransitions.Remove(finishedTransitions[i]);
                }
            }
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

        /// <summary>
        /// Recursively searches the element tree and creates a flat list of the element and its children if it has any.
        /// </summary>
        public virtual List<BaseElement> BuildFlattenedSubTree(bool onlyVisibleElements)
        {
            List<BaseElement> result = new List<BaseElement> { };

            if (onlyVisibleElements && !this.Visible)
            {
                return result;
            }

            result.Add(this);

            return result;
        }

        /// <summary>
        /// Initialise the element.
        /// </summary>
        public void Initialise(Rectangle parentBounds, int priority)
        {
            this.Priority = priority;
            this.logger.Debug($"{this.GetType().Name} - {this.Priority}");
            this.parentBounds = parentBounds;

            this.SetPosition(parentBounds);
            this.InternalInitialise();
            this.ExecuteScript();
        }

        /// <summary>
        /// Gets the boundaries of the content within the border.
        /// </summary>
        protected Rectangle GetContentBounds()
        {
            if (this.Border == null)
            {
                return this.GetBounds();
            }

            int x = this.X + this.Border.Width;
            int y = this.Y + this.Border.Width;
            int width = this.Width - (this.Border.Width * 2);
            int height = this.Height - (this.Border.Width * 2);

            return new Rectangle(x, y, width, height);
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
            var paths = scriptEngine.GetSearchPaths().ToList();
            paths.AddRange(PyPaths);
            scriptEngine.SetSearchPaths(paths);
            ScriptScope scope = scriptEngine.CreateScope();

            ScriptSource source = scriptEngine.CreateScriptSourceFromFile(scriptFilePath);
            scope.SetVariable("this", this);
            source.Execute(scope);
        }

        #endregion

        #region Internal Implementations

        protected abstract void InternalDraw(SpriteBatch spriteBatch);


        /// <summary>
        /// Provides the initialisation behaviour specific to the implementing component.
        /// </summary>
        protected abstract void InternalInitialise();

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

        public void Show()
        {
            this.Visible = true;
        }

        public void Hide()
        {
            this.Visible = false;
        }

        public void ToggleVisibility()
        {
            this.Visible = !this.Visible;
        }

        public void Move(Vector2 position)
        {
            this.PositionProfile = PositionFactory.Absolute(position);
            this.SetPosition(this.parentBounds);
        }

        public void AddMovementTransition(PositionProfile destinationProfile, int duration)
        {
            Vector2 destinationPosition = destinationProfile.CalculatePosition(this.parentBounds, this.GetSize());
            MovementTransition transition = new MovementTransition(this.GetPosition(), destinationPosition, destinationProfile, duration, Move);
            this.activeTransitions.Add(transition);
        }

        #endregion

        #region Transition Callbacks

        private void Move(object data)
        {
            PositionProfile positionProfile = (PositionProfile)data;

            this.PositionProfile = positionProfile;
            this.SetPosition(this.parentBounds);
        }

        #endregion
    }
}
