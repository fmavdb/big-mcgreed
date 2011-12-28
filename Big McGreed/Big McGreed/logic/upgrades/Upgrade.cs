using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Big_McGreed.logic.player;

namespace Big_McGreed.content.upgrades
{
    /// <summary>
    /// This represents an upgrade.
    /// </summary>
    public class Upgrade
    {
        private Player player;

        private string fullName;

        private string name;

        private int level = 0; 

        /// <summary>
        /// Initializes a new instance of the <see cref="Upgrade"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="name">The name.</param>
        public Upgrade(Player player, string name)
        {
            this.player = player;
            Name = name;
        }

        /// <summary>
        /// Returns the name of the upgrade.
        /// 
        /// Sets the name of the upgrade. Definition will get loaded if not present in the cache.
        /// </summary>
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

        /// <summary>
        /// Levels up.
        /// </summary>
        /// <returns></returns>
        public bool LevelUp()
        {
            if (level >= 2)  //Already achieved highest level.
                return false;
            if (player.gold < definition.cost) //Player does not have enough valuta.
                return false;
            level++;
            fullName = name + level;
            return true;
        }

        /// <summary>
        /// Gets the definition.
        /// </summary>
        public UpgradeDefinition definition { get { return UpgradeDefinition.forName(fullName); } }

    }
}
