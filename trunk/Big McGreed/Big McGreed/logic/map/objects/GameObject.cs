using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.logic.map.objects
{
    public class GameObject
    {
        public int type { get; set; }

        public bool visible { get; set; }

        public ObjectDefinition definition { get; private set; }

        public GameObject(int type)
        {
            this.type = type;
            definition = ObjectDefinition.forType(type);
        }

        public void Draw()
        {
        }
    }
}
