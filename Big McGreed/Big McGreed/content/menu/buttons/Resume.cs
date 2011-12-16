using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.content.menu.buttons
{
    public class Resume : Button
    {
        public Resume()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("ButtonNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("ButtonPressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("ButtonHighlight");
            current = normal;

            text = "RESUME";
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.InGame;
        }
    }
}
