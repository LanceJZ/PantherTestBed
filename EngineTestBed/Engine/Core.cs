#region Using
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using XnaModel = Microsoft.Xna.Framework.Graphics.Model;
#endregion

namespace Engine
{
    public sealed class Core : DrawableGameComponent
    {
        #region Fields
        private static Core m_Instance = null;
        private static Random m_RandomNumber;
        private static SpriteBatch m_SpriteBatch;
        private static GraphicsDeviceManager m_GraphicsDM;
        private static Camera m_Camera;
        private static Vector3 m_DefuseLight = Vector3.One;
        private static Vector3 m_LightDirection = Vector3.Left;
        private static Vector3 m_SpecularColor = Vector3.Zero;
        private static Vector3 m_AmbientLightColor = new Vector3(0.25f, 0.25f, 0.25f);
        private static Vector3 m_EmissivieColor = Vector3.Zero;
        private static List<IDrawComponent> m_Drawable;
        private static List<IUpdateableComponent> m_Updateable;
        private static List<IBeginable> m_Beginable;
        private static List<ILoadContent> m_Loadable;
        private static Game m_Game;
        #endregion
        #region Properties
        /// <summary>
        /// This is used to get the Services Instance
        /// Instead of using the mInstance this will do the check to see if the Instance is valid
        /// where ever you use it. It is also private so it will only get used inside the engine services.
        /// </summary>
        private static Core Instance
        {
            get
            {
                //Make sure the Instance is valid
                if (m_Instance != null)
                {
                    return m_Instance;
                }

                throw new InvalidOperationException("The Engine Services have not been started!");
            }
        }

        public static Game GameRef { set => m_Game = value; }
        public static GraphicsDeviceManager GraphicsDM { get => m_GraphicsDM; }
        public static SpriteBatch SpriteBatch { get => m_SpriteBatch; set => m_SpriteBatch = value; }
        public static Camera DefaultCamera { get => m_Camera; }
        public static Random RandomNumber { get => m_RandomNumber; }
        /// <summary>
        /// Returns the window size in pixels, of the height.
        /// </summary>
        /// <returns>int</returns>
        public static int WindowHeight { get => m_GraphicsDM.PreferredBackBufferHeight; }
        /// <summary>
        /// Returns the window size in pixels, of the width.
        /// </summary>
        /// <returns>int</returns>
        public static int WindowWidth { get => m_GraphicsDM.PreferredBackBufferWidth; }

        public static Vector2 WindowSize
        {
            get => new Vector2(m_GraphicsDM.PreferredBackBufferWidth, m_GraphicsDM.PreferredBackBufferHeight);
        }

        public static Vector3 DefuseLight { get => m_DefuseLight; set => m_DefuseLight = value; }
        public static Vector3 LightDirection { get => m_LightDirection; set => m_LightDirection = value; }
        public static Vector3 SpecularColor { get => m_SpecularColor; set => m_SpecularColor = value; }
        public static Vector3 AmbientLightColor { get => m_AmbientLightColor; set => m_AmbientLightColor = value; }
        public static Vector3 EmissivieColor { get => m_EmissivieColor; set => m_EmissivieColor = value; }
        #endregion
        #region Constructor
        /// <summary>
        /// This is the constructor for the Services
        /// You will note that it is private that means that only the Services can only create itself.
        /// </summary>
        private Core(Game game) : base(game)
        {
            game.Components.Add(this);
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }
        #endregion
        #region Public Methods
        public static void BeginRun()
        {
            foreach(IBeginable begin in m_Beginable)
            {
                begin.BeginRun();
            }

            m_Beginable.Clear();
        }

