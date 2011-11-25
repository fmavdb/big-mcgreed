using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Big_McGreed.logic.map
{
    public interface PathFinding
    {
        List<Vector2> findPath();
    }
}
