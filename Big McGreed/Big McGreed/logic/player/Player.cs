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

namespace Big_McGreed.logic.player
{
    public class Player : Entity, Destroyable
    {
        //De grootte van de 'dot' van de crosshair, stelt dotSize(width) x dotSize(height) pixels voor.
        public const int dotSize = 5;

        public PlayerDefinition definition { get; set; }

        public int currentLevel { get; set; }

        public bool leftButtonPressed = false;

        private Vector2 boerLocatie = Vector2.Zero;

        private Vector2 geweerLocatie = Vector2.Zero;

        public Vector2 GeweerLocatie { get { return geweerLocatie; } }

        private float rotation;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player(PlayerDefinition definition)
        {
            this.definition = definition;
            visible = true;
            setX(Mouse.GetState().X);
            setY(Mouse.GetState().Y);
            boerLocatie.X = Program.INSTANCE.Width - (Program.INSTANCE.Width / 4);
            boerLocatie.Y = Program.INSTANCE.Height - (Program.INSTANCE.Height / 4);
            geweerLocatie.X = boerLocatie.X;
            geweerLocatie.Y = boerLocatie.Y + definition.personTexture.Height / 2;
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void destroy()
        {
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
                    Program.INSTANCE.GameMap.AddProjectile(new Projectile(1, new Hit(null, this, 10), new Vector2(Mouse.GetState().X, Mouse.GetState().Y)));
                    leftButtonPressed = true;
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
                setLocation(PrimitivePathFinder.getPosition(Mouse.GetState().X, Mouse.GetState().Y, definition.mainTexture.Width, definition.mainTexture.Height, 2));
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
                    rotation = (float)Math.Atan2(geweerLocatie.Y - Mouse.GetState().Y, geweerLocatie.X - Mouse.GetState().X);
                    Program.INSTANCE.spriteBatch.Draw(definition.personTexture, boerLocatie, Color.Black);
                    Program.INSTANCE.spriteBatch.Draw(definition.revolverTexture, geweerLocatie, new Rectangle(0, 0, definition.revolverTexture.Width, definition.revolverTexture.Height), Color.White, rotation, new Vector2(definition.revolverTexture.Width, definition.revolverTexture.Height), 0.10f, SpriteEffects.None, 1.0f);
                    break;
            }
        }
    }
}
