using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Big_McGreed.content.menu.buttons
{
    public class Upgrade : Button
    {
        public Upgrade()
        {
            normal = Program.INSTANCE.Content.Load<Texture2D>("UpgradeNormal");
            pressed = Program.INSTANCE.Content.Load<Texture2D>("UpgradePressed");
            hover = Program.INSTANCE.Content.Load<Texture2D>("UpgradeHighlight");
            current = normal;

            Location = new Vector2(10, Program.INSTANCE.Height - (current.Height + 10));
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Upgrade;
        }
    }
}
