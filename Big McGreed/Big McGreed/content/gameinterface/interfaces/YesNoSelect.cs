using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.gameinterface.interfaces
{
    public class YesNoSelect : GameInterface
    {
        string newGameText = " Are you sure you wish to start a new game?     \n\n NOTE: YOU WILL LOSE ALL YOUR PROGRESS!";
        string mainMenuText = " Are you sure you wish to go to the main menu? \n\n NOTE: YOU WILL LOSE ALL YOUR PROGRESS!";

        public YesNoSelect()
        {
            mainTexture = Program.INSTANCE.loadTexture("YesNoKeuze");
            YesNoSelectUpdate();
            centerInterface();
        }

        public void YesNoSelectUpdate()
        {
            if (Program.INSTANCE.yesKnopGedrukt == "newGame")
            {
                title = newGameText;
                centerTitle();
            }
            else if (Program.INSTANCE.yesKnopGedrukt == "mainMenu")
            {
                title = mainMenuText;
                centerTitle();
            }
        }
    }
}
