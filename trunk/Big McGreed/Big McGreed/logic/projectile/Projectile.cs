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

        private Vector2 speed = Vector2.Zero;

        private Vector2 velocity = Vector2.Zero;

        private Vector2 direction = Vector2.Zero;

        private Hit hit;

        public Projectile(int type, Hit hit, Vector2 target)
        {
            this.type = type;
            this.target = target;
            this.hit = hit;
            setLocation(Program.INSTANCE.player.GeweerLocatie);
            rotation = (float)Math.Atan2(getY() - target.Y, getX() - target.X);
            direction = target - getLocation();
            direction.Normalize();
            speed = new Vector2((float)5, (float) 5);
            velocity = Vector2.Multiply(direction, speed);
            //speed = target - getLocation();
            //speed.Normalize();
            //speed = new Vector2(target.X / getX(), target.Y / getY());
        }

        public void destroy()
        {
            destroyed = true;
        }

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
            else if (getX() >= Program.INSTANCE.Width)
                destroy();
            if (getY() <= 0)
                destroy();
            else if (getY() >= Program.INSTANCE.Height)
                destroy();
            foreach(NPC npc in PrimitivePathFinder.collision(this, getX(), getY())) {
                npc.hit(hit);
                destroy();
                break;
            }
        }

        public void Draw()
        {
            Program.INSTANCE.spriteBatch.Draw(definition.mainTexture, getLocation(), new Rectangle(0, 0, definition.mainTexture.Width, definition.mainTexture.Height), Color.White, rotation, new Vector2(definition.mainTexture.Width, definition.mainTexture.Height), 0.50f, SpriteEffects.None, 1.0f);
        }
    }
}
