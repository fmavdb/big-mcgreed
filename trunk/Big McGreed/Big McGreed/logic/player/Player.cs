using System;
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

        public Upgrade Wall { get; set; }

        public Upgrade Weapon { get; set; }

        public PlayerDefinition definition { get { return PlayerDefinition.getDefinition(); } }

        public int currentLevel { get; set; }

        public bool leftButtonPressed = false;

        private Vector2 boerLocatie = Vector2.Zero;
        public Vector2 BoerLocatie { get { return boerLocatie; } private set { boerLocatie = value; } }

        private float rotation;

        public int gold { get; set; }

        public int oil { get; set; }

        public int Score { get; set; }

        public int muzzle = 0;

        Texture2D muzzleTexture = Program.INSTANCE.loadTexture("Muzzle Effect");

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            Lifes = maxHP;
            oil = maxOil;
            visible = true;
            currentLevel = 1;
            setX(Mouse.GetState().X);
            setY(Mouse.GetState().Y);
            boerLocatie.X = GameFrame.Width - Program.INSTANCE.gameFrame.boerderijTexture.Width / 3.1f;
            boerLocatie.Y = GameFrame.Height - Program.INSTANCE.gameFrame.boerderijTexture.Height / 1.15f;
            Weapon = new Upgrade(this, new Vector2(boerLocatie.X, boerLocatie.Y + definition.personTexture.Height / 2), "weapon"); //1ste wapen is revolver: naam + level dus: weapon0.png
            Wall = new Upgrade(Program.INSTANCE.player, new Vector2(Program.INSTANCE.gameFrame.boerderijPositie.X - UpgradeDefinition.forName("Muur0").mainTexture.Width / 2 , GameFrame.Height - UpgradeDefinition.forName("Muur0").mainTexture.Height), "Muur");
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
                        Program.INSTANCE.gameMap.AddProjectile(new Projectile(1, new Hit(null, this, 10), new Vector2(Mouse.GetState().X, Mouse.GetState().Y + definition.mainTexture.Height / 3)));
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
            setLocation(PrimitivePathFinder.getCrossHairPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), definition.mainTexture.Width, definition.mainTexture.Height));
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        /// <param name="batch">The batch</param>
        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(definition.mainTexture, getLocation(), Color.White);
            switch(Program.INSTANCE.CurrentGameState)
            {
                case GameWorld.GameState.InGame:
                    if (muzzle > 0)
                    {
                        batch.Draw(muzzleTexture, Weapon.getLocation(), new Rectangle(0, 0, muzzleTexture.Width, muzzleTexture.Height), Color.White, rotation, new Vector2(muzzleTexture.Width, muzzleTexture.Height), 1.0f, SpriteEffects.None, 1.0f);
                        muzzle--;
                    }
                    rotation = (float)Math.Atan2(Weapon.getY() - Mouse.GetState().Y - 50, Weapon.getX() - Mouse.GetState().X);
                    batch.Draw(Weapon.definition.mainTexture, Weapon.getLocation(), new Rectangle(0, 0, Weapon.definition.mainTexture.Width, Weapon.definition.mainTexture.Height), Color.White, rotation, new Vector2(Weapon.definition.mainTexture.Width, Weapon.definition.mainTexture.Height), 1.0f, SpriteEffects.None, 1.0f);
                    batch.Draw(Wall.definition.mainTexture, Wall.getLocation(), Color.White);
                    break;
            }
        }
    }
}
