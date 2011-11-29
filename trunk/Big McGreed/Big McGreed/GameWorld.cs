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
using Big_McGreed.content.mouse;


namespace Big_McGreed
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameWorld : Microsoft.Xna.Framework.Game
    {
        private enum GameState
        {
            HoofdMenu,
            Menu,
            InGame
        }

        private Vector2 mousePosition = Vector2.Zero;
        private GameState gameState = GameState.HoofdMenu;
        private GameState lastState = GameState.HoofdMenu;
        private Player player = null;
        private PlayerUpdate playerUpdate = null;
        private LinkedList<NPC> npcs = new LinkedList<NPC>();
        private NPCUpdate npcUpdate = null;

        private GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        private Crosshair crosshair;

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new Player();
            player.definition = PlayerDefinition.loadDefinition();
            playerUpdate = new PlayerUpdate();
            npcUpdate = new NPCUpdate();
            crosshair = new Big_McGreed.content.mouse.Crosshair();
            IsMouseVisible = true;

            //this.graphics.PreferredBackBufferWidth = 1280;
            //this.graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = true;
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
            playerUpdate.start();
            npcUpdate.start();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
                    crosshair.crosshairLoad();
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            switch (gameState)
            {
                case GameState.HoofdMenu:
                    //Update hoofd menu.
                    break;
                case GameState.Menu:
                    //Update ingame menu.
                    break;
                case GameState.InGame:
                    //Update lopen ofzo, maar geen speler of npcs deze worden in een andere thread gedaan.
                    break;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            switch (gameState)
            {
                case GameState.HoofdMenu:
                    //Update hoofd menu.
                    break;
                case GameState.Menu:
                    //Update ingame menu.
                    break;
                case GameState.InGame:
                    if (player != null && player.definition.mainTexture != null)
                        player.Draw();
                    lock (npcs)
                    {
                        foreach (NPC npc in npcs)
                        {
                            if (npc.visible && npc.definition.mainTexture != null)
                            {
                                npc.Draw();
                            }
                        }
                    }
                    break;
                    
            }
            // MOUSE CROSSHAIR
                    crosshair.crosshairDraw();

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
    }
}
