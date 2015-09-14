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
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public int amount { get; set; }

        public Health(Texture2D texture, int health = 3)
        {
            this.texture = texture;
            this.amount = health;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (this.amount == 3)
            {
                spritebatch.Draw(texture, position, new Rectangle(10, 10, 150, 20), Color.Green);
            }
            else if (this.amount == 2)
            {
                spritebatch.Draw(texture, position, new Rectangle(60, 10, 100, 20), Color.Yellow);
            }
            else if (this.amount == 1)
            {
                spritebatch.Draw(texture, position, new Rectangle(110, 10, 50, 20), Color.Red);
            }
        }
    }
}
