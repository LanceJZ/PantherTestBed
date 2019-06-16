using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using XnaModel = Microsoft.Xna.Framework.Graphics.Model;
using System.Collections.Generic;
using System;
using Engine;

namespace Engine
{
    public class SmokeParticle : AModel
    {
        Timer LifeTimer;

        public SmokeParticle(Game game) : base(game)
        {
            LifeTimer = new Timer(game);

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
            if (LifeTimer.Elapsed)
                Active = false;

            base.Update(gameTime);
        }

        public override void Spawn(Vector3 position)
        {
            base.Spawn(position);

            Scale = Core.RandomMinMax(1, 2);
            LifeTimer.Reset(Core.RandomMinMax(3.1f, 15.5f));
            Velocity.Y = Core.RandomMinMax(1.1f, 4.5f);
            float drift = 1.5f;
            Velocity.X = Core.RandomMinMax(-drift, drift);
            Velocity.Z = Core.RandomMinMax(-drift, drift);
        }
    }
}
