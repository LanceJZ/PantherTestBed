using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using XnaModel = Microsoft.Xna.Framework.Graphics.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using Engine;

namespace Engine
{
    public class Smoke : GameComponent, IBeginable, IUpdateableComponent, ILoadContent
    {
        List<SmokeParticle> Particles;
        XnaModel Cube;
        Vector3 Position;
        float Radius;

        public bool Active { get => Enabled; }

        public Smoke(Game game) : base(game)
        {
            Particles = new List<SmokeParticle>();

            game.Components.Add(this);
        }

        public override void Initialize()
        {

            base.Initialize();
            LoadContent();
            BeginRun();
        }

        public void LoadContent()
        {
            Cube = Core.LoadModel("Core/Cube");
        }

        public void BeginRun()
        {
            Enabled = false;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (SmokeParticle particle in Particles)
            {
                if (!particle.Active)
                {
                    SpawnParticle(particle);
                }
            }

            base.Update(gameTime);
        }

        public void Spawn(Vector3 position, float radius, int minCount)
        {
            Enabled = true;
            Position = position;
            Radius = radius;
            int count = Core.RandomMinMax(minCount, (int)(minCount + radius * 10));

            if (count > Particles.Count)
            {
                int more = count - Particles.Count;

                for (int i = 0; i < more; i++)
                {
                    Particles.Add(new SmokeParticle(Game));
                    Particles.Last().SetModel(Cube);
                    Particles.Last().DefuseColor = new Vector3(0.415f, 0.435f, 0.627f);
                }
            }

            foreach (SmokeParticle particle in Particles)
            {
                SpawnParticle(particle);
            }
        }

        public void Kill()
        {
            foreach (SmokeParticle particle in Particles)
            {
                particle.Active = false;
                Enabled = false;
            }
        }

        void SpawnParticle(SmokeParticle particle)
        {
            Vector3 position = Position;
            position += new Vector3(Core.RandomMinMax(-Radius, Radius),
                Core.RandomMinMax(-Radius, Radius), 0);
            particle.Spawn(position);
        }
    }
}
