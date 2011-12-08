using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.logic.map.objects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

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
            mainTexture = Program.INSTANCE.Content.Load<Texture2D>("Achtergrond");
        }

        public void DrawBackground()
        {
            Program.INSTANCE.spriteBatch.Draw(mainTexture, new Vector2(0, 0), Color.White);
        }

        public void Draw()
        {
            Parallel.ForEach(objects, delegate(GameObject gameObject)
            {
                if (gameObject.definition.mainTexture != null)
                {
                    gameObject.Draw();
                }
            });
        }
    }
}
