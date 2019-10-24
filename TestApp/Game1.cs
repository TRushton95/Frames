namespace TestApp
{
    #region Usings

    using Frames.Deserialisation.Converters;
    using Frames.Enums;
    using Frames.Factories;
    using Frames.Resources;
    using Frames.Structure.Components;
    using Frames.UI.Components;
    using Frames.UI.Elements;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    #endregion

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region Fields

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SpriteFont font;
        private Texture2D image;
        private Frame frame;
        private TextGraphics textGraphics;
        private ImageGraphics imageGraphics;

        private List<BaseElement> elements;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises an instance of the <see cref="Game1"/> class.
        /// </summary>
        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            font = this.Content.Load<SpriteFont>("Fonts/Font");
            image = this.Content.Load<Texture2D>("Images/Fireball");

            Resources.Instance.Initialise(this.GraphicsDevice, this.Content);
            this.InitialiseTestComponents();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (BaseElement element in this.elements)
            {
                // TODO: Add your update logic here
                if (!element.Hovered && element.GetBounds().Contains(Mouse.GetState().Position))
                {
                    element.Hover();
                }
                else if (element.Hovered && !element.GetBounds().Contains(Mouse.GetState().Position))
                {
                    element.HoverLeave();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();
            this.frame.Draw(this.spriteBatch);
            this.textGraphics.Draw(this.spriteBatch);
            this.imageGraphics.Draw(this.spriteBatch);

            foreach (BaseElement element in this.elements)
            {
                element.Draw(this.spriteBatch);
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Initialises the test components.
        /// </summary>
        private void InitialiseTestComponents()
        {
            Rectangle windowBounds = new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);

            this.frame = new Frame(100, 100, Color.DarkRed, PositionFactory.CenteredRelative());
            this.frame.Initialise(windowBounds);

            this.textGraphics = new TextGraphics("Hello world!", font, Color.Black, 0, FontFlow.None, PositionFactory.TopLeftRelative());
            this.textGraphics.Initialise(windowBounds);

            this.imageGraphics = new ImageGraphics(this.image, PositionFactory.BottomRightRelative());
            this.imageGraphics.Initialise(windowBounds);

            string json = File.ReadAllText("ui.json");
            JsonConverter[] converters = {
                new PositionProfileConverter(),
                new SpriteFontConverter(),
                new ColorConverter(),
                new ElementConverter()
            };

            elements = JsonConvert.DeserializeObject<List<BaseElement>>(json, new JsonSerializerSettings() { Converters = converters });

            foreach (BaseElement element in elements)
            {
                element.Initialise(windowBounds);
            }
        }

        #endregion
    }
}
