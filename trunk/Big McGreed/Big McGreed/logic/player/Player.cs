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

namespace Big_McGreed.logic.player
{
    public class Player : Entity, Destroyable
    {
        //De grootte van de 'dot' van de crosshair, stelt dotSize(width) x dotSize(height) pixels voor.
        public const int dotSize = 5;

        public PlayerDefinition definition { get; set; }

        public int currentLevel { get; set; }

        public bool leftButtonPressed = false;

        private Vector2 lastMousePosition = Vector2.Zero;

        private Vector2 crossHairDot = Vector2.Zero;

        private float rotation;

        //Stelt een speler voor.

        public Player()
        {
            visible = true;
            setX(100);
            setY(100);
        }

        /*
         * 'Verwoest' de speler.
         */
        public void destroy()
        {
        }

        /*
         * Specifieke update.
         */
        protected override void run2()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (!leftButtonPressed)
                {
                    foreach (NPC npc in PrimitivePathFinder.collision(this, Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        npc.hit(new Hit(npc, this, 10));
                    }
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
            if (Mouse.GetState().X != lastMousePosition.X || Mouse.GetState().Y != lastMousePosition.Y)
            {
                lastMousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                lastMousePosition.Normalize();
                //setLocation(new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)));
                setLocation(PrimitivePathFinder.getPosition(Mouse.GetState().X, Mouse.GetState().Y, definition.mainTexture.Width, definition.mainTexture.Height, 2));
            }
        }

        public override void Draw()
        {
            //crossHairDot = new Vector2(definition.mainTexture.Width / 2, definition.mainTexture.Height / 2);
            rotation = (float)Math.Atan2(-lastMousePosition.Y, lastMousePosition.X);
            Program.INSTANCE.spriteBatch.Draw(definition.mainTexture, getLocation(), Color.Black);
            Program.INSTANCE.spriteBatch.Draw(definition.personTexture, new Vector2(Program.INSTANCE.Width - (Program.INSTANCE.Width / 4), Program.INSTANCE.Height - (Program.INSTANCE.Height / 4)), null, Color.Black, rotation, new Vector2(definition.personTexture.Width / 25, definition.personTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
        }
    }
}
