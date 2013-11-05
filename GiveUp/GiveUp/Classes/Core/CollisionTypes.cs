using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public enum CollisionType
    {
        Full,
        FullTop,
        Passable,
        GooLeft,
        GooRight,
        GooBoth,
        PerPixelCollision,
        Water,
        Moveable,
        None,
    }
}
