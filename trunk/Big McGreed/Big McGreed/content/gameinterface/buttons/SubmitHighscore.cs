using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.gameinterface.buttons
{
    public class SubmitHighscore : InterfaceComponent
    {
        public SubmitHighscore()
        {
            normal = Program.INSTANCE.loadTexture("TinyButtonNormal");
            pressed = Program.INSTANCE.loadTexture("TinyButtonPressed");
            hover = Program.INSTANCE.loadTexture("TinyButtonHighlight");
            current = normal;

            tinyButton = true;

            text = "SUBMIT";

            Location = new Vector2(Program.INSTANCE.IManager.gameOverInterface.getX() + Program.INSTANCE.IManager.gameOverInterface.mainTexture.Width / 2 - this.current.Width /2,
                                   Program.INSTANCE.IManager.gameOverInterface.getY() + Program.INSTANCE.IManager.gameOverInterface.mainTexture.Height / 1.25f - this.current.Height / 2);
        }

        public override void action()
        {
            Program.INSTANCE.highScores.addToHighScore(Program.INSTANCE.player.naam, Program.INSTANCE.player.Score);
        }
    }
}
