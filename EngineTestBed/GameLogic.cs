using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using XnaModel = Microsoft.Xna.Framework.Graphics.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using Engine;
using EngineTestBed.Entities;

public enum GameState
{
    Over,
    InPlay,
    HighScore,
    MainMenu
};

namespace EngineTestBed
{
    public class GameLogic : GameComponent, IBeginable, IUpdateableComponent, ILoadContent
    {
        List<Box> TheBoxs;
        XnaModel BoxModel;

        GameState GameMode = GameState.MainMenu;
        KeyboardState OldKeyState;

        public GameState CurrentMode { get => GameMode; }

        public GameLogic(Game game) : base(game)
        {
            TheBoxs = new List<Box>();

            // Screen resolution is 1200 X 900.
            // Y positive is Up.
            // X positive is right of window when camera is at rotation zero.
            // Z positive is towards the camera when at rotation zero.
            // Positive rotation rotates CCW. Zero has front facing X positive. Pi/2 on Y faces Z negative.
            game.Components.Add(this);
        }

        public override void Initialize()
        {

            base.Initialize();
            LoadContent();
        }

        public void LoadContent()
        {
            BoxModel = Core.LoadModel("Core/Cube");

            BeginRun();
        }

        public void BeginRun()
        {
            for (int i = 0; i < 10; i++)
            {
                TheBoxs.Add(new Box(Game, BoxModel));
            }

            TheBoxs[0].ModelScale = new Vector3(50);
            TheBoxs[0].Position = new Vector3(50, -50, 0);
            TheBoxs[0].RotationVelocity = new Vector3(0, 0, 1);
            TheBoxs[1].ModelScale = new Vector3(7);
            TheBoxs[1].Position = new Vector3(200, 0, 0);
            TheBoxs[1].RotationVelocity = new Vector3(0, 0, 2);
            TheBoxs[1].AddAsChildOf(TheBoxs[0]);
            TheBoxs[2].ModelScale = new Vector3(4);
            TheBoxs[2].Position = new Vector3(30, 0, 0);
            TheBoxs[2].RotationVelocity = new Vector3(0, 0, 2);
            TheBoxs[2].AddAsChildOf(TheBoxs[1]);
            TheBoxs[3].ModelScale = new Vector3(3);
            TheBoxs[3].Position = new Vector3(-10, 0, 0);
            TheBoxs[3].AddAsChildOf(TheBoxs[1]);
            TheBoxs[4].ModelScale = new Vector3(3);
            TheBoxs[4].Position = new Vector3(0, -10, 0);
            TheBoxs[4].AddAsChildOf(TheBoxs[1]);
            TheBoxs[5].ModelScale = new Vector3(3);
            TheBoxs[5].Position = new Vector3(0, 10, 0);
            TheBoxs[5].AddAsChildOf(TheBoxs[1]);
            TheBoxs[6].ModelScale = new Vector3(1);
            TheBoxs[6].Position = new Vector3(5, 0, 0);
            TheBoxs[6].AddAsChildOf(TheBoxs[2]);
            TheBoxs[7].ModelScale = new Vector3(1);
            TheBoxs[7].Position = new Vector3(-5, 0, 0);
            TheBoxs[7].AddAsChildOf(TheBoxs[2]);
            TheBoxs[8].ModelScale = new Vector3(1);
            TheBoxs[8].Position = new Vector3(0, 5, 0);
            TheBoxs[8].AddAsChildOf(TheBoxs[2]);
            TheBoxs[9].ModelScale = new Vector3(1);
            TheBoxs[9].Position = new Vector3(0, -5, 0);
            TheBoxs[9].AddAsChildOf(TheBoxs[2]);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState KBS = Keyboard.GetState();

            if (KBS != OldKeyState)
            {
                if (KBS.IsKeyDown(Keys.Space))
                {
                    TheBoxs[0].Active = !TheBoxs[0].Active;
                }
            }

            OldKeyState = Keyboard.GetState();

            base.Update(gameTime);
        }
    }
}
