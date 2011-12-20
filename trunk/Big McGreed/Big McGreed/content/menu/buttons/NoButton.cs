﻿using System;
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
            normal = Program.INSTANCE.Content.Load<Texture2D>("TinyButtonNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("TinyButtonPressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("TinyButtonHighlight");
            current = normal;

            tinyButton = true;
            text = "NO";

            Texture2D yesnoselect = Program.INSTANCE.Content.Load<Texture2D>("YesNoKeuze");

            Location = new Vector2(GameFrame.Width / 2 + yesnoselect.Width / 2 - current.Width - 10, GameFrame.Height / 2 + yesnoselect.Height / 2 - current.Height - 10);
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Paused;
        }
    }
}
