using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace Big_McGreed.content.gameinterface.buttons
{
    public class HighScoreButton : InterfaceComponent
    {
        public HighScoreButton()
        {
            normal = Program.INSTANCE.loadTexture("ButtonNormal");
            pressed = Program.INSTANCE.loadTexture("ButtonPressed");
            hover = Program.INSTANCE.loadTexture("ButtonHighlight");
            current = normal;

            text = "HIGHSCORE";
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Highscore;
        }
    }
}
