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


namespace Big_McGreed
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameWorld : Microsoft.Xna.Framework.Game
    {
        public enum GameState
        {
            Menu,
            Paused,
            InGame
        }

        //Width van de application
        public int Width;
        //Hoogte van de application
        public int Height;

        private Vector2 mousePosition = Vector2.Zero;
        private GameState gameState = GameState.Menu;
        private GameState lastState = GameState.Menu;
        private Player player;
        private PlayerUpdate playerUpdate;
        private LinkedList<NPC> npcs = new LinkedList<NPC>();
        private NPCUpdate npcUpdate;
        private FPS fps;
        private GameMap gameMap;
        private Menu menu;

        private GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        //private Crosshair crosshair;

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;

            graphics.PreferMultiSampling = true;
            //graphics.IsFullScreen = true;
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
            Width = Program.INSTANCE.GraphicsDevice.Viewport.Width;
            Height = Program.INSTANCE.GraphicsDevice.Viewport.Height;
            player = new Player();
            player.definition = PlayerDefinition.loadDefinition();
            menu = new Menu();
            playerUpdate = new PlayerUpdate();
            npcUpdate = new NPCUpdate();
            fps = new FPS();
            gameMap = new GameMap();
            newGame();
            playerUpdate.start();
            npcUpdate.start();
            base.Initialize();
        }

        public void newGame()
        {
            npcs.Clear();
            int d = 50;
            for (int i = 0; i < 100; i++)
            {
                NPC npc = new NPC(1);
                npc.setLocation(new Vector2(0, d));
                d += 3;
                npcs.AddFirst(npc);
            }
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
            if (playerUpdate != null)
                playerUpdate.stop();
            if (player != null)
                player.destroy();
            Engine.getInstance().destroy();
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
                case GameState.Paused:
                case GameState.Menu:
                    menu.Update();
                    break;
                case GameState.InGame:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        setGameState(GameState.Paused);
                    break;
            }
            fps.update(gameTime);
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
                case GameState.Paused:
                case GameState.Menu:
                    menu.Draw();
                    break;
                case GameState.InGame:
                    gameMap.Draw();
                    if (player != null &&  player.visible && player.definition.mainTexture != null)
                        player.Draw();
                    npcUpdate.Draw();
                    break;
                    
            }
            fps.draw();
            spriteBatch.End();
            base.Draw(gameTime);
        } 

        public Player getPlayer()
        {
            return player;
        }

        public LinkedList<NPC> getNPCs()
        {
            return npcs;
        }

        public GameState getGameState() {
            return gameState;
        }

        public void setGameState(GameState gameState)
        {
            this.lastState = this.gameState;
            this.gameState = gameState;
            switch(gameState) {
                case GameState.InGame:
                    if (lastState == GameState.Paused)
                    {
                        menu.getButtons().Remove(menu.resume);
                        menu.updateButtons();
                    }
                    break;
                case GameState.Paused:
                    menu.getButtons().AddFirst(menu.resume);
                    menu.updateButtons();
                    break;
                case GameState.Menu:
                    menu.getButtons().Remove(menu.resume);
                    menu.updateButtons();
                    break;
            }
        }
    }
}
