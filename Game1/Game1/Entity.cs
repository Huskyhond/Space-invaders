using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public abstract class Entity
    {
        public abstract Vector2 position { get; }
        public abstract Vector2 velocity { get; }
        public abstract Texture2D texture { get; }
    }
}
