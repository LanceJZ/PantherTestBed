using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using XnaModel = Microsoft.Xna.Framework.Graphics.Model;
using System.Collections.Generic;
using System;
using Engine;

namespace EngineTestBed.Entities
{
    public class Box : AModel
    {


        public Box(Game game, XnaModel model) : base(game, model)
        {


            LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();


        }

        public void LoadContent()
        {

            BeginRun();
        }

        public override void BeginRun()
        {
            base.BeginRun();


        }

        public override void Update(GameTime gameTime)
        {


            base.Update(gameTime);
        }

        public override void Spawn(Vector3 position)
        {
            base.Spawn(position);


        }
    }
}
