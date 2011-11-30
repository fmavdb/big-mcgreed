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

        public Menu()
        {
            buttons = new LinkedList<Button>();
            buttons.AddLast(new Quit());
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
                }
            }
            switch(Mouse.GetState().LeftButton) {
                //Dit is voor als je met de muis op de button klikt, en dan loslaat.
                case ButtonState.Released:
                    buttonNearMouse.action();
                    break;
                //Dit is voor als je met de muis op de button klikt, en dan inhoud.
                case ButtonState.Pressed:
                    break;
            }
        }

        public void Draw()
        {
            lock (buttons)
            {
                foreach (Button button in buttons)
                {
                   
                }
            }
        }
    }
}
