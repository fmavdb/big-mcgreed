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
    public class Player : Entity, Destroyable
    {
        public static int maxhp = 100;

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

        public bool muzzle = false;

        Texture2D muzzleTexture = Program.INSTANCE.Content.Load<Texture2D>("Muzzle Effect");

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            Lifes = maxhp;
            visible = true;
            setX(Mouse.GetState().X);
            setY(Mouse.GetState().Y);
            boerLocatie.X = Program.INSTANCE.gameFrame.boerderijPositie.X * 1.115f;
            boerLocatie.Y = Program.INSTANCE.gameFrame.boerderijPositie.Y * 1.8f;
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
                        hit(new Hit(this, null, 10));
                        leftButtonPressed = true;
                        muzzle = true;
                    }
                }
            }
            else
            {
                leftButtonPressed = false;
            }
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                lock (Program.INSTANCE.npcs)
                {
                    foreach (NPC npc in Program.INSTANCE.npcs)
                    {
                        npc.visible = true;
                    }
                }
            }
            //if (Mouse.GetState().X != lastMousePosition.X || Mouse.GetState().Y != lastMousePosition.Y)
            //{
                //lastMousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                //lastMousePosition.Normalize();
                //setLocation(new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)));
                setLocation(PrimitivePathFinder.getPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), definition.mainTexture.Width, definition.mainTexture.Height, 2));
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
                    if (muzzle)
                    {
                        Program.INSTANCE.spriteBatch.Draw(muzzleTexture, geweerLocatie, Color.White);
                        muzzle = false;
                    }
                    rotation = (float)Math.Atan2(geweerLocatie.Y - Mouse.GetState().Y, geweerLocatie.X - Mouse.GetState().X);
                    Program.INSTANCE.spriteBatch.Draw(definition.revolverTexture, geweerLocatie, new Rectangle(0, 0, definition.revolverTexture.Width, definition.revolverTexture.Height), Color.White, rotation, new Vector2(definition.revolverTexture.Width, definition.revolverTexture.Height), 0.10f, SpriteEffects.None, 1.0f);
                    break;
            }
        }
    }
}
