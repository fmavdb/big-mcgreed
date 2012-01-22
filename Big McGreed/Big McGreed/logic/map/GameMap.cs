using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.logic.map.objects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Big_McGreed.logic.projectile;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.logic.map
{
    /// <summary>
    /// Represents a game map.
    /// </summary>
    public class GameMap
    {
        private Texture2D mainTexture;

        private LinkedList<GameObject> objects;
        private LinkedList<Projectile> projectiles;

        /// <summary>
        /// Gets the projectiles.
        /// </summary>
        public LinkedList<Projectile> Projectiles
        {
            get
            {
                return projectiles;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameMap"/> class.
        /// </summary>
        public GameMap()
        {
            objects = new LinkedList<GameObject>();
            projectiles = new LinkedList<Projectile>();
            mainTexture = Program.INSTANCE.loadTexture("Achtergrond");
        }

        /// <summary>
        /// Loads the game objects.
        /// </summary>
        public void LoadGameObjects()
        {
            objects.Clear();
            objects.AddFirst(new GameObject(1, new Vector2(GameWorld.random.Next(-ObjectDefinition.forType(1).mainTexture.Width - GameWorld.random.Next(0, 750), GameFrame.Width), 0)));
        }

        /// <summary>
        /// Adds the projectile.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        public void AddProjectile(Projectile projectile)
        {
            lock (projectiles)
            {
                projectiles.AddLast(projectile);
            }
        }

        /// <summary>
        /// Removes the projectile.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        public void RemoveProjectile(Projectile projectile)
        {
            lock (projectiles)
            {
                projectiles.Remove(projectile);
            }
        }

        /// <summary>
        /// Clears the projectiles.
        /// </summary>
        public void ClearProjectiles()
        {
            lock (projectiles)
            {
                projectiles.Clear();
            }
        }

        /// <summary>
        /// Draws the background.
        /// </summary>
        public void DrawBackground(SpriteBatch batch)
        {
            float scale = 1.0f;
            //Console.WriteLine(scale);
            batch.Draw(mainTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Draws objects in this instance.
        /// </summary>
        public void DrawObjects(SpriteBatch batch)
        {
            lock(objects) {
                foreach (GameObject gameObject in objects)
                {
                    if (gameObject.visible)
                    {
                        gameObject.Draw(batch);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the projectiles.
        /// </summary>
        public void UpdateProjectiles()
        {
            lock (projectiles)
            {
                //This prevents enumaration errors.
                LinkedList<Projectile> currentProjectiles = new LinkedList<Projectile>(projectiles);
                foreach (Projectile projectile in currentProjectiles)
                {
                    if (!projectile.disposed)
                    {
                        projectile.Update();
                    }
                    else
                    {
                        RemoveProjectile(projectile);
                    }
                }
            }
        }

        /// <summary>
        /// Draws projectiles in this instance.
        /// </summary>
        public void DrawProjectiles(SpriteBatch batch)
        {
            lock (projectiles)
            {
                foreach (Projectile projectile in projectiles)
                {
                    if (projectile.visible && !projectile.disposed)
                    {
                        projectile.Draw(batch);
                    }
                }
            }
        }
    }
}
