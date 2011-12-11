using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Big_McGreed.logic.map.objects
{
    public class GameObject : Locatable
    {
        public int type { get; set; }

        public bool visible { get; set; }

        public ObjectDefinition definition { get { return ObjectDefinition.forType(type); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public GameObject(int type)
        {
            this.type = type;
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            Program.INSTANCE.spriteBatch.Draw(definition.mainTexture, getLocation(), Color.White);
        }
    }
}
