using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Big_McGreed.content.gameinterface.buttons
{
    public class MainMenu : InterfaceComponent
    {
        public MainMenu()
        {
            normal = Program.INSTANCE.loadTexture("ButtonNormal");
            pressed = Program.INSTANCE.loadTexture("ButtonPressed");
            hover = Program.INSTANCE.loadTexture("ButtonHighlight");
            current = normal;

            text = "MAIN MENU";
        }

        public override void action()
        {
            Program.INSTANCE.buttonClickedState = "mainMenu";
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Select;
        }

        public override void drawInfo()
        {
        }
    }
}
