using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Big_McGreed.content.menu.buttons
{
    public class MainMenu : Button
    {
        public MainMenu()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("ButtonNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("ButtonPressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("ButtonHighlight");
            current = normal;

            text = "MAIN MENU";
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Select;
            Program.INSTANCE.yesKnopGedrukt = "mainMenu";
        }
    }
}
