using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace Big_McGreed.content.menu.buttons
{
    public class HighScore : Button
    {
        public HighScore()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("HighscoreNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("HighscorePressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("HighscoreHighlight");
            current = normal;
            text = "HIGHSCORE";

        }

        public override void action()
        {
            Program.INSTANCE.Exit();
        }
    }
}
