using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game1.GameControllers
{
    public abstract class GameController
    {
        public static GameController operator +(GameController c1, GameController c2) {
            return new CombineController(c1, c2);
        }
        public abstract void update(float DeltaTime, Player player);
        public abstract bool shooting();
        public abstract bool exit();
    }
}
