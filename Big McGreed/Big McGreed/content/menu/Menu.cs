﻿using System;
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
        private NewGame newGame;
        private HighScore highScore;
        private Quit quit;
        public Resume resume { get; private set; }

        public Menu()
        {
            buttons = new LinkedList<Button>();
            newGame = new NewGame();
            buttons.AddFirst(newGame);
            highScore = new HighScore();
            buttons.AddAfter(buttons.Find(newGame), highScore);
            quit = new Quit();
            buttons.AddLast(quit);
            resume = new Resume();
            updateButtons();
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

        public LinkedList<Button> getButtons()
        {
            return buttons;
        }

        public void updateButtons()
        {
            float startY = 15;
            foreach (Button button in buttons)
            {
                button.location.X = Program.INSTANCE.Width / 2 - button.current.Width / 2;
                button.location.Y = startY;
                startY += button.normal.Height + 15;
            }
        }
    }
}
