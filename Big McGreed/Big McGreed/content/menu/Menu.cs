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
        public NewGame newGame;
        public HighScore highScore;
        public Quit quit;
        public Upgrade upgrade { get; private set; }
        public Resume resume { get; private set; }
        public MenuButtonKlein menuButtonKlein { get; private set; }
        public ResumeKlein resumeKlein { get; private set; }
        public MainMenu mainMenu { get; private set; }
        public YesButton yesButton { get; private set; }
        public NoButton noButton { get; private set; }
        public YesNoSelect yesNoSelect { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
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
            upgrade = new Upgrade();
            menuButtonKlein = new MenuButtonKlein();
            resumeKlein = new ResumeKlein();
            mainMenu = new MainMenu();
            yesButton = new YesButton();
            noButton = new NoButton();
            yesNoSelect = new YesNoSelect();
            updateButtons();
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            Button buttonNearMouse = null;
            lock (buttons)
            {
                Rectangle mouse = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                foreach (Button button in buttons)
                {
                    Rectangle rectangleButton = new Rectangle((int)button.Location.X, (int)button.Location.Y, button.Normal.Width, button.Normal.Height);
                    if (PrimitivePathFinder.intersects(mouse, rectangleButton))
                    {
                        buttonNearMouse = button;
                    }
                    else
                    {
                        button.Current = button.Normal;
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
                            buttonNearMouse.Current = buttonNearMouse.Hover;
                        }
                    }
                    break;
                case ButtonState.Pressed:
                    if (buttonNearMouse != null)
                    {
                        buttonNearMouse.Current = buttonNearMouse.pressed;
                        released = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
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

        /// <summary>
        /// Gets the buttons.
        /// </summary>
        /// <returns></returns>
        public LinkedList<Button> getButtons()
        {
            return buttons;
        }

        /// <summary>
        /// Updates the buttons.
        /// </summary>
        public void updateButtons()
        {
            float startY = Program.INSTANCE.Height / 5;
            lock (buttons)
            {
                foreach (Button button in buttons)
                {
                    if (button != upgrade && button != resumeKlein && button != menuButtonKlein)
                    {
                        button.Location = new Vector2(Program.INSTANCE.Width / 2 - button.Current.Width / 2, startY);
                        startY += button.Normal.Height + 15;
                    }
                 }
            } 
        }
    }
}
