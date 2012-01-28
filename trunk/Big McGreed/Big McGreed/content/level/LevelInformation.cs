using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using Big_McGreed.utility;

namespace Big_McGreed.content.level
{
    public class LevelInformation
    {
        /// <summary>
        /// Gets the waves.
        /// </summary>
        public static Dictionary<int, LevelInformation> levels { get; private set; }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public static void Load()
        {
            levels = new Dictionary<int, LevelInformation>();
            OleDbDataReader reader = Program.INSTANCE.dataBase.getReader("SELECT * FROM LevelInformation");
            int level;
            int[] npcs;
            int npcCount;
            int npcSpawnInterval;
            int npcBossType;
            while (reader.Read())
            {
                level = Convert.ToInt32(reader["Level"]);
                npcs = ArrayUtilities.StringToIntArray(Convert.ToString(reader["NPCTypes"]), ',');
                npcCount = Convert.ToInt32(reader["NPCCount"]);
                npcSpawnInterval = Convert.ToInt32(reader["NPCSpawnInterval"]);
                npcBossType = Convert.ToInt32(reader["NPCBossType"]);
                levels.Add(level, new LevelInformation(level, npcs, npcCount, npcSpawnInterval, npcBossType));
            }
            reader.Close();
        }

        /// <summary>
        /// Fors the value.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        public static LevelInformation forValue(int level)
        {
            LevelInformation levelInformation = null;
            if (!levels.TryGetValue(level, out levelInformation))
            {
                return null;
            }
            return levelInformation;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [boss spawned].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [boss spawned]; otherwise, <c>false</c>.
        /// </value>
        public bool bossSpawned { get; set; }

        /// <summary>
        /// Gets or sets the current spawned enemies amount.
        /// </summary>
        /// <value>
        /// The current spawned enemies amount.
        /// </value>
        public int currentSpawnedEnemiesAmount { get; set; }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            bossSpawned = false;
            currentSpawnedEnemiesAmount = 0;
        }

        #region Loaded info

        /// <summary>
        /// Gets the amount of enemies.
        /// </summary>
        public int amountOfEnemies { get; private set; }
        /// <summary>
        /// Gets the wave.
        /// </summary>
        public int level { get; private set; }
        /// <summary>
        /// Gets the wave delay.
        /// </summary>
        public int waveDelay { get; private set; }
        /// <summary>
        /// Gets the NPC types.
        /// </summary>
        public int[] npcTypes { get; private set; } 
        /// <summary>
        /// Gets the type of the boss.
        /// </summary>
        public int bossType { get; private set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="LevelInformation"/> class from being created.
        /// </summary>
        /// <param name="level">The wave.</param>
        /// <param name="npcTypes">The NPC types.</param>
        /// <param name="amountOfEnemies">The amount of enemies.</param>
        /// <param name="waveDelay">The wave delay.</param>
        private LevelInformation(int level, int[] npcTypes, int amountOfEnemies, int waveDelay, int bossType)   //Wavedelay in ms
        {
            this.level = level;
            this.waveDelay = waveDelay;
            this.npcTypes = npcTypes;
            this.amountOfEnemies = amountOfEnemies;
            this.bossType = bossType;
        }

        #endregion Loaded info
    }
}
