using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Big_McGreed.content;
using Big_McGreed.logic.map;

namespace Big_McGreed.content.mouse
{
    public class Crosshair
    {
        public Crosshair()
        {

        }

        Texture2D mouseCrosshair;
        Vector2 mousePosition = Vector2.Zero;

        public void crosshairLoad()
        {
            mouseCrosshair = Program.INSTANCE.Content.Load<Texture2D>("crosshair");
        }

        public void crosshairDraw()
        {
            mousePosition = PrimitivePathFinder.getPosition(Mouse.GetState().X, Mouse.GetState().Y, mouseCrosshair.Width, mouseCrosshair.Height, 2);
            
            Program.INSTANCE.spriteBatch.Begin();
            Program.INSTANCE.spriteBatch.Draw(mouseCrosshair, mousePosition, Color.Black);
            Program.INSTANCE.spriteBatch.End();
        }
    }
}
