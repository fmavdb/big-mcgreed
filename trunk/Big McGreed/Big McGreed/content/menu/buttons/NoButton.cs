using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.menu.buttons
{
    public class NoButton : Button
    {
        public NoButton()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("ButtonNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("ButtonPressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("ButtonHighlight");
            current = normal;

            //tinyButton = true;
            text = "NO";

            Texture2D yesnoselect = Program.INSTANCE.Content.Load<Texture2D>("YesNoKeuze");

            Location = new Vector2(GameFrame.Width / 2 + yesnoselect.Width / 2 - current.Width - 50, GameFrame.Height / 2 + yesnoselect.Height / 2 - current.Height - 50);
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Paused;
        }
    }
}
