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

        private Hit hit;

        public Projectile(int type, Hit hit, Vector2 target)
        {
            this.type = type;
            this.target = target;
            this.hit = hit;
            setLocation(Program.INSTANCE.player.GeweerLocatie);
            rotation = (float)Math.Atan2(getY() - target.Y, getX() - target.X);
            speed = new Vector2((float)0.985, (float)0.985);
        }

        public void destroy()
        {
            destroyed = true;
        }

        public void Update()
        {
            setLocation(getLocation() * speed);
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
