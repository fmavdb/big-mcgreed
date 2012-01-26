using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.gameinterface.buttons
{
    public class UpgradeButtonInGame : InterfaceComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradeButton"/> class.
        /// </summary>
        public UpgradeButtonInGame()
        {
            normal = Program.INSTANCE.loadTexture("TinyButtonNormal");
            pressed = Program.INSTANCE.loadTexture("TinyButtonPressed");
            hover = Program.INSTANCE.loadTexture("TinyButtonHighlight");
            current = normal;

            tinyButton = true;
            text = "UPGRADE";

            Location = new Vector2(GameFrame.Width - current.Width * 1.3f, current.Height * 3.8f);
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Upgrade;
        }
    }
}
