namespace Frames.Resources
{
    #region Usings

    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using System.IO;

    #endregion

    public class Resources
    {
        #region Constants

        private const string ContentDirectoryName = "Content";
        private const string FontsDirectoryName = "Fonts";
        private const string TexturesDirectoryname = "Textures";

        #endregion

        #region Fields

        private static Resources _instance;

        private GraphicsDevice graphicsDevice;
        private ContentManager contentManager;
        private Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static Resources Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Resources();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice => this.graphicsDevice;

        /// <summary>
        /// Gets the content manager.
        /// </summary>
        public ContentManager ContentManager => this.contentManager;

        /// <summary>
        /// Gets the fonts.
        /// </summary>
        public Dictionary<string, SpriteFont> Fonts => this.fonts;

        /// <summary>
        /// Gets the textures.
        /// </summary>
        public Dictionary<string, Texture2D> Textures => this.textures;

        #endregion

        #region Methods

        /// <summary>
        /// Initialise the resources.
        /// </summary>
        public void Initialise(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;

            this.contentManager.RootDirectory = ContentDirectoryName;
            this.InitialiseFonts();
            this.InitialiseTextures();
        }

        /// <summary>
        /// Initialises all spritefonts provided in the Fonts directory.
        /// </summary>
        private void InitialiseFonts()
        {
            DirectoryInfo fontsDirectory = new DirectoryInfo(Path.Combine(ContentDirectoryName, "Fonts"));

            foreach (FileInfo file in fontsDirectory.GetFiles())
            {
                string fileName = Path.GetFileNameWithoutExtension(file.Name);
                string path = Path.Combine(FontsDirectoryName, fileName);
                this.fonts[fileName] = this.ContentManager.Load<SpriteFont>(path);
            }
        }

        /// <summary>
        /// Initialises all textures provided in the Textures directory.
        /// </summary>
        private void InitialiseTextures()
        {
            DirectoryInfo fontsDirectory = new DirectoryInfo(Path.Combine(ContentDirectoryName, "Textures"));

            foreach (FileInfo file in fontsDirectory.GetFiles())
            {
                string fileName = Path.GetFileNameWithoutExtension(file.Name);
                string path = Path.Combine(TexturesDirectoryname, fileName);
                this.textures[fileName] = this.ContentManager.Load<Texture2D>(path);
            }
        }

        #endregion
    }
}
