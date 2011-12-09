using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

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

            location.X = 10;
            location.Y = Program.INSTANCE.Height - (current.Height + 10);
        }

        public override void action()
        {
            Program.INSTANCE.setGameState(GameWorld.GameState.Paused);
        }
    }
}
