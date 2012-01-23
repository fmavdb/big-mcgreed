using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Data.OleDb;

namespace Big_McGreed.logic.map.objects
{
	public class ObjectDefinition
	{
        /// <summary>
        /// Gets the main texture.
        /// </summary>
        public Texture2D mainTexture { get; private set; }

        //public Color[] pixels { get; set; }

        /// <summary>
        /// Initializes a new definition for the given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static ObjectDefinition forType(int type)
        {
            ObjectDefinition def = (ObjectDefinition)GameWorld.objectDefinitions[type];
            if (def == null)
            {
                def = new ObjectDefinition();
                OleDbDataReader reader = Program.INSTANCE.dataBase.getReader("SELECT * FROM GameObject WHERE GameObjectType = " + type);
                reader.Read();
                object texture = reader["GameObjectTextureName"];
                if (texture != null)
                    def.mainTexture = Program.INSTANCE.loadTexture(Convert.ToString(texture));
                else
                {
                    def.mainTexture = Program.INSTANCE.loadTexture("wolk");
                    Console.WriteLine("GameObject type: " + type + " does not have a main texture.");
                }
                reader.Close();
                GameWorld.objectDefinitions.Add(type, def);
            }
            return def;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectDefinition"/> class.
        /// </summary>
        public ObjectDefinition()
        {
            mainTexture = null;
        }
	}
}
