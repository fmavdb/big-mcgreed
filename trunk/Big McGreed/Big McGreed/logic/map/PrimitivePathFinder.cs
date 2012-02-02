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
using Microsoft.Xna.Framework.Graphics;
using Big_McGreed.logic.mask;
using XNAGifAnimation;

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
        /// <param name="player">The player.</param>
        /// <param name="npc">The NPC.</param>
        /// <returns></returns>
        public static bool collision(Player player, NPC npc)
        {
            if (npc.visible) {
                if (npc.attacking) //Waarom controleren op collision als ie toch stilstaat...
                {
                    TimeSpan hitTimePassed = DateTime.Now - npc.lastHit;
                    if (hitTimePassed.TotalMilliseconds >= npc.definition.attackSpeed)
                    {
                        if (player.electricWallTimer > 0)
                        {
                            npc.hit(new Hit(npc, player, player.Damage));
                        }
                        player.hit(new Hit(player, npc, GameWorld.random.Next(1, npc.Damage)));
                        npc.lastHit = DateTime.Now;
                    }
                    return true;
                }
                GifAnimation mainTexture = npc.definition.mainTexture;
                Rectangle npcRectangle = new Rectangle(npc.getX(), npc.getY(), mainTexture.Width, mainTexture.Height);
                Rectangle wallRectangle = new Rectangle(player.Wall.getX(), player.Wall.getY(), player.Wall.definition.mainTexture.Width, player.Wall.definition.mainTexture.Height);
                if (intersects(npcRectangle, wallRectangle) && (npc.getX() >= (player.Wall.getX() + player.Wall.definition.mainTexture.Width / 2) - npc.definition.mainTexture.Width))
                {
                    Matrix npcMatrix = Matrix.CreateTranslation(npc.getX(), npc.getY(), 0);
                    Matrix wallMatrix = Matrix.CreateTranslation(player.Wall.getX(), player.Wall.getY(), 0);
                    Vector2 collision = texturesCollide(player.Wall.definition.pixels, wallMatrix, npc.definition.pixels, npcMatrix);
                    if (collision.X != -1 && collision.Y != -1)
                    {
                        TimeSpan hitTimePassed = DateTime.Now - npc.lastHit;
                        if (hitTimePassed.TotalMilliseconds >= npc.definition.attackSpeed)
                        {
                            if (player.electricWallTimer > 0)
                            {
                                npc.hit(new Hit(npc, player, player.Damage));
                            }
                            player.hit(new Hit(player, npc, GameWorld.random.Next(1, npc.Damage)));
                            npc.attacking = true;
                            npc.lastHit = DateTime.Now;
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Collisions the specified projectile.
        /// </summary>
        /// <param name="projectile">The projectile.</param>
        public static bool collision(Projectile projectile)
        {
            /*//TODO fix color collision xD
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
                                projectile.Hit.To = npc;
                                npc.hit(projectile.Hit);
                                projectile.destroy();
                                break;
                            //}
                        }
                    }
                }
            }*/
            LinkedList<NPC> currentNPCS = new LinkedList<NPC>(Program.INSTANCE.npcs);
            Rectangle projectileRectangle = new Rectangle(projectile.getX(), projectile.getY(), projectile.definition.mainTexture.Width, projectile.definition.mainTexture.Height);
            //Eerst maken we een matrix aan, die passen we aan op de positie, origine en de rotatie van het projectiel.
            Matrix projectileMatrix = Matrix.CreateRotationZ(projectile.Rotation) * Matrix.CreateTranslation(projectile.getX(), projectile.getY(), 0) * Matrix.Identity;
            foreach (NPC npc in currentNPCS)
            {
                if (npc.visible && !npc.disposed)
                {
                    Rectangle npcRectangle = new Rectangle(npc.getX(), npc.getY(), npc.definition.mainTexture.GetTexture().Width, npc.definition.mainTexture.GetTexture().Height);
                    if (intersects(projectileRectangle, npcRectangle)) {
                        //Nu maken we de NPC matrix aan. (Die heeft de standaard origine en geen rotatie)
                        Matrix npcMatrix = Matrix.CreateTranslation(npc.getX(), npc.getY(), 0) * Matrix.Identity;
                        Vector2 collision = texturesCollide(npc.definition.pixels, npcMatrix, projectile.definition.pixels, projectileMatrix);
                        if (collision.X != -1 && collision.Y != -1)
                        {
                            projectile.Hit.To = npc;
                            npc.hit(projectile.Hit);
                            projectile.Dispose();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Calculates if x and y coord are on the screen.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static bool OnScreen(int x, int y, int width, int height) {
            if ((x + width) < 0)
            {
                return false;
            }
            else if (x > GameFrame.Width)
            {
                return false;
            }
            if ((y + height) < 0)
            {
                return false;
            }
            else if (y > GameFrame.Height)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Distances the specified x and y.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns></returns>
        public static double distance(int x, int y, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x, 2) + Math.Pow(y2 - y, 2));
        }

        /// <summary>
        /// Textureses the collide.
        /// </summary>
        /// <param name="tex1">The tex1.</param>
        /// <param name="mat1">The mat1.</param>
        /// <param name="tex2">The tex2.</param>
        /// <param name="mat2">The mat2.</param>
        /// <returns></returns>
        private static Vector2 texturesCollide(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2)
        {
            Matrix mat1to2 = mat1 * Matrix.Invert(mat2);
            int width1 = tex1.GetLength(0); 
            int height1 = tex1.GetLength(1);
            int width2 = tex2.GetLength(0);
            int height2 = tex2.GetLength(1);

            for (int x1 = 0; x1 < width1; x1++)
            {
                for (int y1 = 0; y1 < height1; y1++)
                {
                    Vector2 pos1 = new Vector2(x1, y1);
                    Vector2 pos2 = Vector2.Transform(pos1, mat1to2);

                    int x2 = (int)pos2.X; //pixel x locatie
                    int y2 = (int)pos2.Y; //pixel y locatie
                    if ((x2 >= 0) && (x2 < width2)) //collision op x-as
                    {
                        if ((y2 >= 0) && (y2 < height2)) //collision op y-as
                        {
                            if (tex1[x1, y1].A > 0) //pixels van texture1 niet transparant
                            {
                                if (tex2[x2, y2].A > 0) //pixels van texture2 niet transparant
                                {
                                    Vector2 screenPos = Vector2.Transform(pos1, mat1); //Pixel positie op het scherm.
                                    return screenPos;
                                }
                            }
                        }
                    }
                }
            }

            return new Vector2(-1, -1);
        }

        /// <summary>
        /// Colors the collision.
        /// </summary>
        /// <param name="collideColor">Color of the collide.</param>
        /// <param name="projectile">The cross hair.</param>
        /// <param name="npc">The NPC.</param>
        /// <param name="projectilePixels">The projectile pixels.</param>
        /// <param name="npcPixels">The NPC pixels.</param>
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
