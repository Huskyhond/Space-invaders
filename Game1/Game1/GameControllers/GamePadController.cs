using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Game1.GameControllers
{
    class GamePadController : GameController
    {
        public void update(float DeltaTime, Player player)
        {
            Vector2 state = GamePad.GetState(0).ThumbSticks.Left;
            
            player.position = new Vector2(player.position.X + (state.X * player.velocity.X), player.position.Y - (state.Y * player.velocity.Y));

        }

        public bool shooting()
        {
            return GamePad.GetState(0).IsButtonDown(Buttons.RightTrigger);
        }

        public bool exit()
        {
            return GamePad.GetState(0).IsButtonDown(Buttons.Start);
        }
    }
}
