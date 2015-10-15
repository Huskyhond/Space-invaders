using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    class SingleBlaster : GenericBlaster
    {
        public SingleBlaster(ContentManager content, Player player) : base(content, player) { }

        protected override void Addshots()
        {
            currentBullets.Add(new Bullet(Content.Load<Texture2D>("player_bullet_left"), player.position, player));
        }
    }
}
