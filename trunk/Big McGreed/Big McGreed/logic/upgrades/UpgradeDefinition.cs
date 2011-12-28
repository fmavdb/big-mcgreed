using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.content.upgrades
{
    public class UpgradeDefinition
    {
        public Texture2D mainTexture { get; private set; }

        public int cost = 0;

        /// <summary>
        /// Returns the definition from the cache.
        /// </summary>
        /// <param name="fullName">The full name of the upgrade (name + level).</param>
        /// <returns></returns>
        public static UpgradeDefinition forName(string fullName)
        {
            UpgradeDefinition def = (UpgradeDefinition)GameWorld.upgradeDefinitions[fullName];
            if (def == null) {
                def = new UpgradeDefinition();
                def.mainTexture = Program.INSTANCE.Content.Load<Texture2D>(fullName);
                GameWorld.projectileDefinitions.Add(fullName, def);
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
