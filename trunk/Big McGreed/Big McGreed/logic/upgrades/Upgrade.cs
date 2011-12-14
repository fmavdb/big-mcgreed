using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.content.upgrades
{
    public class Upgrade
    {
        private string fullName;

        private string name;

        private int level = 0;

        /// <summary>
        /// Returns the name of the upgrade.
        /// 
        /// Sets the name of the upgrade. Definition will get loaded if not present in the cache.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string Name 
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
                this.fullName = name + level;
            }        
        }

        public void LevelUp()
        {
            if (level < 3) //Highest level = 3
                level++;
            fullName = name + level;
        }

        public UpgradeDefinition definition { get { return UpgradeDefinition.forName(fullName); } }

    }
}
