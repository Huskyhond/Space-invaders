using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.GameControllers
{
    class KeyboardController : GameController
    {
        public bool shooting()
        {
            return Keyboard.GetState().IsKeyDown(Keys.LeftControl);
        }

        public void update(float DeltaTime, Player player)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                player.position = new Vector2(player.position.X - player.velocity.X, player.position.Y);
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
                player.position = new Vector2(player.position.X + player.velocity.X, player.position.Y);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                player.position = new Vector2(player.position.X, player.position.Y- player.velocity.Y);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                player.position = new Vector2(player.position.X, player.position.Y + player.velocity.Y);
        }

        public bool exit()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Escape);
        }
    }
}
