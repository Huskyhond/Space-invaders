using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game1.GameControllers
{
    interface GameController
    {
        void update(float DeltaTime, Player player);
        bool shooting();
        bool exit();
    }
}
