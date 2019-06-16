using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using XnaModel = Microsoft.Xna.Framework.Graphics.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Engine
{
    public class Explode : GameComponent, IBeginable, IUpdateableComponent, ILoadContent
    {
        List<ExplodeParticle> Particles;
        XnaModel Cube;
        Vector3 TheColor;

        public bool Active { get => Enabled; }
        public Vector3 DefuseColor { set => TheColor = value; }

        public Explode(Game game) : base(game)
        {
            Particles = new List<ExplodeParticle>();

            game.Components.Add(this);
        }

        public override void Initialize()
        {
            TheColor = new Vector3(1);
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

        }

        public override void Update(GameTime gameTime)
        {
            bool done = true;

            foreach (ExplodeParticle particle in Particles)
            {
                if (particle.Active)
                {
                    done = false;
                    break;
                }
            }

            if (done)
                Enabled = false;

            base.Update(gameTime);
        }

        public void Spawn(Vector3 position, float radius, int minCount)
        {
            Enabled = true;
            int count = Core.RandomMinMax(minCount, (int)(minCount + radius * 2));

            if (count > Particles.Count)
            {
                int more = count - Particles.Count;

                for (int i = 0; i < more; i++)
                {
                    Particles.Add(new ExplodeParticle(Game));
                    Particles.Last().SetModel(Cube);
                    Particles.Last().DefuseColor = TheColor;
                }
            }

            foreach (ExplodeParticle particle in Particles)
            {
                position += new Vector3(Core.RandomMinMax(-radius, radius),
                    Core.RandomMinMax(-radius, radius), 0);

                particle.Spawn(position);
            }
        }

        public void Kill()
        {
            foreach (ExplodeParticle particle in Particles)
            {
                particle.Active = false;
                Enabled = false;
            }
        }
    }
}
