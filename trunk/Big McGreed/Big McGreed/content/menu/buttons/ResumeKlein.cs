﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.content.menu.buttons
{
    public class ResumeKlein : Button
    {
        public ResumeKlein()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("ResumeNormalKlein");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("ResumePressedKlein");
            hover = Program.INSTANCE.Content.Load<Texture2D>("ResumeHighlightKlein");
            current = normal;

            location.X = Program.INSTANCE.Width - (current.Width + 10);
            location.Y = Program.INSTANCE.Height - (current.Height + 10);
        }

        public override void action()
        {
            Program.INSTANCE.setGameState(GameWorld.GameState.InGame);
        }
    }
}
