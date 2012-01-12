using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.menu.buttons
{
    public class YesButton : Button
    {
        public YesButton()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("ButtonNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("ButtonPressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("ButtonHighlight");
            current = normal;

            //tinyButton = true;
            text = "YES";

            Texture2D yesnoselect = Program.INSTANCE.Content.Load<Texture2D>("YesNoKeuze");

            Location = new Vector2(GameFrame.Width / 2 - yesnoselect.Width / 2 + 50, GameFrame.Height / 2 + yesnoselect.Height / 2 - current.Height - 50);
        }

        public override void action()
        {
            if (Program.INSTANCE.yesKnopGedrukt == "newGame")
            {
                Program.INSTANCE.CurrentGameState = GameWorld.GameState.InGame;
                Program.INSTANCE.newGame();
                Program.INSTANCE.yesKnopGedrukt = "";
            }
            else if (Program.INSTANCE.yesKnopGedrukt == "mainMenu")
            {
                Program.INSTANCE.CurrentGameState = GameWorld.GameState.Menu;
                Program.INSTANCE.yesKnopGedrukt = "";
            }
        }
    }
}
