using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.GameControllers
{
    class MouseController : GameController
    {
        public override bool shooting()
        {
            return (Mouse.GetState().LeftButton == ButtonState.Pressed);
        }

        public override void update(float DeltaTime, Player player)
        {
            player.position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }

        public override bool exit()
        {
            return false;
        }
    }
}
