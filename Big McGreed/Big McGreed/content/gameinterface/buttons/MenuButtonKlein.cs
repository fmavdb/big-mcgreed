using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.gameinterface.buttons
{
    public class MenuButtonKlein : InterfaceComponent
    {
        public MenuButtonKlein()
        {
            normal = Program.INSTANCE.loadTexture("TinyButtonNormal");
            pressed = Program.INSTANCE.loadTexture("TinyButtonPressed");
            hover = Program.INSTANCE.loadTexture("TinyButtonHighlight");
            current = normal;

            tinyButton = true;
            text = "MENU";

            Location = new Vector2(10, GameFrame.Height - (current.Height + 10));
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Paused;

            if (Program.INSTANCE.LastGameState == GameWorld.GameState.GameOver)
            {
                Program.INSTANCE.CurrentGameState = GameWorld.GameState.Menu;
            }
            else if (Program.INSTANCE.highscoreMenu == "HoofdMenu")
            {
                Program.INSTANCE.CurrentGameState = GameWorld.GameState.Menu;
                Program.INSTANCE.highscoreMenu = "";
            }
            else if (Program.INSTANCE.highscoreMenu == "Paused")
            {
                Program.INSTANCE.CurrentGameState = GameWorld.GameState.Paused;
                Program.INSTANCE.highscoreMenu = "";
            }
        }
    }
}
