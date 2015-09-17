﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    public class Bullet
    {
        public Texture2D texture { get; set; }
        public Vector2 position { get ; set; }
        public Vector2 origin { get; set; }
        public Vector2 velocity { get; set; }
        public Rectangle rectangle { get; set; }

        public Bullet(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;
            rectangle = new Rectangle(0, 0, 5, 10);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1f,SpriteEffects.None, 0);
        }
    }
}
