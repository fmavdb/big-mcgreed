using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Data.OleDb;

namespace Big_McGreed.content.upgrades
{
    public class UpgradeDefinition
    {
        /// <summary>
        /// Gets the main texture.
        /// </summary>
        public Texture2D mainTexture { get; private set; }

        /// <summary>
        /// The upgrades cost.
        /// </summary>
        public int cost = 0;

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
                GameWorld.upgradeDefinitions.Add(fullName, def);
            }
            return def;
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
