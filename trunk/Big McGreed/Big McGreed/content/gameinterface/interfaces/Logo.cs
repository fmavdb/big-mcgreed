using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;
using Big_McGreed.content.gameinterface;

namespace Big_McGreed.content.gameinterface.interfaces
{
    public class Logo : GameInterface
    {
        public Logo()
        {
            mainTexture = Program.INSTANCE.loadTexture("BM logo kogelgaten");

            size = 0.5f;

            setLocation(new Vector2(GameFrame.Width / 2 - mainTexture.Width / (2 / size), mainTexture.Height / (4 / size)));
        }
    }
}