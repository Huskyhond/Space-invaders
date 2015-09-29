using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    public class Astroid : Entity
    {
       public Vector2 position { get; set; }
       public Vector2 origin { get; set; }
       public Vector2 velocity { get; set; }
       public double health { get; set; }
       public Rectangle rectangle { get; set; }

       public Texture2D texture { get; set; }

       public Astroid(Texture2D texture, Vector2 position = new Vector2(), Vector2 velocity = new Vector2(), double health = 10)
       {
            this.texture = texture;
            this.health = health;
            rectangle = new Rectangle(0, 0, 40, 40);
            this.position = position;
            this.velocity = velocity;
       }
       public void Draw(SpriteBatch spriteBatch)
       {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
       }
    }
}
