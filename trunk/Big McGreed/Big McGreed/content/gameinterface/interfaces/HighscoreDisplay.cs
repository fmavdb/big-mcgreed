using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;
using Big_McGreed.content.gameinterface;

namespace Big_McGreed.content.gameinterface.interfaces
{
    public class HighscoreDisplay : GameInterface
    {

        public HighscoreDisplay()
        {
            mainTexture = Program.INSTANCE.loadTexture("NormalInterface");

            title = "HIGHSCORES";

            centerInterface();
        }
    }
}
