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
    public class Player : Entity, IDisposable
    {
        public static int maxHP = 100;

        public static int maxOil = 100;

        public int electricWallTimer { get; set; }

        public static bool drawCrosshair = true;

        private DateTime lastShot;

        public double damageFactor = 1.00;

        /// <summary>
        /// Gets or sets the wall.
        /// </summary>
        /// <value>
        /// The wall.
        /// </value>
        public Upgrade Wall { get; set; }

        private Upgrade weapon;

        public Upgrade boerderij;

        /// <summary>
        /// Gets or sets the weapon.
        /// </summary>
        /// <value>
        /// The weapon.
        /// </value>
        public Upgrade Weapon 
        {
            get { return weapon; }

            set
            {                
                this.weapon = value;
                this.Damage = Weapon.definition.damage;
            }
        }

        /// <summary>
        /// Gets the definition.
        /// </summary>
        public PlayerDefinition definition { get { return PlayerDefinition.getDefinition(); } }

        /// <summary>
        /// Gets the current level.
        /// </summary>
        public int currentLevel { get; set; }

        /// <summary>
        /// Wether the left mouse button has been pressed.
        /// </summary>
        public bool leftButtonPressed = false;

        private Vector2 boerLocatie = Vector2.Zero;

        /// <summary>
        /// Gets the boer locatie.
        /// </summary>
        public Vector2 BoerLocatie { get { return boerLocatie; } private set { boerLocatie = value; } }

        static Texture2D muzzleTexture = Program.INSTANCE.loadTexture("Muzzle Effect");

        public bool evilWall { get; set; }

        private float rotation;

        public int gold { get; set; }

        public int oil { get; set; }

        public int Score { get; set; }

        public string naam = "";

        public int muzzle = 0;

        public int kills = 0;

        public int good = 0;
        public int evil = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            damageFactor = 1.00;
            evilWall = false;
            maxHP = 100;
            oil = maxOil;
            electricWallTimer = -1;
            Lifes = maxHP;
            oil = maxOil;
            lastShot = DateTime.Now;
            naam = "";
            good = 0;
            evil = 0;
            UpdateCrosshair();
            Program.INSTANCE.gameFrame.UpdateOil(oil);
            visible = true;
            currentLevel = 1;
            setX(Mouse.GetState().X);
            setY(Mouse.GetState().Y);
            boerLocatie.X = GameFrame.Width - Program.INSTANCE.gameFrame.boerderijTexture.Width / 3f;
            boerLocatie.Y = GameFrame.Height - Program.INSTANCE.gameFrame.boerderijTexture.Height / 1.12f;
            Weapon = new Upgrade(this, new Vector2(boerLocatie.X + definition.personTexture.Width / 1.75f, boerLocatie.Y + definition.personTexture.Height / 1.55f), "weapon", 2); //1ste wapen is revolver: naam + level dus: weapon0.png
            Wall = new Upgrade(this, new Vector2(Program.INSTANCE.gameFrame.boerderijPositie.X - UpgradeDefinition.forName("muur0").mainTexture.Width , GameFrame.Height - UpgradeDefinition.forName("muur0").mainTexture.Height * 1.15f), "muur", 1);
            boerderij = new Upgrade(this, Vector2.Zero, "Boerderij", 1);
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Dispose()
        {
            disposed = true;
        }

        /// <summary>
        /// Run2s this instance.
        /// </summary>
        protected override void run2()
        {
            if (electricWallTimer > 0)
            {
                electricWallTimer--;
            }
            else if (electricWallTimer == 0)
            {
                Wall.definition.ChangeTextureBackToNormal();
                electricWallTimer = -1;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (!leftButtonPressed)
                {
                    if (Program.INSTANCE.CurrentGameState == GameWorld.GameState.InGame)
                    {
                        TimeSpan hitTimePassed = DateTime.Now - lastShot;
                        if (hitTimePassed.TotalMilliseconds >= Weapon.definition.weaponSpeed)
                        {
                            Program.INSTANCE.gameMap.AddProjectile(new Projectile(1, new Hit(null, this, Damage), new Vector2(Mouse.GetState().X, Mouse.GetState().Y + definition.mainTexture.Height / 8)));
                            //hit(new Hit(this, null, 10));
                            leftButtonPressed = true;
                            muzzle = 12;
                            lastShot = DateTime.Now;
                        }
                    }
                }
            }
            else
            {
                leftButtonPressed = false;
            }
            setLocation(PrimitivePathFinder.getCrossHairPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), definition.mainTexture.Width, definition.mainTexture.Height));
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        /// <param name="batch">The batch</param>
        public override void Draw(SpriteBatch batch)
        {
            switch(Program.INSTANCE.CurrentGameState)
            {
                case GameWorld.GameState.InGame:
                    if (muzzle > 0)
                    {
                        batch.Draw(muzzleTexture, weapon.getLocation(), new Rectangle(0, 0, muzzleTexture.Width, muzzleTexture.Height), Color.White, rotation, new Vector2(weapon.definition.mainTexture.Width * (weapon.getLevel() == 0 ? 2.10f : 1.75f), Weapon.definition.mainTexture.Height * 0.35f), 1.0f, SpriteEffects.None, 1.0f);
                        muzzle--;
                    }
                    rotation = (float)Math.Atan2(Weapon.getY() - Mouse.GetState().Y * 0.96, Weapon.getX() - Mouse.GetState().X);
                    batch.Draw(Weapon.definition.mainTexture, Weapon.getLocation(), new Rectangle(0, 0, Weapon.definition.mainTexture.Width, Weapon.definition.mainTexture.Height), Color.White, rotation, new Vector2(Weapon.definition.mainTexture.Width, 0), 1.0f, SpriteEffects.None, 1.0f);
                    batch.Draw(Wall.definition.mainTexture, Wall.getLocation(), Color.White);
                    break;
            }
            if (drawCrosshair)
            {
                batch.Draw(definition.mainTexture, getLocation(), Color.White);
            }
        }

        public void UpdateCrosshair()
        {
            int conclusion = good - evil; // Hoger dan 1 is groen, 0 is normaal, lager dan 0 is olie
            if (conclusion > 0)
            {
                GameWorld.playerDefinition.mainTexture = Program.INSTANCE.loadTexture("crosshairGood");
            }
            else if (conclusion == 0)
            {
                GameWorld.playerDefinition.mainTexture = Program.INSTANCE.loadTexture("crosshairNormal");
            }
            else
            {
                GameWorld.playerDefinition.mainTexture = Program.INSTANCE.loadTexture("crosshairEvil");
            }
        }
    }
}
