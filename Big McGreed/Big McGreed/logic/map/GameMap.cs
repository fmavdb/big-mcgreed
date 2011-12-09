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
        private Texture2D mainTexture = null;

        private LinkedList<GameObject> objects;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameMap"/> class.
        /// </summary>
        public GameMap()
        {
            objects = new LinkedList<GameObject>();
            mainTexture = Program.INSTANCE.Content.Load<Texture2D>("Achtergrond");
        }

        /// <summary>
        /// Draws the background.
        /// </summary>
        public void DrawBackground()
        {
            float scale = 1.0f;
            //Console.WriteLine(scale);
            Program.INSTANCE.spriteBatch.Draw(mainTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
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
