using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Big_McGreed.logic.npc;
using Big_McGreed.logic.player;

namespace Big_McGreed.logic.map
{
    public class PrimitivePathFinder : PathFinding
    {

        public static bool collision(Entity entity, int x, int y, bool checkForNPCCollision)
        {
            if (checkForNPCCollision)
            {
                foreach (NPC npc in Program.INSTANCE.getNPCs()) {
                    if (npc.visible && npc.definition.mainTexture != null) {
                        if (entity is Player)
                        {
                        }
                        else if (entity is NPC)
                        {
                            if (intersects(new Rectangle(x, y, ((NPC)entity).definition.mainTexture.Width, ((NPC)entity).definition.mainTexture.Height), new Rectangle(npc.getX(), npc.getY(), npc.definition.mainTexture.Width, npc.definition.mainTexture.Height)))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }


        public static bool intersects(Rectangle object1, Rectangle object2)
        {
            return object1.Intersects(object2);
        }

        public List<Vector2> findPath()
        {
            return null;
        }
    }
}
