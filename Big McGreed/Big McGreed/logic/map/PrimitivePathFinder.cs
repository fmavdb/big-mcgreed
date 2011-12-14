using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Big_McGreed.logic.npc;
using Big_McGreed.logic.player;
using System.Collections.ObjectModel;
using Big_McGreed.logic.projectile;

namespace Big_McGreed.logic.map
{
    public class PrimitivePathFinder : PathFinding
    {

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <param name="curX">The cur X.</param>
        /// <param name="curY">The cur Y.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="divider">The divider.</param>
        /// <returns></returns>
        public static Vector2 getPosition(Vector2 curPos, float imageWidth, float imageHeight, float divider)
        {
            float xRadius = divider > 0 ? imageWidth / divider : 0;
            float yRadius = divider > 0 ? imageHeight / divider : 0;
            curPos.X = curPos.X - xRadius;
            curPos.Y = curPos.Y - yRadius;
            float maxX = Program.INSTANCE.Width;
            float maxY = Program.INSTANCE.Height;
            float minX = 0 - xRadius;
            float minY = 0 - yRadius;
            if (curPos.X < minX)
                curPos.X = minX;
            else if (curPos.X > maxX)
                curPos.X = maxX;
            if (curPos.Y < minY)
                curPos.Y = minY;
            else if (curPos.Y > maxY)
                curPos.Y = maxY;
            return curPos;
        }

        /// <summary>
        /// Collisions the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="checkForNPCCollision">if set to <c>true</c> [check for NPC collision].</param>
        /// <returns></returns>
        public static bool collision(Entity entity, int x, int y, bool checkForNPCCollision)
        {
            if (checkForNPCCollision)
            {
                lock (Program.INSTANCE.npcs)
                {
                    Rectangle crossHair = new Rectangle(x, y, Player.dotSize, Player.dotSize);
                    foreach (NPC npc in Program.INSTANCE.npcs)
                    {
                        if (npc.visible && npc.definition.mainTexture != null)
                        {
                            if (entity is Player) //crosshair
                            {
                                if (intersects(crossHair, new Rectangle(npc.getX(), npc.getY(), npc.definition.mainTexture.Width, npc.definition.mainTexture.Height)))
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
            }
            return false;
        }

        /// <summary>
        /// Collisions the specified projectile.
        /// </summary>
        /// <param name="projectile">The player.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static Collection<NPC> collision(Projectile projectile, int x, int y)
        {
            Collection<NPC> npcsCollided= new Collection<NPC>();
            //TODO fix the projectilesize to include the rotation.
            Rectangle projectileSize = new Rectangle(x, y, projectile.definition.mainTexture.Width, projectile.definition.mainTexture.Height);
            lock (Program.INSTANCE.npcs)
            {
                foreach (NPC npc in Program.INSTANCE.npcs)
                {
                    if (npc.visible && npc.definition.mainTexture != null)
                    {
                        Rectangle npcRectangle = new Rectangle(npc.getX(), npc.getY(), npc.definition.mainTexture.Width, npc.definition.mainTexture.Height);
                        if (intersects(projectileSize, npcRectangle))
                        {
                            //Color[] pixels = new Color[npc.definition.mainTexture.Width * npc.definition.mainTexture.Height];
                            //npc.definition.mainTexture.GetData<Color>(pixels);
                            if (colorCollision(Color.Transparent, projectileSize, npcRectangle, npc.definition.pixels))
                            {
                                npcsCollided.Add(npc);
                            }
                        }
                    }
                }
            }
            return npcsCollided;
        }

        /// <summary>
        /// Colors the collision.
        /// </summary>
        /// <param name="collideColor">Color of the collide.</param>
        /// <param name="crossHair">The cross hair.</param>
        /// <param name="npc">The NPC.</param>
        /// <param name="pixels">The pixels.</param>
        /// <returns></returns>
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


        /// <summary>
        /// Intersectses the specified object1.
        /// </summary>
        /// <param name="object1">The object1.</param>
        /// <param name="object2">The object2.</param>
        /// <returns></returns>
        public static bool intersects(Rectangle object1, Rectangle object2)
        {
            return object1.Intersects(object2);
        }

        /// <summary>
        /// Finds the path.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public LinkedList<Vector2> findPath(Vector2 start, Vector2 end)
        {
            if (start.X < 0 || start.Y < 0 || start.X > Program.INSTANCE.Width || start.Y > Program.INSTANCE.Height || end.X < 0 || end.Y < 0 || end.X > Program.INSTANCE.Width || end.Y > Program.INSTANCE.Height)
            {
                return null;
            }
            if (start.X == end.X && start.Y == end.Y)
            {
                return null;
            }
            LinkedList<Vector2> path = new LinkedList<Vector2>();
            return null;
        }
    }
}
