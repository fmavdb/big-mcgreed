using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.menu.buttons
{
    public class MenuButtonKlein : Button
    {
        public MenuButtonKlein()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("TinyButtonNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("TinyButtonPressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("TinyButtonHighlight");
            current = normal;

            tinyButton = true;
            text = "MENU";

            Location = new Vector2(10, GameFrame.Height - (current.Height + 10));
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Paused;
        }
    }
}
