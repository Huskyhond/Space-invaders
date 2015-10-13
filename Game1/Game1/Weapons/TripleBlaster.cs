using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class TripleBlaster : GenericBlaster
    {
        public TripleBlaster(ContentManager content, Vector2 shipPos) : base(content, shipPos) { }

        protected override void Addshots()
        {
            currentBullets.Add(new Bullet(Content.Load<Texture2D>("player_bullet_left"), shipPosition));
            currentBullets.Add(new Bullet(Content.Load<Texture2D>("player_bullet_left"), shipPosition + new Vector2(25.0f, 0.0f)));
            currentBullets.Add(new Bullet(Content.Load<Texture2D>("player_bullet_left"), shipPosition + new Vector2(12.5f, 0.0f)));
        }
    }
}
