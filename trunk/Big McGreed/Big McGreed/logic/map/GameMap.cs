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

        private LinkedList<GameObject> objects;

        public GameMap()
        {
            objects = new LinkedList<GameObject>();
            mainTexture = Program.INSTANCE.Content.Load<Texture2D>("Achtergrond");
        }

        public void DrawBackground()
        {
            float scale = 1.0f;
            //Console.WriteLine(scale);
            Program.INSTANCE.spriteBatch.Draw(mainTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public void Draw()
        {
            lock(objects) {
                foreach (GameObject gameObject in objects)
                {
                    gameObject.Draw();
                }
            }
        }
    }
}
