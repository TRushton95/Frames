namespace TestApp
{
    #region Usings

    using Frames.DataStructures.PositionProfiles;
    using Frames.Enums;
    using Frames.Resources;
    using Frames.Structure.Components;
    using Frames.UI.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

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

            Resources.Instance.Initialise(this.GraphicsDevice);
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

            // TODO: Add your update logic here

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
            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Initialises the test components.
        /// </summary>
        private void InitialiseTestComponents()
        {
            Rectangle windowBounds = new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);

            var centerPositionProfile = new RelativePositionProfile
            {
                VerticalAlign = VerticalAlign.Middle,
                HorizontalAlign = HorizontalAlign.Middle
            };

            this.frame = new Frame(100, 100, Color.DarkRed, centerPositionProfile);
            this.frame.Initialise(windowBounds);

            var topPositionProfile = new RelativePositionProfile
            {
                VerticalAlign = VerticalAlign.Top,
                HorizontalAlign = HorizontalAlign.Left
            };

            this.textGraphics = new TextGraphics("Hello world!", font, Color.Black, topPositionProfile);
            this.textGraphics.Initialise(windowBounds);

            var bottomPositionProfile = new RelativePositionProfile
            {
                VerticalAlign = VerticalAlign.Bottom,
                HorizontalAlign = HorizontalAlign.Right
            };

            this.imageGraphics = new ImageGraphics(this.image, bottomPositionProfile);
            this.imageGraphics.Initialise(windowBounds);
        }

        #endregion
    }
}
