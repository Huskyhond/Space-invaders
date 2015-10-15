using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game1.GameControllers;

namespace Game1
{
    public class Player : Entity
    {
        public Vector2 position { get; set; }
        public Vector2 origin { get; set; }
        public Vector2 velocity { get; set; }
        public int lives { get; set; }
        public Health health { get; set; }
        public Texture2D texture { get; set; }
        public int powerupcounter { get; set; }
        public Weapon<Bullet> weapon { get; set; }
        public GameController controller { get; set; }
        public String player_name { get; set; }

        public int score { get; set; }

        public Player(Texture2D texture, Health health, String player_name)
        {
            ////this.score = 0;
            this.health = health;
            this.texture = texture;
            this.powerupcounter = 0;
            this.lives = 3;
            this.velocity = new Vector2(6.0f, 6.0f);
            this.player_name = player_name;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
