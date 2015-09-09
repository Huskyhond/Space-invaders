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
        private Vector2 position;
        private Vector2 origin;
        private Vector2 velocity;

        public Vector2 getPosition { get; set; }
        public Vector2 getOrigin { get; set; }
        public Vector2 getVelocity { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f,SpriteEffects.None, 0);
        }
    }
}
