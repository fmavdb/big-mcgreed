using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Big_McGreed.logic.mask;
using Big_McGreed.logic.map;
using Big_McGreed.logic.npc;
using Big_McGreed.utility;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.logic.projectile
{
    /// <summary>
    /// Represents a projectile.
    /// </summary>
    public class Projectile : Locatable, Destroyable
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        public int type { get; private set; }

        /// <summary>
        /// Wether the projectile is visible or not.
        /// </summary>
        public bool visible = true;

        /// <summary>
        /// Wether the projectile is scheduled for removal or not.
        /// </summary>
        public bool destroyed = false;

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        public float Rotation { get; private set; }

        private Vector2 target = Vector2.Zero;

        /// <summary>
        /// Gets the definition.
        /// </summary>
        public ProjectileDefinition definition { get { return ProjectileDefinition.forType(type); } }

        private static Vector2 speed = new Vector2(40f, 40f);

        private Vector2 velocity = Vector2.Zero;

        /// <summary>
        /// Gets the hit.
        /// </summary>
        public Hit Hit { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Projectile"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="hit">The hit.</param>
        /// <param name="target">The target.</param>
        public Projectile(int type, Hit hit, Vector2 target)
        {
            this.type = type;
            this.target = target;
            Hit = hit;
            setLocation(Program.INSTANCE.player.Weapon.getLocation());
            //setLocation(translate(-GameWorld.playerDefinition.revolverTexture.Width, -GameWorld.playerDefinition.revolverTexture.Height)); 
            Rotation = (float)Math.Atan2(getY() - target.Y, getX() - target.X);
            Vector2 direction = target - getLocation();
            direction.Normalize();
            velocity = Vector2.Multiply(direction, speed);
            //speed = target - getLocation();
            //speed.Normalize();
            //speed = new Vector2(target.X / getX(), target.Y / getY());
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void destroy()
        {
            destroyed = true;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            setLocation(getLocation() + velocity);
            /*float speedVal = Math.Max(Math.Abs(speed.X), Math.Abs(speed.Y));
            Console.WriteLine(speedVal);
            if (target.X < getX())
                setX(getX() - speedVal);
            else if (target.X > getX())
                setX(getX() + speedVal);
            if (target.Y < getY())
                setY(getY() - speedVal);
            else if (target.Y > getY())
                setY(getY() + speedVal);*/
            //setLocation(getLocation() * direction * 1.001f);
            if (getX() <= 0)
                destroy();
            else if (getX() >= GameFrame.Width)
                destroy();
            if (getY() <= 0)
                destroy();
            else if (getY() >= GameFrame.Height)
                destroy();
            else
            {
                PrimitivePathFinder.collision(this, getX(), getY());
            }
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(definition.mainTexture, getLocation(), new Rectangle(0, 0, definition.mainTexture.Width, definition.mainTexture.Height), Color.White, Rotation, new Vector2(Program.INSTANCE.player.Weapon.definition.mainTexture.Width, Program.INSTANCE.player.Weapon.definition.mainTexture.Height), 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}
