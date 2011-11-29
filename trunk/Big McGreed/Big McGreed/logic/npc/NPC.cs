using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Big_McGreed.utility;

namespace Big_McGreed.logic.npc
{
    public class NPC : Entity, Destroyable
    {
        //Stelt een door de computer gestuurde entiteit voor.

        public NPCDefinition definition { get; set; }

        Vector2 npcPosition = Vector2.Zero;

        public NPC()
        {
            definition = NPCDefinition.forType(1);
            visible = true;
        }

        /*
         * 'Verwoest' de NPC.
         */
        public void destroy()
        {
        }

        /*
         * Specifieke update.
         */
        protected override void run2()
        {
            npcPosition.X += 1;

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {

            }
        }

        public override void Draw() 
        {
            Program.INSTANCE.spriteBatch.Begin();
            Program.INSTANCE.spriteBatch.Draw(definition.mainTexture, npcPosition, Color.White);
            Program.INSTANCE.spriteBatch.End();
        }
    }
}
