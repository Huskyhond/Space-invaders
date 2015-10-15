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
    abstract class GenericBlaster : Weapon<Bullet>
    {
        protected List<Bullet> currentBullets = new List<Bullet>();
        protected ContentManager Content;
        protected SoundEffect bulletshot;
        protected Vector2 shipPosition;
        protected Player player;
        const int minShotDelay = 5;
        int shotDelay = 0;

        public GenericBlaster(ContentManager content, Player player)
        {
            this.Content = content;
            this.shipPosition = player.position;
            this.player = player;
        }

        public List<Bullet> newBullets
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

        public void Update(float dt, Player player)
        {
            if (shotDelay > 0)
                shotDelay--;
            this.shipPosition = player.position;
            currentBullets = new List<Bullet>();
        }
    }
}
