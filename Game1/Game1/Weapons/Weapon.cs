using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface Weapon<Ammunition>
{
    void PullTrigger();
    List<Ammunition> newBullets { get; }
    void Update(float dt, Vector2 shipPosition);
}