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
    public class Factory : GameComponent, IBeginable, IUpdateableComponent, ILoadContent
    {

        public Factory(Game game) : base(game)
        {

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

        }

        public void BeginRun()
        {

        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public List<AModel> Spawn(List<AModel> entities, Vector3 position)
        {
            bool makeNew = true;
            int thisOne = 0;


            foreach (AModel item in entities)
            {
                if (!item.Active)
                {
                    makeNew = false;
                    break;
                }

                thisOne++;
            }

            if (makeNew)
            {
                entities.Add(new AModel(Game));
                thisOne = entities.Count - 1;
            }

            entities[thisOne].Spawn(position);
            return entities;
        }
    }
}
