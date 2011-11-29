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
            //mousePosition = new Vector2(Mouse.GetState().X - (mouseCrosshair.Width / 2), Mouse.GetState().Y - (mouseCrosshair.Height / 2));
            mousePosition.X = Mouse.GetState().X - (mouseCrosshair.Width / 2);
            mousePosition.Y = Mouse.GetState().Y - (mouseCrosshair.Height / 2);
            if (mousePosition.X < (0 - mouseCrosshair.Width / 2))
                mousePosition.X = (0 - mouseCrosshair.Width / 2);
            if (mousePosition.X > Program.INSTANCE.GraphicsDevice.Adapter.CurrentDisplayMode.Width)
                mousePosition.X = Program.INSTANCE.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            if (mousePosition.Y < (0 - mouseCrosshair.Height / 2))
                mousePosition.Y = (0 - mouseCrosshair.Height / 2);
            if (mousePosition.Y > Program.INSTANCE.GraphicsDevice.Adapter.CurrentDisplayMode.Height)
                mousePosition.Y = Program.INSTANCE.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            Program.INSTANCE.spriteBatch.Begin();
            Program.INSTANCE.spriteBatch.Draw(mouseCrosshair, mousePosition, Color.Black);
            Program.INSTANCE.spriteBatch.End();
        }
    }
}
