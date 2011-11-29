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

namespace Big_McGreed.content.mouse
{
    class Crosshair: Game
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
            mousePosition = new Vector2(Mouse.GetState().X - (mouseCrosshair.Width / 2), Mouse.GetState().Y - (mouseCrosshair.Height / 2));
            Program.INSTANCE.spriteBatch.Begin();
            Program.INSTANCE.spriteBatch.Draw(mouseCrosshair, mousePosition, Color.Black);
            Program.INSTANCE.spriteBatch.End();
        }
    }
}
