using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace Panther
{
    public static class Helper
    {
        #region Fields
        static GraphicsDeviceManager TheGraphicsDM;
        static GraphicsDevice TheGraphicsDevice;
        static Random RandomNumberGenerator = new Random(DateTime.Now.Millisecond);
        static Game GameRef;
        public static uint SreenWidth;
        public static uint ScreenHeight;
        #endregion
        #region Properties
        public static Random Rand { get => RandomNumberGenerator; }
        public static GraphicsDeviceManager GraphicsDM { get => TheGraphicsDM; }
        public static GraphicsDevice Graphics { get => TheGraphicsDevice; }
        public static Game TheGame { get => GameRef; }
        /// <summary>
        /// Returns the window size in pixels, of the height.
        /// </summary>
        /// <returns>int</returns>
        public static int WindowHeight { get => TheGraphicsDM.PreferredBackBufferHeight; }
        /// <summary>
        /// Returns the window size in pixels, of the width.
        /// </summary>
        /// <returns>int</returns>
        public static int WindowWidth { get => TheGraphicsDM.PreferredBackBufferWidth; }
        /// <summary>
        /// Returns The Windows size in pixels as a Vector2.
        /// </summary>
        public static Vector2 WindowSize
        {
            get => new Vector2(TheGraphicsDM.PreferredBackBufferWidth,
                TheGraphicsDM.PreferredBackBufferHeight);
        }
        #endregion
        #region Initialize
        public static void Initialize(Game game, GraphicsDeviceManager graphicsDeviceManager)
        {
            GameRef = game;
            TheGraphicsDM = graphicsDeviceManager;
            TheGraphicsDevice = graphicsDeviceManager.GraphicsDevice;
        }
        #endregion
        #region Helper Methods
        /// <summary>
        /// Get a random float between min and max
        /// </summary>
        /// <param name="min">the minimum random value</param>
        /// <param name="max">the maximum random value</param>
        /// <returns>float</returns>
        public static float RandomMinMax(float min, float max)
        {
            return min + (float)RandomNumberGenerator.NextDouble() * (max - min);
        }
        /// <summary>
        /// Get a random int between min and max
        /// </summary>
        /// <param name="min">the minimum random value</param>
        /// <param name="max">the maximum random value</param>
        /// <returns>int</returns>
        public static int RandomMinMax(int min, int max)
        {
            return min + (int)(RandomNumberGenerator.NextDouble() * ((max + 1) - min));
        }
        /// <summary>
        /// Loads XNA Model from file using the filename. Stored in Content/Models/
        /// </summary>
        /// <param name="modelFileName">File name of model to load.</param>
        /// <returns>XNA Model</returns>
        public static Model LoadModel(string modelFileName)
        {
            if (modelFileName != "")
            {
                if (File.Exists("Content/Models/" + modelFileName + ".xnb"))
                    return GameRef.Content.Load<Model>("Models/" + modelFileName);

                System.Diagnostics.Debug.WriteLine("The Model File " + modelFileName + " was not found.");
            }
            else
                System.Diagnostics.Debug.WriteLine("The Model File Name was empty");

            return null;
        }
        /// <summary>
        /// Loads Texture2D from file using the filename. Stored in Content/Textures
        /// </summary>
        /// <param name="textureFileName">File Name of the texture.</param>
        /// <returns></returns>
        public static Texture2D LoadTexture(string textureFileName)
        {
            if (textureFileName != "")
            {
                if (File.Exists("Content/Textures/" + textureFileName + ".xnb"))
                    return GameRef.Content.Load<Texture2D>("Textures/" + textureFileName);
            }

            System.Diagnostics.Debug.WriteLine("The Texture File " + textureFileName + " was not found.");
            return null;
        }
        #endregion
    }
}
