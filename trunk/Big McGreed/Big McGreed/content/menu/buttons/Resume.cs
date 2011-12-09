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
            normal = Program.INSTANCE.Content.Load<Texture2D>("ResumeNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("ResumePressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("ResumeHighlight");
            current = normal;
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.InGame;
        }
    }
}