        protected override void LoadContent()
        {
            foreach(ILoadContent load in m_Loadable)
            {
                load.LoadContent();
            }

            m_Loadable.Clear();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (IDrawComponent drawable in m_Drawable)
            {
                drawable.Draw();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (IUpdateableComponent updateable in m_Updateable)
            {
                updateable.Update(gameTime);
            }
        }
        /// <summary>
        /// Loads Texture2D from file.
        /// </summary>
        /// <param name="textureName"></param>
        /// <returns></returns>
        public static Texture2D LoadTexture(string textureName)
        {
            if (textureName != null)
            {
                if (File.Exists("Content/Textures/" + textureName + ".xnb"))
                {
                    return m_Game.Content.Load<Texture2D>("Textures/" + textureName);
                }
            }

            System.Diagnostics.Debug.WriteLine("Texture File " + textureName + " was not found.");
            return null;
        }
        /// <summary>
        /// Loads XNA Model from file using the filename. Stored in Content/Models/
        /// </summary>
        /// <param name="modelFileName">File name of model to load.</param>
        /// <returns>XNA Model</returns>
        public static XnaModel LoadModel(string modelFileName)
        {
            if (modelFileName != null)
            {
                if (File.Exists("Content/Models/" + modelFileName + ".xnb"))
                {
                    return m_Game.Content.Load<XnaModel>("Models/" + modelFileName);
                }
            }

            System.Diagnostics.Debug.WriteLine("Model File " + modelFileName + " was not found.");
            return null;
        }
        /// <summary>
        /// Loads Sound Effect from file using filename. Stored in Content/Sounds
        /// </summary>
        /// <param name="soundFileName">FileName</param>
        /// <returns>SoundEffect</returns>
        public static SoundEffect LoadSoundEffect(string soundFileName)
        {
            if (soundFileName != null)
            {
                if (File.Exists("Content/Sounds/" + soundFileName + ".xnb"))
                {
                    return m_Game.Content.Load<SoundEffect>("Sounds/" + soundFileName);
                }
            }

            System.Diagnostics.Debug.WriteLine("Sound File " + soundFileName + " was not found.");
            return null;
        }
        /// <summary>
        /// This is used to start up Panther Engine Services.
        /// It makes sure that it has not already been started if it has been it will throw and exception
        /// to let the user know.
        ///
        /// You pass in the game class so you can get information needed.
        /// </summary>
        /// <param name="graphics">Reference to the graphic device.</param>
        public static void Initialize(GraphicsDeviceManager graphics, Game game, Vector3 cameraPosition, bool othrographic, float near, float far)
        {
            //First make sure there is not already an instance started
            if (m_Instance == null)
            {
                m_Game = game;
                m_GraphicsDM = graphics;
                //Create the Engine Services
                m_Instance = new Core(game);
                m_RandomNumber = new Random(DateTime.Now.Millisecond);
                m_Camera = new Camera(game, cameraPosition, Vector3.Zero, Vector3.Zero, othrographic, near, far);
                m_Drawable = new List<IDrawComponent>();
                m_Updateable = new List<IUpdateableComponent>();
                m_Beginable = new List<IBeginable>();
                m_Loadable = new List<ILoadContent>();

                return;
            }

            throw new Exception("The Engine Services have already been started.");
        }
        /// <summary>
        /// Get a random float between min and max
        /// </summary>
        /// <param name="min">the minimum random value</param>
        /// <param name="max">the maximum random value</param>
        /// <returns>float</returns>
        public static float RandomMinMax(float min, float max)
        {
            return min + (float)RandomNumber.NextDouble() * (max - min);
        }
        /// <summary>
        /// Get a random int between min and max
        /// </summary>
        /// <param name="min">the minimum random value</param>
        /// <param name="max">the maximum random value</param>
        /// <returns>int</returns>
        public static int RandomMinMax(int min, int max)
        {
            return min + (int)(RandomNumber.NextDouble() * ((max + 1) - min));
        }

        public static void AddDrawableComponent(IDrawComponent drawableComponent)
        {
            m_Drawable.Add(drawableComponent);
        }

        public static void AddUpdateableComponent(IUpdateableComponent updateableComponent)
        {
            m_Updateable.Add(updateableComponent);
        }

        public static void AddBeginable(IBeginable beginable)
        {
            m_Beginable.Add(beginable);
        }

        public static void AddLoadable(ILoadContent load)
        {
            m_Loadable.Add(load);
        }
        #endregion
    }
}
