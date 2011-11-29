using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.logic.map.objects;

namespace Big_McGreed.logic.map
{
    public class GameMap
    {
        private LinkedList<GameObject> objects = new LinkedList<GameObject>();

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
