using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Data.OleDb;

namespace Big_McGreed.logic.npc
{
    public class NPCDefinition
    {     
        /// <summary>
        /// Gets the main texture.
        /// </summary>
        public Texture2D mainTexture { get; private set; }

        /// <summary>
        /// Gets the pixels.
        /// </summary>
        public Color[,] pixels { get; private set; }

        /// <summary>
        /// Gets the hitted texture.
        /// </summary>
        public Texture2D hittedTexture { get; private set; }

        /// <summary>
        /// The NPC's lifes.
        /// </summary>
        public int hp = 0;

        /// <summary>
        /// The NPC's speed.
        /// </summary>
        public int speed = 0;

        /// <summary>
        /// The NPC's damage.
        /// </summary>
        public int damage = -1;

        /// <summary>
        /// Initializes a new instance for the npc type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static NPCDefinition forType(int type)
        {
            NPCDefinition def = (NPCDefinition)GameWorld.npcDefinitions[type];
            if (def == null)
            {
                def = new NPCDefinition();
                OleDbDataReader reader = Program.INSTANCE.dataBase.getReader("SELECT * FROM NPC WHERE NPCType = " + type);
                reader.Read();
                object texture = reader["NPCTextureName"];
                if (texture != null)
                    def.mainTexture = Program.INSTANCE.loadTexture(Convert.ToString(texture));
                else
                {
                    Console.Error.WriteLine("NPC type: " + type + " main texture has not been added.");
                    def.mainTexture = Program.INSTANCE.loadTexture("poppetje");
                }
                def.hittedTexture = Program.INSTANCE.loadTexture("poppetje_rood");
                Color[] colors1D = new Color[def.mainTexture.Width * def.mainTexture.Height];
                def.mainTexture.GetData<Color>(colors1D);
                def.pixels = new Color[def.mainTexture.Width, def.mainTexture.Height];
                for (int x = 0; x < def.mainTexture.Width; x++)
                    for (int y = 0; y < def.mainTexture.Height; y++)
                        def.pixels[x, y] = colors1D[x + y * def.mainTexture.Width];
                object lifes = reader["NPCLifes"];
                if (lifes != null)
                    def.hp = Convert.ToInt32(lifes);
                else
                {
                    Console.Error.WriteLine("NPC type: " + type + " lifes has not been added.");
                    def.hp = 20;
                }
                object speed = reader["NPCSpeed"];
                if (speed != null)
                    def.speed = Convert.ToInt32(speed);
                else
                {
                    Console.Error.WriteLine("NPC type: " + type + " speed has not been added.");
                    def.speed = 5;
                }
                object damage = reader["NPCDamage"];
                if (damage != null)
                    def.damage = Convert.ToInt32(damage);
                else
                {
                    Console.Error.WriteLine("NPC type: " + type + " damage has not been added.");
                    def.damage = -1;
                }
                reader.Close();
                GameWorld.npcDefinitions.Add(type, def);
            }
            return def;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NPCDefinition"/> class.
        /// </summary>
        public NPCDefinition()
        {
            mainTexture = null;
        }
    }
}
