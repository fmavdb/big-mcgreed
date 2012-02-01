using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.gameinterface.buttons
{
    public class YesButton : InterfaceComponent
    {
        public YesButton()
        {
            normal = Program.INSTANCE.loadTexture("ButtonNormal");
            pressed = Program.INSTANCE.loadTexture("ButtonPressed");
            hover = Program.INSTANCE.loadTexture("ButtonHighlight");
            current = normal;

            //tinyButton = true;
            text = "YES";

            Texture2D yesnoselect = Program.INSTANCE.loadTexture("TinyInterface");

            Location = new Vector2(GameFrame.Width / 2 - yesnoselect.Width / 2 + 50, GameFrame.Height / 2 + yesnoselect.Height / 2 - current.Height - 50);
        }

        public override void action()
        {
            if (Program.INSTANCE.buttonClickedState == "newGame")
            {
                Program.INSTANCE.CurrentGameState = GameWorld.GameState.Begin;
                Program.INSTANCE.buttonClickedState = "";
            }
            else if (Program.INSTANCE.buttonClickedState == "mainMenu")
            {
                Program.INSTANCE.CurrentGameState = GameWorld.GameState.Menu;
                Program.INSTANCE.buttonClickedState = "";
            }
        }
    }
}
