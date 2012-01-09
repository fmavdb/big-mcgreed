using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.menu.buttons
{
    public class YesNoSelect : Button
    {
        public YesNoSelect()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("YesNoKeuze");
            hover = normal;
            pressed = normal;
            current = normal;
        }

        public override void action()
        {
        }

        public void YesNoSelectUpdate()
        {
            if (Program.INSTANCE.yesKnopGedrukt == "newGame")
            {
                text = "Are you sure you wish to start a new game? \n\n NOTE: YOU WILL LOSE ALL YOUR PROGRESS!";
                Location = new Vector2(GameFrame.Width / 2 - current.Width / 2, GameFrame.Height / 2 - current.Height / 2);
            }
            else if (Program.INSTANCE.yesKnopGedrukt == "mainMenu")
            {
                text = "Are you sure you wish to go to the main menu? \n\n NOTE: YOU WILL LOSE ALL YOUR PROGRESS!";
                Location = new Vector2(GameFrame.Width / 2 - current.Width / 2, GameFrame.Height / 2 - current.Height / 2);
            }
        }
    }
}
