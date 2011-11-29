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

        public static Vector2 getPosition(float curX, float curY, float imageWidth, float imageHeight, float divider)
        {
            float xRadius = imageWidth / divider;
            float yRadius = imageHeight / divider;
            curX = curX - xRadius;
            curY = curY - yRadius;
            float maxX = Program.INSTANCE.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            float maxY = Program.INSTANCE.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            float minX = 0 - xRadius;
            float minY = 0 - yRadius;
            if (curX < minX)
                curX = minX;
            else if (curX > maxX)
                curX = maxX;
            if (curY < minY)
                curY = minY;
            else if (curY > maxY)
                curY = maxY;
            return new Vector2(curX, curY);
        }

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
