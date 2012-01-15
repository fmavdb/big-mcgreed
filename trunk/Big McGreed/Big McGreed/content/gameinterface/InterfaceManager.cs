using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.content.gameinterface.buttons;
using Microsoft.Xna.Framework.Input;
using Big_McGreed.logic.map;
using Microsoft.Xna.Framework;
using Big_McGreed.content.gameframe;
using Microsoft.Xna.Framework.Graphics;
using Big_McGreed.content.gameinterface.interfaces;

namespace Big_McGreed.content.gameinterface
{
    public class InterfaceManager
    {
        private LinkedList<InterfaceComponent> buttons;
        private bool released = false;
        public NewGame newGame;
        public HighScoreButton highScore;
        /// <summary>
        /// 
        /// </summary>
        public Quit quit;
        /// <summary>
        /// Gets the upgrade.
        /// </summary>
        public Upgrade upgrade { get; private set; }
        /// <summary>
        /// Gets the resume.
        /// </summary>
        public Resume resume { get; private set; }
        /// <summary>
        /// Gets the menu button klein.
        /// </summary>
        public MenuButtonKlein menuButtonKlein { get; private set; }
        /// <summary>
        /// Gets the resume klein.
        /// </summary>
        public ResumeKlein resumeKlein { get; private set; }
        /// <summary>
        /// Gets the main menu.
        /// </summary>
        public MainMenu mainMenu { get; private set; }
        /// <summary>
        /// Gets the yes button.
        /// </summary>
        public YesButton yesButton { get; private set; }
        /// <summary>
        /// Gets the no button.
        /// </summary>
        public NoButton noButton { get; private set; }
        /// <summary>
        /// Gets the yes no select.
        /// </summary>
        public YesNoSelect yesNoSelect { get; private set; }
        /// <summary>
        /// Gets the upgrade achtergrond.
        /// </summary>
        public UpgradeAchtergrond upgradeAchtergrond { get; private set; }
        /// <summary>
        /// Gets the submit highscore.
        /// </summary>
        public SubmitHighscore submitHighscore { get; private set; }
        /// <summary>
        /// Gets the highscore display.
        /// </summary>
        public HighscoreDisplay highscoreDisplay { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceManager"/> class.
        /// </summary>
        public InterfaceManager()
        {
            buttons = new LinkedList<InterfaceComponent>();
            newGame = new NewGame();
            buttons.AddFirst(newGame);
            highScore = new HighScoreButton();
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
            upgradeAchtergrond = new UpgradeAchtergrond();
            submitHighscore = new SubmitHighscore();
            highscoreDisplay = new HighscoreDisplay();
            updateButtons();
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            InterfaceComponent buttonNearMouse = null;
            yesNoSelect.YesNoSelectUpdate();

            lock (buttons)
            {
                Rectangle mouse = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                foreach (InterfaceComponent button in buttons)
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
                        buttonNearMouse.Current = buttonNearMouse.Pressed;
                        released = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw(SpriteBatch batch)
        {
            lock (buttons)
            {
                foreach (InterfaceComponent button in buttons)
                {
                    button.Draw(batch);
                }
            }
        }

        /// <summary>
        /// Gets the buttons.
        /// </summary>
        /// <returns></returns>
        public LinkedList<InterfaceComponent> getButtons()
        {
            return buttons;
        }

        /// <summary>
        /// Updates the buttons.
        /// </summary>
        public void updateButtons()
        {
            float startY = GameFrame.Height / 5;
            lock (buttons)
            {
                foreach (InterfaceComponent button in buttons)
                {
                    if (button != upgrade && button != resumeKlein && button != menuButtonKlein )
                    {
                        button.Location = new Vector2(GameFrame.Width / 2 - button.Current.Width / 2, startY);
                        startY += button.Normal.Height + 15;
                    }
                 }
            } 
        }
    }
}
