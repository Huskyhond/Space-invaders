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
        public int lives { get; set; }
        public Health health { get; set; }
        public Texture2D texture { get; set; }
        public List<Bullet_Path> bullet_path { get; set; }
        public int powerupcounter { get; set; }

        public int score { get; set; }

        public Player(Texture2D texture, Health health, List<Bullet_Path> bullet_path)
        {
            ////this.score = 0;
            this.health = health;
            this.texture = texture;
            this.bullet_path = bullet_path;
            this.powerupcounter = 0;
            this.lives = 3;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
