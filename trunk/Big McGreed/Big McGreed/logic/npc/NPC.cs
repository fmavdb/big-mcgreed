﻿using System;
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
using Big_McGreed.logic.map;

namespace Big_McGreed.logic.npc
{
    public class NPC : Entity, Destroyable
    {
        //Stelt een door de computer gestuurde entiteit voor.

        public int type { get; private set; }

        public NPCDefinition definition { get; set; }

        public NPC(int type)
        {
            this.type = type;
            definition = NPCDefinition.forType(type);
            updateLifes(definition.hp);
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
            setLocation(PrimitivePathFinder.getPosition(getX() + 1, getY(), definition.mainTexture.Width, definition.mainTexture.Height, 0));
        }

        public override void Draw() 
        {
            Program.INSTANCE.spriteBatch.Draw(definition.mainTexture, getLocation(), Color.White);
        }
    }
}
