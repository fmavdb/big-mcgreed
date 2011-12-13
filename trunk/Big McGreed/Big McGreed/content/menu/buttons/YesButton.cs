using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.content.menu.buttons
{
    public class YesButton : Button
    {
        public YesButton()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("YesNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("YesPressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("YesHighlight");
            current = normal;

            Texture2D yesnoselect = Program.INSTANCE.Content.Load<Texture2D>("YesNoKeuze");

            location.X = Program.INSTANCE.Width / 2 - yesnoselect.Width / 2 + 10;
            location.Y = Program.INSTANCE.Height / 2 + yesnoselect.Height / 2 - current.Height - 10;
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Menu;
        }
    }
}
