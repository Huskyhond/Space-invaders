using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class Bullet
    {
        private Texture2D texture;

        public Vector2 position { get ; set; }
        public Vector2 origin { get; set; }
        public Vector2 velocity { get; set; }
        public Boolean visible { get; set;}

        public Bullet(Texture2D texture)
        {
            this.texture = texture;
            visible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f,SpriteEffects.None, 0);
        }
    }
}
