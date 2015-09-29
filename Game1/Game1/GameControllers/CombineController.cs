using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.GameControllers
{
    class CombineController : GameController
    {
        GameController controller1, controller2;

        public CombineController(GameController ctrl1, GameController ctrl2)
        {
            controller1 = ctrl1;
            controller2 = ctrl2;
        }

        public bool shooting()
        {
            return controller1.shooting() || controller2.shooting();
        }

        public void update(float DeltaTime, Player player)
        {
            controller1.update(DeltaTime, player);
            controller2.update(DeltaTime, player);
        }

        public bool exit()
        {
            return controller1.exit() || controller2.exit();
        }
    }
}
