using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    public class Bullet_Path
    {
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public List<Bullet> bullets { get; set; }
        public Vector2 offset { get; set; }

        public Bullet_Path(Vector2 position, Vector2 velocity, List<Bullet> bullets, Vector2 offset)
        {
            this.position = position;
            this.velocity = velocity;
            this.bullets = bullets;
            this.offset = offset;
        }

        public void Shoot(Texture2D texture)
        {
            Bullet newBullet = new Bullet(texture);
            newBullet.velocity = velocity;
            newBullet.position = position + offset;

            bullets.Add(newBullet);
        }

        public void Update(Vector2 position)
        {
            this.position = position + offset;
        }
    }
}
