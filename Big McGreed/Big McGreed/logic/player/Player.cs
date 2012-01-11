﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.utility;
using Microsoft.Xna.Framework;
using Big_McGreed.logic.map;
using Microsoft.Xna.Framework.Input;
using Big_McGreed.logic.npc;
using Big_McGreed.logic.mask;
using Microsoft.Xna.Framework.Graphics;
using Big_McGreed.logic.projectile;
using Big_McGreed.content.upgrades;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.logic.player
{
    /// <summary>
    /// This class represents a player, which is also an entity.
    /// </summary>
    public class Player : Entity, Destroyable
    {
        public static int maxHP = 100;

        private Upgrade wall = null;

        private Upgrade weapon = null;

        //De grootte van de 'dot' van de crosshair, stelt dotSize(width) x dotSize(height) pixels voor.
        public const int dotSize = 5;

        public PlayerDefinition definition { get { return PlayerDefinition.getDefinition(); } }

        public int currentWave { get; set; }

        public bool leftButtonPressed = false;

        private Vector2 boerLocatie = Vector2.Zero;
        public Vector2 BoerLocatie { get { return boerLocatie; } private set { boerLocatie = value; } }

        private Vector2 geweerLocatie = Vector2.Zero;

        public Vector2 GeweerLocatie { get { return geweerLocatie; } }

        private float rotation;

        public int gold { get; set; }
        public int lastGold { get; set; }

        public int muzzle = 0;

        Texture2D muzzleTexture = Program.INSTANCE.Content.Load<Texture2D>("Muzzle Effect");

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            Lifes = maxHP;
            visible = true;
            currentWave = 1;
            setX(Mouse.GetState().X);
            setY(Mouse.GetState().Y);
            boerLocatie.X = GameFrame.Width - Program.INSTANCE.gameFrame.boerderijTexture.Width / 3.1f;
            boerLocatie.Y = GameFrame.Height - Program.INSTANCE.gameFrame.boerderijTexture.Height / 1.15f;
            geweerLocatie.X = boerLocatie.X;
            geweerLocatie.Y = boerLocatie.Y + definition.personTexture.Height / 2;
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void destroy()
        {
            destroyed = true;
        }

        /// <summary>
        /// Run2s this instance.
        /// </summary>
        protected override void run2()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (!leftButtonPressed)
                {
                    //foreach (NPC npc in PrimitivePathFinder.collision(this, Mouse.GetState().X, Mouse.GetState().Y))
                    //{
                        //npc.hit(new Hit(npc, this, 10));
                    //}
                    if (Program.INSTANCE.CurrentGameState == GameWorld.GameState.InGame)
                    {
                        Program.INSTANCE.GameMap.AddProjectile(new Projectile(1, new Hit(null, this, 10), new Vector2(Mouse.GetState().X, Mouse.GetState().Y)));
                        //hit(new Hit(this, null, 10));
                        leftButtonPressed = true;
                        muzzle = 12;
                    }
                }
            }
            else
            {
                leftButtonPressed = false;
            }
            /*if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                lock (Program.INSTANCE.npcs)
                {
                    foreach (NPC npc in Program.INSTANCE.npcs)
                    {
                        npc.visible = true;
                    }
                }
            }*/
            //if (Mouse.GetState().X != lastMousePosition.X || Mouse.GetState().Y != lastMousePosition.Y)
            //{
                //lastMousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                //lastMousePosition.Normalize();
                //setLocation(new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)));
                setLocation(PrimitivePathFinder.getCrossHairPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), definition.mainTexture.Width, definition.mainTexture.Height));
            //}
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public override void Draw()
        {
            Program.INSTANCE.spriteBatch.Draw(definition.mainTexture, getLocation(), Color.Black);
            switch (Program.INSTANCE.CurrentGameState)
            {
                case GameWorld.GameState.InGame:
                    if (muzzle > 0)
                    {
                        Program.INSTANCE.spriteBatch.Draw(muzzleTexture, geweerLocatie, new Rectangle(0, 0, muzzleTexture.Width, muzzleTexture.Height), Color.White, rotation, new Vector2(muzzleTexture.Width, muzzleTexture.Height), 1.0f, SpriteEffects.None, 1.0f);
                        muzzle--;
                    }
                    rotation = (float)Math.Atan2(geweerLocatie.Y - Mouse.GetState().Y, geweerLocatie.X - Mouse.GetState().X);
                    Program.INSTANCE.spriteBatch.Draw(definition.revolverTexture, geweerLocatie, new Rectangle(0, 0, definition.revolverTexture.Width, definition.revolverTexture.Height), Color.White, rotation, new Vector2(definition.revolverTexture.Width, definition.revolverTexture.Height), 1.0f, SpriteEffects.None, 1.0f);
                    break;
            }
        }
    }
}
