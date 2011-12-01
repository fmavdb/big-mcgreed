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
            float xRadius = divider > 0 ? imageWidth / divider : 0;
            float yRadius = divider > 0 ? imageHeight / divider : 0;
            curX = curX - xRadius;
            curY = curY - yRadius;
            float maxX = Program.INSTANCE.Width;
            float maxY = Program.INSTANCE.Height;
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
                        if (entity is Player) //crosshair
                        {
                            if (intersects(new Rectangle(x, y, Player.dotSize, Player.dotSize), new Rectangle(npc.getX(), npc.getY(), npc.definition.mainTexture.Width, npc.definition.mainTexture.Height)))
                            {
                                return true;
                            }
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

        public static List<NPC> collision(Player player, int x, int y)
        {
            List<NPC> npcsCollided= new List<NPC>();
            Rectangle crossHair = new Rectangle(x, y, Player.dotSize, Player.dotSize);
            foreach (NPC npc in Program.INSTANCE.getNPCs())
            {
                if (npc.visible && npc.definition.mainTexture != null)
                {
                    Rectangle npcRectangle = new Rectangle(npc.getX(), npc.getY(), npc.definition.mainTexture.Width, npc.definition.mainTexture.Height);
                    if (intersects(crossHair, npcRectangle))
                    {
                        Color[] pixels = new Color[npc.definition.mainTexture.Width * npc.definition.mainTexture.Height];
                        npc.definition.mainTexture.GetData<Color>(pixels);
                        if (colorCollision(Color.Transparent, crossHair, npcRectangle, pixels))
                        {
                            npcsCollided.Add(npc);
                        }
                    }
                }
            }
            return npcsCollided;
        }

        public static bool colorCollision(Color collideColor, Rectangle crossHair, Rectangle npc, Color[] pixels)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(crossHair.Top, npc.Top);
            int bottom = Math.Min(crossHair.Bottom, npc.Bottom);
            int left = Math.Max(crossHair.Left, npc.Left);
            int right = Math.Min(crossHair.Right, npc.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color color = pixels[(x - npc.Left) + (y - npc.Top) * npc.Width];
                    // Transparency is color.A == 0
                    if (color != collideColor)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
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
