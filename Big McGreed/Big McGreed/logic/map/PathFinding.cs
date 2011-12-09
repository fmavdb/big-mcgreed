using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Big_McGreed.logic.map
{
    public interface PathFinding
    {
        /// <summary>
        /// Finds the path.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        LinkedList<Vector2> findPath(Vector2 start, Vector2 end);
    }
}
