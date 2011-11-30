using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.content.menu.buttons;
using Microsoft.Xna.Framework.Input;
using Big_McGreed.logic.map;
using Microsoft.Xna.Framework;

namespace Big_McGreed.content.menu
{
    public class Menu
    {

        private LinkedList<Button> buttons;
        private bool released = false;

        public Menu()
        {
            buttons = new LinkedList<Button>();
            buttons.AddFirst(new HighScore());            
            buttons.AddLast(new Quit());


            float startY = 100;
            foreach (Button button in buttons)
            {
                button.location.X = Program.INSTANCE.GraphicsDevice.Viewport.Width / 2 - button.current.Width / 2;
                button.location.Y = startY;
                startY += button.normal.Height + 50;
            }
        }

        public void Update()
        {
            Button buttonNearMouse = null;
            lock (buttons)
            {
                Rectangle mouse = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                foreach (Button button in buttons)
                {
                    Rectangle rectangleButton = new Rectangle((int)button.location.X, (int)button.location.Y, button.normal.Width, button.normal.Height);
                    if (PrimitivePathFinder.intersects(mouse, rectangleButton))
                    {
                        buttonNearMouse = button;
                        break;
                    }
                    else
                    {
                        button.current = button.normal;
                    }
                }
            }
            switch(Mouse.GetState().LeftButton) {
                case ButtonState.Released:
                    if (buttonNearMouse != null)
                    {
                        if (released)
                        {
                            buttonNearMouse.action();
                            released = false;
                        }
                        else
                        {
                            buttonNearMouse.current = buttonNearMouse.hover;
                        }
                    }
                    break;
                case ButtonState.Pressed:
                    if (buttonNearMouse != null)
                    {
                        buttonNearMouse.current = buttonNearMouse.pressed;
                        released = true;
                    }
                    break;
            }
        }

        public void Draw()
        {
            lock (buttons)
            {
                foreach (Button button in buttons)
                {
                    button.Draw();
                }
            }
        }
    }
}
