using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Data.OleDb;
using Microsoft.Xna.Framework;

namespace Big_McGreed.content.upgrades
{
    public class UpgradeDefinition
    {
        /// <summary>
        /// Gets the main texture.
        /// </summary>
        public Texture2D mainTexture { get; set; }

        private Texture2D oldTexture { get; set; }

        /// <summary>
        /// The upgrades cost.
        /// </summary>
        public int cost = 0;

        public int damage = -1;

        public int weaponSpeed = -1;

        public Texture2D electricWall { get; private set; }

        /// <summary>
        /// Gets the pixels.
        /// </summary>
        public Color[,] pixels { get; private set; }

        /// <summary>
        /// Returns the definition from the cache.
        /// </summary>
        /// <param name="fullName">The full name of the upgrade (name + level).</param>
        /// <returns>The definition.</returns>
        public static UpgradeDefinition forName(string fullName)
        {
            UpgradeDefinition def = (UpgradeDefinition)GameWorld.upgradeDefinitions[fullName];
            if (def == null)
            {
                def = new UpgradeDefinition();
                def.mainTexture = Program.INSTANCE.loadTexture(fullName);
                OleDbDataReader reader = Program.INSTANCE.dataBase.getReader("SELECT * FROM Upgrade WHERE UpgradeFullName = '" + fullName + "'");
                reader.Read();
                object cost = reader["UpgradeCost"];
                if (cost != null)
                    def.cost = Convert.ToInt32(cost);
                else
                    Console.Error.WriteLine(fullName + " does not have a definition in the database.");
                reader.Close();
                if (fullName.Contains("weapon")) 
                {
                    reader = Program.INSTANCE.dataBase.getReader("SELECT * FROM Weapon WHERE WeaponFullName = '" + fullName + "'");
                    reader.Read();
                    def.damage = Convert.ToInt32(reader["WeaponDamage"]);
                    def.weaponSpeed = Convert.ToInt32(reader["WeaponSpeed"]);
                    reader.Close();
                }
                if (fullName.Contains("muur"))
                {
                    Color[] colors1D = new Color[def.mainTexture.Width * def.mainTexture.Height];
                    def.mainTexture.GetData<Color>(colors1D);
                    def.pixels = new Color[def.mainTexture.Width, def.mainTexture.Height];
                    for (int x = 0; x < def.mainTexture.Width; x++)
                        for (int y = 0; y < def.mainTexture.Height; y++)
                            def.pixels[x, y] = colors1D[x + y * def.mainTexture.Width];
                    if (fullName.Contains("1")) 
                    {
                        def.electricWall = Program.INSTANCE.loadTexture("Muur1Shock");
                    }
                }
                GameWorld.upgradeDefinitions.Add(fullName, def);
            }
            return def;
        }

        public void ChangeTextureToElectric()
        {
            oldTexture = mainTexture;
            mainTexture = electricWall;
        }

        public void ChangeTextureBackToNormal()
        {
            mainTexture = oldTexture;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeDefinition"/> class.
        /// </summary>
        public UpgradeDefinition()
        {
            mainTexture = null;
        }
    }
}
