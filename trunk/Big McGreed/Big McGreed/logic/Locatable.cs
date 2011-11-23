using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.logic.map;
using Microsoft.Xna.Framework;

namespace Big_McGreed.logic
{
    public abstract class Locatable
    {
        private Vector2 location;

        public Vector2 getLocation()
        {
            return location;
        }

        public int getX()
        {
            return (int)location.X;
        }

        public int getY()
        {
            return (int)location.Y;
        }

        public void setLocation(Vector2 newLocation)
        {
            this.location = newLocation;
        }
    }
}
