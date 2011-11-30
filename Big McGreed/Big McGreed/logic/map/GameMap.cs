using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.logic.map.objects;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.logic.map
{
    public class GameMap
    {
        /*
         * Stelt de gamemap voor met de nodige objects, npcs en de speler.
         */

        private Texture2D mainTexture = null;

        private LinkedList<GameObject> objects = new LinkedList<GameObject>();

        public GameMap()
        {
        }

        public void Draw()
        {
            lock (objects)
            {
                foreach (GameObject o in objects) {
                    o.Draw();
                }
            }
        }
    }
}
