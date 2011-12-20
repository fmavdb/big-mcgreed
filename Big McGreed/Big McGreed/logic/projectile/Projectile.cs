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
    public class Projectile : Locatable, Destroyable
    {
        public int type { get; private set; }

        public bool visible = true;

        public bool destroyed = false;

        private float rotation;

        private Vector2 target = Vector2.Zero;

        public ProjectileDefinition definition { get { return ProjectileDefinition.forType(type); } }

        private static Vector2 speed = new Vector2((float)10, (float)10);

        private Vector2 velocity = Vector2.Zero;

        private Hit hit;

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
            this.hit = hit;
            setLocation(Program.INSTANCE.player.GeweerLocatie);
            rotation = (float)Math.Atan2(getY() - target.Y, getX() - target.X);
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
            foreach(NPC npc in PrimitivePathFinder.collision(this, getX(), getY())) {
                npc.hit(hit);
                destroy();
                break;
            }
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            Program.INSTANCE.spriteBatch.Draw(definition.mainTexture, getLocation(), new Rectangle(0, 0, definition.mainTexture.Width, definition.mainTexture.Height), Color.White, rotation, new Vector2(definition.mainTexture.Width, definition.mainTexture.Height), 0.50f, SpriteEffects.None, 1.0f);
        }
    }
}
