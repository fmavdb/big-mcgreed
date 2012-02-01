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
using Big_McGreed.content.level;
using XNAGifAnimation;
using System.IO;
using Big_McGreed.logic.mask;
using Big_McGreed.content.time;

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

        public string buttonClickedState = "";
        public bool highscoreNameInUse = false;

        private Vector2 mousePosition = Vector2.Zero;
        private GameState gameState = GameState.Menu;
        private GameState lastState = GameState.Menu;
        /// <summary>
        /// Gets the player.
        /// </summary>
        public Player player { get; private set; }
        private PlayerUpdate playerUpdate;
        /// <summary>
        /// Gets the NPCS.
        /// </summary>
        public LinkedList<NPC> npcs { get; private set; }
        private NPCUpdate npcUpdate;
        private ProgramInformation info;
        /// <summary>
        /// Gets the game map.
        /// </summary>
        public GameMap gameMap { get; private set; }
        /// <summary>
        /// Gets the Interface manager.
        /// </summary>
        public InterfaceManager IManager { get; private set; }
        /// <summary>
        /// Gets the game frame.
        /// </summary>
        public GameFrame gameFrame { get; private set; }
        /// <summary>
        /// Gets the high scores.
        /// </summary>
        public HighScore highScores { get; private set; }
        /// <summary>
        /// Gets the data base.
        /// </summary>
        public SqlDatabase dataBase { get; private set; }
        /// <summary>
        /// Gets the arduino.
        /// </summary>
        public ArduinoManager arduino { get; private set; }
        private TimeSpan lastWave = TimeSpan.Zero;
        public static Random random = new Random();
        private InputHandler inputHandler;

        private GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        private GifAnimation animation;

        private Time time;

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
           //Components.Add(new GamerServicesComponent(this)); //Games for windows live ^^
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
            inputHandler = new InputHandler(Window.Handle);
            inputHandler.Initialize();
            dataBase = new SqlDatabase();   
            arduino = new ArduinoManager();
            npcs = new LinkedList<NPC>();
            IManager = new InterfaceManager();
            IManager.Initialize();
            gameFrame = new GameFrame();
            playerUpdate = new PlayerUpdate();
            npcUpdate = new NPCUpdate();
            info = new ProgramInformation();
            gameMap = new GameMap();
            gameMap.LoadGameObjects();         
            highScores = new HighScore();
            time = new Time();
            LevelInformation.Load();
            base.Initialize();
        }

        /// <summary>
        /// Creates a new game.
        /// </summary>
        public void newGame()
        {
            player = new Player();
            time.Reset();
            time.Start();
            IManager.gameOverInterface.gameWon = false;
            lock (npcs)
            {
                npcs.Clear();
            }
            //Reset all levels.
            int level = 1;
            LevelInformation levelInfo = LevelInformation.forValue(level);
            while (levelInfo != null)
            {
                levelInfo.Reset();
                level++;
                levelInfo = LevelInformation.forValue(level);
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
            player = new Player(); //Voor crosshair
            //animation = Content.Load<GifAnimation>("wolfanim");
            highScores.LoadContent();
            arduino.connect();
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
            dataBase.Dispose();
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
            //animation.Update(gameTime.ElapsedGameTime.Ticks);
            foreach (NPC npc in new LinkedList<NPC>(npcs))
            {
                npc.Update(gameTime);
            }
            switch (gameState)
            {
                case GameState.GameOver:
                case GameState.Highscore:
                case GameState.Select:
                case GameState.Paused:
                case GameState.Upgrade:
                case GameState.Menu:
                    IManager.Update();
                    break;
                case GameState.InGame:
                    IManager.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        CurrentGameState = GameState.Paused;
                        break;
                    } 
                    else if (Keyboard.GetState().IsKeyDown(Keys.PrintScreen))
                    {
                        ScreenShot(DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                    }
                    lastWave += gameTime.ElapsedGameTime;
                    if (lastWave.TotalMilliseconds >= LevelInformation.forValue(player.currentLevel).waveDelay)
                    {
                        LevelInformation wave = LevelInformation.forValue(player.currentLevel);
                        if (!wave.bossSpawned)
                        {
                            bool spawnBoss = wave.currentSpawnedEnemiesAmount >= wave.amountOfEnemies && !wave.bossSpawned;
                            if (spawnBoss)
                                wave.bossSpawned = spawnBoss;
                            int typeToSpawn = spawnBoss ? wave.bossType : wave.npcTypes[random.Next(wave.npcTypes.Length)];
                            NPC npc = new NPC(typeToSpawn);
                            float maxY = GameFrame.Height - npc.definition.mainTexture.Height - 50;
                            float minY = GameFrame.Height / 2;
                            float y = random.Next((int)minY, (int)maxY);
                            if (y < minY)
                                y = minY;
                            else if (y > maxY)
                                y = maxY;
                            npc.setLocation(new Vector2(-npc.definition.mainTexture.Width, y));
                            npcs.AddLast(npc);
                            wave.currentSpawnedEnemiesAmount++;
                        }
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
            //spriteBatch.Draw(this.animation.GetTexture(), new Vector2(GameFrame.Width / 2, 0), Color.White);
            switch (gameState)
            {
                case GameState.Highscore:
                    IManager.Draw(spriteBatch);
                    highScores.Draw(spriteBatch);
                    if (player != null && player.visible)
                        player.Draw(spriteBatch);    
                    break;
                case GameState.GameOver:
                    IManager.Draw(spriteBatch);
                    time.Draw(spriteBatch);
                    if (player != null && player.visible)
                        player.Draw(spriteBatch);
                    if (highscoreNameInUse)
                        highScores.NameInUse(spriteBatch);
                    spriteBatch.DrawString(IManager.font, "Score: " + Program.INSTANCE.player.Score, new Vector2(GameFrame.Width / 2 - IManager.font.MeasureString("Score: " + Program.INSTANCE.player.Score).X / 2, IManager.gameOverInterface.getY() + IManager.gameOverInterface.mainTexture.Height * 1.2f), Color.White);
                    break;
                case GameState.Select:
                case GameState.Paused:
                case GameState.Menu:
                    IManager.Draw(spriteBatch);
                    if (gameState == GameState.Paused || gameState == GameState.Select) //Mag niet bij menu
                    {
                        time.Draw(spriteBatch);
                    }
                    if (player != null && player.visible)
                        player.Draw(spriteBatch);
                    break;
                case GameState.Upgrade:
                    IManager.Draw(spriteBatch);
                    time.Draw(spriteBatch);
                    gameFrame.DrawGold(spriteBatch);
                    if (player != null && player.visible)
                        player.Draw(spriteBatch);
                    break;
                case GameState.InGame:
                    gameMap.DrawObjects(spriteBatch);
                    gameFrame.Draw(spriteBatch);
                    IManager.Draw(spriteBatch);
                    time.Draw(spriteBatch);
                    if (player != null && player.visible)
                        player.Draw(spriteBatch);
                    npcUpdate.Draw(spriteBatch);
                    gameMap.DrawProjectiles(spriteBatch);
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
                        time.Start();
                        IManager.getActiveComponents().Clear();
                        IManager.activeInterface = null;
                        addInterfaceComponent(IManager.upgradeButtonIG, true);
                        break;

                    case GameState.Paused:
                        time.Pause();
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
                        time.Pause();
                        IManager.getActiveComponents().Clear();
                        IManager.activeInterface = IManager.upgradeAchtergrond;
                        addInterfaceComponent(IManager.menuButtonKlein, true);
                        addInterfaceComponent(IManager.resumeKlein, false);
                        addInterfaceComponent(IManager.upgradeWeaponMagnum, false);
                        addInterfaceComponent(IManager.upgradeWeaponRifle, false);
                        addInterfaceComponent(IManager.upgradeWeaponShotgun, false);
                        addInterfaceComponent(IManager.upgradeEcoWall, false);
                        addInterfaceComponent(IManager.upgradeEcoHelp, false);
                        addInterfaceComponent(IManager.upgradeOilWall, false);
                        addInterfaceComponent(IManager.upgradeOilHelp, false);
                        break;

                    case GameState.Highscore:
                        IManager.getActiveComponents().Clear();
                        IManager.activeInterface = IManager.highscoreDisplay;
                        addInterfaceComponent(IManager.menuButtonKlein, false);
                        break;

                    case GameState.Select:
                        time.Pause();
                        IManager.getActiveComponents().Clear();
                        IManager.activeInterface = IManager.yesNoSelect;
                        addInterfaceComponent(IManager.noButton, false);
                        addInterfaceComponent(IManager.yesButton, false);
                        break;

                    case GameState.GameOver:
                        time.Pause();
                        IManager.getActiveComponents().Clear();
                        IManager.activeInterface = IManager.gameOverInterface;
                        addInterfaceComponent(IManager.menuButtonKlein, true);
                        addInterfaceComponent(IManager.submitHighscore, false);                        
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

        public void ScreenShot(string prefix)
        {
            int w = GraphicsDevice.PresentationParameters.BackBufferWidth;
            int h = GraphicsDevice.PresentationParameters.BackBufferHeight;

            //force a frame to be drawn (otherwise back buffer is empty) 
            Draw(new GameTime());

            //pull the picture from the buffer 
            int[] backBuffer = new int[w * h];
            GraphicsDevice.GetBackBufferData(backBuffer);

            //copy into a texture 
            Texture2D texture = new Texture2D(GraphicsDevice, w, h, false, GraphicsDevice.PresentationParameters.BackBufferFormat);
            texture.SetData(backBuffer);

            //save to disk 
            Stream stream = File.OpenWrite(prefix + ".png");
            texture.SaveAsPng(stream, w, h);
            texture.Dispose();
            stream.Close();
            Console.WriteLine("Screenshot saved in the root directory.");
        }
    }
}
