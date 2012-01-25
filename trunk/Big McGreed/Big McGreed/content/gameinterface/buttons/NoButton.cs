using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.gameinterface.buttons
{
    public class NoButton : InterfaceComponent
    {
        public NoButton()
        {
            normal = Program.INSTANCE.loadTexture("ButtonNormal");
            pressed = Program.INSTANCE.loadTexture("ButtonPressed");
            hover = Program.INSTANCE.loadTexture("ButtonHighlight");
            current = normal;

            //tinyButton = true;
            text = "NO";

            Texture2D yesnoselect = Program.INSTANCE.loadTexture("TinyInterface");

            Location = new Vector2(GameFrame.Width / 2 + yesnoselect.Width / 2 - current.Width - 50, GameFrame.Height / 2 + yesnoselect.Height / 2 - current.Height - 50);
        }

        public override void action()
        {
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.Paused;
        }

        public override void drawInfo()
        {
        }
    }
}
