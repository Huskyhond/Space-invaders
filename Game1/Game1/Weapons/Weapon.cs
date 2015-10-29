using Game1;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Weapon<Ammunition>
{
    public abstract void PullTrigger();
    public abstract List<Ammunition> newBullets { get; }
    public abstract void Update(float dt, Player player);
}