using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    abstract class GenericBlaster : Weapon<Entity>
    {
        protected List<Entity> currentBullets = new List<Entity>();
        protected ContentManager Content;
        protected SoundEffect bulletshot;
        protected Vector2 shipPosition;
        const int minShotDelay = 5;
        int shotDelay = 0;

        public GenericBlaster(ContentManager content, Vector2 shipPos)
        {
            this.Content = content;
            this.shipPosition = shipPos;
        }

        public List<Entity> newBullets
        {
            get
            {
                return currentBullets;
            }
        }

        protected abstract void Addshots();

        public void PullTrigger()
        {
            if (shotDelay == 0)
            {
                bulletshot = Content.Load<SoundEffect>("player_shoot");
                Addshots();
                shotDelay = minShotDelay;
                bulletshot.Play();
            }
        }

        public void Update(float dt, Vector2 shipPos)
        {
            if (shotDelay > 0)
                shotDelay--;
            this.shipPosition = shipPos;
            currentBullets = new List<Entity>();
        }
    }
}
