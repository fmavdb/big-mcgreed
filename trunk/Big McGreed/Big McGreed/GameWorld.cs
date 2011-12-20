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
using Big_McGreed.content.fps;
using Big_McGreed.logic.map;
using Big_McGreed.content.menu;
using Big_McGreed.engine.misc;
using System.Collections;
using Big_McGreed.content.menu.buttons;
using Big_McGreed.content.gameframe;


namespace Big_McGreed
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameWorld : Microsoft.Xna.Framework.Game
    {
        //Word gebruikt om de definities op te slaan nadat ze zijn geladen.
        public static readonly Hashtable projectileDefinitions = new Hashtable();

        //Word gebruikt om de definities op te slaan nadat ze zijn geladen.
        public static readonly Hashtable npcDefinitions = new Hashtable();

        //Word gebruikt om de definities op te slaan nadat ze zijn geladen.
        public static readonly Hashtable objectDefinitions = new Hashtable();

        //Word gebruikt om de definities op te slaan nadat ze zijn geladen.
        public static readonly Hashtable upgradeDefinitions = new Hashtable();

        public static PlayerDefinition playerDefinition = null;

        public enum GameState
        {
            Menu,
            Paused,
            InGame,
            Upgrade,
            Select
        }

        public string yesKnopGedrukt = "";

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
        private Menu menu;
        private GameFrame gameFrame;

        private GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
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
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.ApplyChanges();
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
            gameFrame = new GameFrame();
            npcs = new LinkedList<NPC>();
            player = new Player();
            menu = new Menu();
            playerUpdate = new PlayerUpdate();
            npcUpdate = new NPCUpdate();
            info = new ProgramInformation();
            gameMap = new GameMap();
            newGame();
            playerUpdate.start();
            npcUpdate.start();
            //Engine.getInstance().start();
            CleanUp.INSTANCE.start();
            //Engine.getInstance().submitEvent(new Cleanup());
            base.Initialize();
        }

        /// <summary>
        /// Creates a new game.
        /// </summary>
        public void newGame()
        {
            lock (npcs)
            {
                npcs.Clear();
                int d = 0;
                for (int i = 0; i < 4; i++)
                {
                    NPC npc = new NPC(1);
                    npc.setLocation(new Vector2(0, d));
                    d += 150;
                    npcs.AddFirst(npc);
                }
            }
            gameMap.ClearProjectiles();
            //npc.setLocation(new Vector2(0, 100));
            //npcs.AddFirst(npc);
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
                    //crosshair.crosshairLoad();
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
                    npc.destroy();
                }
            }
            gameMap.ClearProjectiles();
            if (playerUpdate != null)
                playerUpdate.stop();
            if (player != null)
                player.destroy();
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
                case GameState.Select:
                case GameState.Paused:
                case GameState.Upgrade:
                case GameState.Menu:
                    menu.Update();
                    break;
                case GameState.InGame:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        CurrentGameState = GameState.Paused;
                    break;
            }
            info.update(gameTime);
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
            gameMap.DrawBackground();
            switch (gameState)
            {
                case GameState.Select:
                case GameState.Paused:
                case GameState.Upgrade:
                case GameState.Menu:
                    menu.Draw();
                    if (player != null && player.visible && player.definition.mainTexture != null)
                        player.Draw();
                    break;
                case GameState.InGame:
                    gameFrame.Draw();
                    gameMap.DrawObjects();
                    npcUpdate.Draw();
                    gameMap.DrawProjectiles();
                    if (player != null && player.visible && player.definition.mainTexture != null)
                        player.Draw();
                    break;
                    
            }
            info.draw();
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
                            removeButton(menu.upgrade);
                            removeButton(menu.resume);
                        }
                        break;

                    case GameState.Paused:
                        addButton(menu.resume, true);
                        addButton(menu.newGame, false);
                        addButton(menu.highScore, false);
                        addButton(menu.mainMenu, false);
                        addButton(menu.upgrade, false);
                        removeButton(menu.menuButtonKlein);
                        removeButton(menu.resumeKlein);
                        removeButton(menu.quit);
                        removeButton(menu.yesNoSelect);
                        removeButton(menu.yesButton);
                        removeButton(menu.noButton);
                        menu.updateButtons();
                        break;

                    case GameState.Menu:
                        removeButton(menu.resume);
                        removeButton(menu.upgrade);
                        removeButton(menu.menuButtonKlein);
                        removeButton(menu.resumeKlein);
                        removeButton(menu.mainMenu);
                        removeButton(menu.yesNoSelect);
                        removeButton(menu.yesButton);
                        removeButton(menu.noButton);
                         addButton(menu.newGame, true);
                        addButton(menu.highScore, false);
                        addButton(menu.quit, false);
                        menu.updateButtons();
                        break;

                    case GameState.Upgrade:
                        menu.getButtons().Clear();
                        addButton(menu.menuButtonKlein, true);
                        addButton(menu.resumeKlein, false);
                        removeButton(menu.yesNoSelect);
                        removeButton(menu.yesButton);
                        removeButton(menu.noButton);
                        menu.updateButtons();
                        break;

                    case GameState.Select:
                        removeButton(menu.resume);
                        removeButton(menu.upgrade);
                        removeButton(menu.mainMenu);
                        removeButton(menu.highScore);
                        removeButton(menu.newGame);
                        addButton(menu.yesNoSelect, false);
                        addButton(menu.noButton, false);
                        addButton(menu.yesButton, false);
                        break;
                }
            }
        }

        /// <summary>
        /// Removes the button.
        /// </summary>
        /// <param name="button">The button.</param>
        private void removeButton(Button button)
        {
            if (menu.getButtons().Find(button) != null)
            {
                menu.getButtons().Remove(button);
            }
        }

        /// <summary>
        /// Adds the button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="first">if set to <c>true</c> [first].</param>
        private void addButton(Button button, bool first)
        {
            if (menu.getButtons().Find(button) == null)
            {
                if (first)
                    menu.getButtons().AddFirst(button);
                else
                    menu.getButtons().AddLast(button);
            }
        }

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
    }
}
