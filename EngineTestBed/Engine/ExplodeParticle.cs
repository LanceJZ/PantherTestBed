using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using XnaModel = Microsoft.Xna.Framework.Graphics.Model;
using System.Collections.Generic;
using System;

namespace Engine
{
    public class ExplodeParticle : AModel
    {
        Timer LifeTimer;

        public ExplodeParticle(Game game) : base(game)
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

            Velocity = RandomVelocity(100);
            ModelScale = new Vector3(Core.RandomMinMax(0.5f, 1.5f));
            LifeTimer.Reset(Core.RandomMinMax(0.1f, 1));
        }
    }
}
