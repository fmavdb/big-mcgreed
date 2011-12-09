using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Big_McGreed.logic.map
{
    public interface PathFinding
    {
        LinkedList<Vector2> findPath(Vector2 start, Vector2 end);
    }
}
