﻿namespace Frames.Resources
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

        #endregion

        #region Fields

        private static Resources _instance;

        private GraphicsDevice graphicsDevice;
        private ContentManager contentManager;
        private Dictionary<string, SpriteFont> fonts;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises an instance of the <see cref="Resources"/> class.
        /// </summary>
        public Resources()
        {
            this.fonts = new Dictionary<string, SpriteFont>();
        }

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

        #endregion
    }
}
