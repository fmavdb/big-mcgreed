using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Big_McGreed.logic.npc;
using Big_McGreed.logic.player;
using System.Collections.ObjectModel;
using Big_McGreed.logic.projectile;
using Big_McGreed.content.gameframe;
using Big_McGreed.utility;

namespace Big_McGreed.logic.map
{
    public class PrimitivePathFinder : PathFinding
    {

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <param name="curPos">The cur pos.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <returns></returns>
        public static Vector2 getCrossHairPosition(Vector2 curPos, float imageWidth, float imageHeight)
        {
            float xRadius = imageWidth / 2;
            float yRadius = imageHeight / 2;
            curPos.X = curPos.X - xRadius;
            curPos.Y = curPos.Y - yRadius;
            float maxX = GameFrame.Width - xRadius;
            float maxY = GameFrame.Height - yRadius;
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
        /// <param name="projectile">The projectile.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static void collision(Projectile projectile, int x, int y)
        {
            //TODO fix color collision xD
            RotatedRectangle projectileRectangle = new RotatedRectangle(new Rectangle(x, y, projectile.definition.mainTexture.Width, projectile.definition.mainTexture.Height), projectile.Rotation);
            //Rectangle projectileSize = new Rectangle(x, y, projectile.definition.mainTexture.Width, projectile.definition.mainTexture.Height);
            lock (Program.INSTANCE.npcs)
            {
                LinkedList<NPC> currentNPCs = new LinkedList<NPC>(Program.INSTANCE.npcs);
                foreach (NPC npc in currentNPCs)
                {
                    if (npc.visible && npc.definition.mainTexture != null)
                    {
                        RotatedRectangle npcRectangle = new RotatedRectangle(new Rectangle(npc.getX(), npc.getY(), npc.definition.mainTexture.Width, npc.definition.mainTexture.Height), 0.0f);
                        if (projectileRectangle.Intersects(npcRectangle))
                        {
                            //Color[] pixels = new Color[projectile.definition.mainTexture.Width * npc.definition.mainTexture.Height];
                            //projectile.definition.mainTexture.GetData<Color>(pixels);
                            //if (colorCollision(Color.Transparent, projectileRectangle, npcRectangle, projectile.definition.pixels, npc.definition.pixels))
                           // {
                                npc.hit(projectile.Hit);
                                projectile.destroy();
                            //}
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Colors the collision.
        /// </summary>
        /// <param name="collideColor">Color of the collide.</param>
        /// <param name="projectile">The cross hair.</param>
        /// <param name="npc">The NPC.</param>
        /// <param name="pixels">The pixels.</param>
        /// <returns></returns>
        public static bool colorCollision(Color collideColor, RotatedRectangle projectile, RotatedRectangle npc, Color[] projectilePixels, Color[] npcPixels)
        {
            Rectangle Rectangle1 = _getBoundingRectangleOfRotatedRectangle(projectile.RectanglePoints);
            Rectangle Rectangle2 = _getBoundingRectangleOfRotatedRectangle(npc.RectanglePoints);

            int top = Math.Max(Rectangle1.Top, Rectangle2.Top);
            int bottom = Math.Min(Rectangle1.Bottom, Rectangle2.Bottom);
            int left = Math.Max(Rectangle1.Left, Rectangle2.Left);
            int right = Math.Min(Rectangle1.Right, Rectangle2.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorA = projectilePixels[(x - Rectangle1.Left) + (y - Rectangle1.Top) * Rectangle1.Width];
                    Color colorB = npcPixels[(x - Rectangle2.Left) + (y - Rectangle2.Top) * Rectangle2.Width];
                    if (colorA != collideColor && colorB != collideColor)
                        return true;
                }
            }
            return false;
        }

        private static Rectangle _getBoundingRectangleOfRotatedRectangle(List<Vector2> RectanglePoints)
        {
            Vector2 BoundingRectangleStart = new Vector2()
            {
                X = _getMinimumOf(RectanglePoints[0].X, RectanglePoints[1].X, RectanglePoints[2].X, RectanglePoints[3].X),
                Y = _getMinimumOf(RectanglePoints[0].Y, RectanglePoints[1].Y, RectanglePoints[2].Y, RectanglePoints[3].Y)
            };

            int BoundingRectangleWidth = -(int)BoundingRectangleStart.X + _getMaximumOf(RectanglePoints[0].X, RectanglePoints[1].X, RectanglePoints[2].X, RectanglePoints[3].X);
            int BoundingRectangleHeight = -(int)BoundingRectangleStart.Y + _getMaximumOf(RectanglePoints[0].Y, RectanglePoints[1].Y, RectanglePoints[2].Y, RectanglePoints[3].Y);

            return new Rectangle((int)BoundingRectangleStart.X, (int)BoundingRectangleStart.Y, BoundingRectangleWidth, BoundingRectangleHeight);

        }

        private static int _getMinimumOf(float a1, float a2, float a3, float a4)
        {
            return (int)MathHelper.Min(MathHelper.Min(MathHelper.Min(a1, a2), a3), a4);
        }
        private static int _getMaximumOf(float a1, float a2, float a3, float a4)
        {
            return (int)MathHelper.Max(MathHelper.Max(MathHelper.Max(a1, a2), a3), a4);
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
            if (start.X < 0 || start.Y < 0 || start.X > GameFrame.Width || start.Y > GameFrame.Height || end.X < 0 || end.Y < 0 || end.X > GameFrame.Width || end.Y > GameFrame.Height)
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
