using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.gameinterface.interfaces
{
    public class HighscoreDisplay : InterfaceComponent
    {
        public HighscoreDisplay()
        {
            normal = Program.INSTANCE.loadTexture("HighScoresInterface");
            hover = normal;
            pressed = normal;
            current = normal;

            Location = new Vector2(GameFrame.Width / 2 - current.Width / 2, GameFrame.Height / 2 - current.Height / 2);
        }

        public override void action()
        {
        }
    }
}
