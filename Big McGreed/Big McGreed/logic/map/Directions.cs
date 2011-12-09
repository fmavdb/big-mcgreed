using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Big_McGreed.logic.map
{
    public class Directions
    {
        public enum Direction {
            EAST = 2, 
            NORTH = 6, 
            NORTH_EAST = 7, 
            NORTH_WEST = 5, 
            SOUTH = 1, 
            SOUTH_EAST = 2, 
            SOUTH_WEST = 0, 
            WEST = 3,
            NULL = -1
        }

        public static Direction forIntValue(int value)
        {
            switch (value)
            {
                case 0:
                    return Direction.SOUTH_WEST;
                case 1:
                    return Direction.SOUTH;
                case 2:
                    return Direction.SOUTH_EAST;
                case 3:
                    return Direction.WEST;
                case 4:
                    return Direction.EAST;
                case 5:
                    return Direction.NORTH_WEST;
                case 6:
                    return Direction.NORTH;
                case 7:
                    return Direction.NORTH_EAST;
            }
            return Direction.NULL;
        }

        public static Direction directionFor(Vector2 currentPos, Vector2 nextPos) {
            int dirX = (int) (nextPos.X - currentPos.X);
            int dirY = (int) (nextPos.Y - currentPos.Y);
            if (dirX < 0)
            {
                if (dirY < 0)
                    return Direction.SOUTH_WEST;
                else if (dirY > 0)
                    return Direction.NORTH_WEST;
                else
                    return Direction.WEST;
            }
            else if (dirX > 0)
            {
                if (dirY < 0)
                    return Direction.SOUTH_EAST;
                else if (dirY > 0)
                    return Direction.NORTH_EAST;
                else
                    return Direction.EAST;
            }
            else
            {
                if (dirY < 0)
                    return Direction.SOUTH;
                else if (dirY > 0)
                    return Direction.NORTH;
                else
                    return Direction.NULL;
            }
        }
    }
}
