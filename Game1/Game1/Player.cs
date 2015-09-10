using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    public class Player
    {
        public Vector2 position { get; set; }
        public Vector2 origin { get; set; }
        public Vector2 velocity { get; set; }

        public int health { get; set; }
        public Texture2D texture { get; set; }
        public Player(Texture2D texture, int health = 3)
        {
            this.health = health;
            this.texture = texture;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
