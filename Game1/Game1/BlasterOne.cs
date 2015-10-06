using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class BlasterOne : GenericBlaster
    {
        public BlasterOne(ContentManager content, Vector2 shipPos) : base(content, shipPos) { }

        protected override void Addshots()
        {
            currentBullets.Add(new Bullet(Content.Load<Texture2D>("player_bullet_left")));
        }
    }
}
