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
            normal = Program.INSTANCE.Content.Load<Texture2D>("MainMenu");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("MainMenuPressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("MainMenuHighlight");
            current = normal;
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Select;
            Program.INSTANCE.yesKnopGedrukt = "mainMenu";
        }
    }
}
