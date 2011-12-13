using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Big_McGreed.content.menu.buttons
{
    public class MenuButtonKlein : Button
    {
        public MenuButtonKlein()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("MenuNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("MenuPressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("MenuHighlight");
            current = normal;

            Location = new Vector2(10, Program.INSTANCE.Height - (current.Height + 10));
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Paused;
        }
    }
}
