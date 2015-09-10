using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    public class Health
    {
        public Texture2D texture {get; set; }
        public Vector2 position {get; set; }
        public int amount { get; set; }

        public Health(Texture2D texture,int health = 3)
        {
            this.texture = texture;
            this.amount = health;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, new Rectangle(10, 10, 150, 20), Color.Red);
        }
    }
}
