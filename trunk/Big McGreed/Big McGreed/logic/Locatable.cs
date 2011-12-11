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

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <returns></returns>
        public Vector2 getLocation()
        {
            return location;
        }


        public Vector2 translate(float x, float y)
        {
            return new Vector2(location.X + x, location.Y + y);
        }
        /// <summary>
        /// Gets the X.
        /// </summary>
        /// <returns></returns>
        public int getX()
        {
            return (int)location.X;
        }

        /// <summary>
        /// Gets the Y.
        /// </summary>
        /// <returns></returns>
        public int getY()
        {
            return (int)location.Y;
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        /// <param name="x">The x.</param>
        public void setX(float x)
        {
            location.X = x;
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        /// <param name="y">The y.</param>
        public void setY(float y)
        {
            location.Y = y;
        }

        /// <summary>
        /// Sets the location.
        /// </summary>
        /// <param name="newLocation">The new location.</param>
        public void setLocation(Vector2 newLocation)
        {
            this.location = newLocation;
        }
    }
}
