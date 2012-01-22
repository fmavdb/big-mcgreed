using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Big_McGreed.logic.player;
using Big_McGreed.logic.npc;
using Big_McGreed.engine;
using Big_McGreed.engine.update;
using Big_McGreed.content.info;
using Big_McGreed.logic.map;
using Big_McGreed.content.gameinterface;
using Big_McGreed.engine.misc;
using System.Collections;
using Big_McGreed.content.gameinterface.buttons;
using Big_McGreed.content.gameframe;
using Big_McGreed.content.highscore;
using Big_McGreed.content.data.sql;
using Big_McGreed.content.hardware;
using Big_McGreed.content.input;


namespace Big_McGreed
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameWorld : Microsoft.Xna.Framework.Game
    {

        //Als er iets niet werkt, deze aanzetten. Zorgt ervoor dat je de console goed kan zien terwijl je het spel speelt.
        private static bool DEBUG_MODE = false;

        /// <summary>
        /// Word gebruikt om de definities op te slaan nadat ze zijn geladen.
        /// </summary>
        public static readonly Hashtable projectileDefinitions = new Hashtable();

        /// <summary>
        /// Word gebruikt om de definities op te slaan nadat ze zijn geladen.
        /// </summary>
        public static readonly Hashtable npcDefinitions = new Hashtable();

        /// <summary>
        /// Word gebruikt om de definities op te slaan nadat ze zijn geladen.
        /// </summary>
        public static readonly Hashtable objectDefinitions = new Hashtable();

        /// <summary>
        /// Word gebruikt om de definities op te slaan nadat ze zijn geladen.
        /// </summary>
        public static readonly Hashtable upgradeDefinitions = new Hashtable();

        /// <summary>
        /// Word gebruikt om de definitie op te slaan nadat het is geladen.
        /// </summary>
        public static PlayerDefinition playerDefinition;

        /// <summary>
        /// The current game state.
        /// </summary>
        public enum GameState
        {
            /// <summary>
            /// Main menu.
            /// </summary>
            Menu,
            /// <summary>
            /// When ingame and you press escape, you will get this state.
            /// </summary>
            Paused,
            /// <summary>
            /// The player is playing.
            /// </summary>
            InGame,  
            /// <summary>
            /// The player is in the upgrade interface.
            /// </summary>
            Upgrade,
            /// <summary>
            /// The player is in the highscores interface.
            /// </summary>
            Highscore,
            /// <summary>
            /// The player is in the selection screen.
            /// </summary>
            Select,
            /// <summary>
            ///  The player failed to win the game.
            /// </summary>
            GameOver
        }

        //Als dit java was, dan was dit een enum -.- Ccrap enums...
        /// <summary>
        /// Contains all level info.
        /// </summary>
        public class LevelInformation
        {
            /// <summary>
            /// Gets the waves.
            /// </summary>
            public static Dictionary<int, LevelInformation> levels { get; private set; }

            /// <summary>
            /// Loads this instance.
            /// </summary>
            public static void Load() {
                levels = new Dictionary<int, LevelInformation>();
                levels.Add(1, new LevelInformation(1, new int[] {1}, 10, 1500));
                levels.Add(2, new LevelInformation(2, new int[] {4, 5, 6}, 15, 1000));
                levels.Add(3, new LevelInformation(3, new int[] {7, 8, 9}, 20, 500));
            }

            /// <summary>
            /// Fors the value.
            /// </summary>
            /// <param name="wave">The wave.</param>
            /// <returns></returns>
            public static LevelInformation forValue(int wave)
            {
                LevelInformation levelInformation = null;
                if (!levels.TryGetValue(wave, out levelInformation)) {
                    return null;
                }
                return levelInformation;
            }

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
            /// Prevents a default instance of the <see cref="LevelInformation"/> class from being created.
            /// </summary>
            /// <param name="level">The wave.</param>
            /// <param name="npcTypes">The NPC types.</param>
            /// <param name="amountOfEnemies">The amount of enemies.</param>
            /// <param name="waveDelay">The wave delay.</param>
            private LevelInformation(int level, int[] npcTypes, int amountOfEnemies, int waveDelay)   //Wavedelay in ms
            {
                this.level = level;
                this.waveDelay = waveDelay;
                this.npcTypes = npcTypes;
                this.amountOfEnemies = amountOfEnemies;
                
            }
        }

        public string yesKnopGedrukt = "";
        public string highscoreMenu = "";

        private Vector2 mousePosition = Vector2.Zero;
        private GameState gameState = GameState.Menu;
        private GameState lastState = GameState.Menu;
        public Player player { get; private set; }
        private PlayerUpdate playerUpdate;
        public LinkedList<NPC> npcs { get; private set; }
        private NPCUpdate npcUpdate;
        private ProgramInformation info;
        private GameMap gameMap;
        public GameMap GameMap { get { return gameMap; } }
        public InterfaceManager IManager { get; private set; }
        public GameFrame gameFrame { get; set; }
        public HighScore highScores { get; private set; }
        public SqlDatabase dataBase;
        private ArduinoManager arduino;
        private TimeSpan lastWave = TimeSpan.Zero;
        public static Random random = new Random();
        private KeyboardHandler keyboardHandler;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        //public MouseState lastMouseState { get; private set; }

        //private Crosshair crosshair;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWorld"/> class.
        /// </summary>
        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //IsMouseVisible = true;

            //graphics.PreferMultiSampling = true;
            IsFixedTimeStep = false; //This disables the 60 ms interval of the Update method.
            graphics.SynchronizeWithVerticalRetrace = false; //This disables V-sync. (Lock frames to screen refresh rate)
            if (!DEBUG_MODE)
            {
                graphics.IsFullScreen = true;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.ApplyChanges();
            }
            else
            {
                this.IsMouseVisible = true;
                graphics.PreferredBackBufferHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 1.5);
                graphics.PreferredBackBufferWidth = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 1.5);
                graphics.ApplyChanges();
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameFrame.Width = graphics.PreferredBackBufferWidth;
            GameFrame.Height = graphics.PreferredBackBufferHeight;
            keyboardHandler = new KeyboardHandler(Window.Handle);
            keyboardHandler.Initialize();
            arduino = new ArduinoManager();
            LevelInformation.Load();
            npcs = new LinkedList<NPC>();
            IManager = new InterfaceManager();
            IManager.Initialize();
            gameFrame = new GameFrame();
            player = new Player();
            playerUpdate = new PlayerUpdate();
            npcUpdate = new NPCUpdate();
            info = new ProgramInformation();
            gameMap = new GameMap();
            gameMap.LoadGameObjects();
            dataBase = new SqlDatabase();            
            highScores = new HighScore();
            base.Initialize();
        }

        /// <summary>
        /// Creates a new game.
        /// </summary>
        public void newGame()
        {
            player = new Player();
            lock (npcs)
            {
                npcs.Clear();
                player.Lifes = Player.maxHP;
                player.gold = 0;
            }
            gameMap.ClearProjectiles(); //Remove existing projectiles.
            gameMap.LoadGameObjects(); //Reload game objects.
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            arduino.connect();
            newGame();
            playerUpdate.start();
            npcUpdate.start();
            CleanUp.INSTANCE.start();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            if (npcUpdate != null)
                npcUpdate.stop();
            lock (npcs)
            {
                foreach(NPC npc in npcs) {
                    npc.Dispose();
                }
            }
            gameMap.ClearProjectiles();
            if (playerUpdate != null)
                playerUpdate.stop();
            if (player != null)
                player.Dispose();
            arduino.stop();
            CleanUp.INSTANCE.stop();
            //Engine.getInstance().destroy();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Highscore:
                case GameState.GameOver:
                case GameState.Select:
                case GameState.Paused:
                case GameState.Upgrade:
                case GameState.Menu:
                    IManager.Update();
                    break;
                case GameState.InGame:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        CurrentGameState = GameState.Paused;
                        break;
                    }
                    lastWave += gameTime.ElapsedGameTime;
                    if (lastWave.TotalMilliseconds >= LevelInformation.forValue(player.currentLevel).waveDelay)
                    {
                        LevelInformation wave = LevelInformation.forValue(player.currentLevel);
                        int typeToSpawn = wave.npcTypes[random.Next(wave.npcTypes.Length)];
                        NPC npc = new NPC(typeToSpawn);
                        float maxY = GameFrame.Height - npc.definition.mainTexture.Height;
                        float y = random.Next(GameFrame.Height);
                        float minY = 0 - npc.definition.mainTexture.Height;
                        if (y < minY)
                            y = minY;
                        else if (y > maxY)
                            y = maxY;
                        npc.setLocation(new Vector2(-npc.definition.mainTexture.Width, y));
                        npcs.AddLast(npc);                       
                        lastWave = TimeSpan.Zero;
                    }
                    break;
            }
            info.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //LET OP! - De volgorde bepaalt de layer op het scherm. (Dus door een plaatje als eerste te drawen, krijg je een background)
            spriteBatch.Begin();
            gameMap.DrawBackground(spriteBatch);
            switch (gameState)
            {
                case GameState.Highscore:
                    IManager.Draw(spriteBatch);
                    highScores.Draw(spriteBatch);
                    if (player != null && player.visible)
                        player.Draw(spriteBatch);    
                    break;
                case GameState.GameOver:
                case GameState.Select:
                case GameState.Paused:
                case GameState.Menu:
                    IManager.Draw(spriteBatch);
                    if (player != null && player.visible)
                        player.Draw(spriteBatch);
                    break;
                case GameState.Upgrade:
                    IManager.Draw(spriteBatch);
                    gameFrame.DrawGold(spriteBatch);
                    if (player != null && player.visible)
                        player.Draw(spriteBatch);
                    break;
                case GameState.InGame:
                    gameMap.DrawObjects(spriteBatch);
                    gameFrame.Draw(spriteBatch);
                    npcUpdate.Draw(spriteBatch);
                    gameMap.DrawProjectiles(spriteBatch);
                    if (player != null && player.visible)
                        player.Draw(spriteBatch);
                    break;
                    
            }
            info.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }


        /// <summary>
        /// Gets or sets the state of the current game.
        /// </summary>
        /// <value>
        /// The state of the current game.
        /// </value>
        public GameState CurrentGameState { 
            get 
            {
                return gameState; 
            } 
            set 
            {
                this.lastState = this.gameState;
                this.gameState = value;
                switch (gameState)
                {
                    case GameState.InGame:
                        if (lastState == GameState.Paused)
                        {
                            removeInterfaceComponent(IManager.upgrade);
                            removeInterfaceComponent(IManager.resume);
                        }
                        break;

                    case GameState.Paused:
                        IManager.getActiveComponents().Clear();
                        IManager.activeInterface = null;
                        addInterfaceComponent(IManager.resume, true);
                        addInterfaceComponent(IManager.newGame, false);
                        addInterfaceComponent(IManager.highScore, false);
                        addInterfaceComponent(IManager.mainMenu, false);
                        addInterfaceComponent(IManager.upgrade, false);
                        IManager.updateButtons();
                        break;

                    case GameState.Menu:
                        IManager.getActiveComponents().Clear();
                        IManager.activeInterface = null;
                        addInterfaceComponent(IManager.newGame, true);
                        addInterfaceComponent(IManager.highScore, false);
                        addInterfaceComponent(IManager.quit, false);
                        IManager.updateButtons();
                        break;

                    case GameState.Upgrade:
                        IManager.getActiveComponents().Clear();
                        IManager.activeInterface = IManager.upgradeAchtergrond;
                        addInterfaceComponent(IManager.menuButtonKlein, true);
                        addInterfaceComponent(IManager.resumeKlein, false);
                        addInterfaceComponent(IManager.upgradeEcoWall, false);
                        //IManager.updateButtons();
                        break;

                    case GameState.Highscore:
                        IManager.getActiveComponents().Clear();
                        IManager.activeInterface = IManager.highscoreDisplay;
                        addInterfaceComponent(IManager.menuButtonKlein, false);
                        break;

                    case GameState.Select:
                        IManager.getActiveComponents().Clear();
                        IManager.activeInterface = IManager.yesNoSelect;
                        addInterfaceComponent(IManager.noButton, false);
                        addInterfaceComponent(IManager.yesButton, false);
                        break;

                    case GameState.GameOver:
                        IManager.getActiveComponents().Clear();
                        IManager.activeInterface = null;
                        addInterfaceComponent(IManager.menuButtonKlein, true);
                        addInterfaceComponent(IManager.submitHighscore, false);                        
                        IManager.updateButtons();
                        break;
                }
            }
        }

        /// <summary>
        /// Removes the button.
        /// </summary>
        /// <param name="button">The button.</param>
        private void removeInterfaceComponent(InterfaceComponent button)
        {
            if (IManager.getActiveComponents().Find(button) != null)
            {
                IManager.getActiveComponents().Remove(button);
            }
        }

        /// <summary>
        /// Adds the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="first">if set to <c>true</c> [first].</param>
        private void addInterfaceComponent(InterfaceComponent button, bool first)
        {
            if (IManager.getActiveComponents().Find(button) == null)
            {
                if (first)
                    IManager.getActiveComponents().AddFirst(button);
                else
                    IManager.getActiveComponents().AddLast(button);
            }
        }

        /// <summary>
        /// Gets the last state of the game.
        /// </summary>
        /// <value>
        /// The last state of the game.
        /// </value>
        public GameState LastGameState
        {
            get
            {
                return lastState;
            }
            private set
            {
            }
        }

        /// <summary>
        /// Loads the texture.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>The loaded texture</returns>
        public Texture2D loadTexture(string location)
        {
            Texture2D texture = null;
            try
            {
                texture = Content.Load<Texture2D>(location);
            } 
            catch (Exception e) 
            {
                Console.Error.WriteLine("Can not load " + location + ". Please make sure the input path is correct and that the image exists.");  
            }
            if (texture != null)
                Console.WriteLine("Loaded image: " + location);
            return texture;
        }
    }
}
