using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    interface Entity
    {
        Vector2 position { get; }
        Vector2 velocity { get; }
        Texture2D texture { get; }
    }
}
