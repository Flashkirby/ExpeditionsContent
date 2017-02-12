using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Expeditions;
using System.IO;

namespace ExpeditionsContent
{
    public static class ModMapController
    {
        public static List<Point> heartTiles;
        public static List<Point> fruitTiles;
        public static List<Point> shrineTiles;
        public static List<Vector2> fallenStarPos;
        internal static void FullMapInitialise()
        {
            heartTiles = new List<Point>();
            fruitTiles = new List<Point>();
            shrineTiles = new List<Point>();
            fallenStarPos = new List<Vector2>();
        }

        public static void DrawFullscreenMap(Mod mod, ref string mouseText)
        {
            Player player = Main.player[Main.myPlayer];
            PlayerExplorer px = PlayerExplorer.Get(player, mod);
            if (px.accHeartCompass ||
                px.accFruitCompass ||
                px.stargazer)
            {
                UpdateMapLocations(player, px);
                DrawIcons();
            }
        }

        private static void UpdateMapLocations(Player player, PlayerExplorer px)
        {
            fallenStarPos.Clear();
            if (px.stargazer)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.projectile[i].active) continue;
                    if (Main.projectile[i].type == ProjectileID.FallingStar
                        && Main.projectile[i].velocity.Y > 0
                        && Main.projectile[i].damage >= 1000)
                    {
                        fallenStarPos.Add(Main.projectile[i].Center);
                    }
                }
            }

            if ((int)Main.time % 30 == 0)
            {
                heartTiles.Clear();
                fruitTiles.Clear();
            }
            else { return; }

            try
            {
                // 800 is 100ft
                int searchRadius = 3200;

                int leftTiles = ((int)player.Center.X - searchRadius + 8) / 16;
                int rightTiles = ((int)player.Center.X + searchRadius + 8) / 16;
                leftTiles = Math.Max(leftTiles, (int)Main.leftWorld / 16);
                rightTiles = Math.Min(rightTiles, (int)Main.rightWorld / 16);

                int topTiles = ((int)player.Center.Y - searchRadius + 8) / 16;
                int bottomTiles = ((int)player.Center.Y + searchRadius + 8) / 16;
                topTiles = Math.Max(topTiles, (int)Main.topWorld / 16);
                bottomTiles = Math.Min(bottomTiles, (int)Main.bottomWorld / 16);

                for (int y = topTiles + 1; y < bottomTiles; y += 2)
                {
                    for (int x = leftTiles + 1; x < rightTiles; x += 2)
                    {
                        try
                        {
                            Tile t = Main.tile[x, y];
                            if (t == null) continue;

                            if (px.accHeartCompass) AddHeart(y, x, t);
                            if (px.accFruitCompass) AddFruit(y, x, t);
                        }
                        catch (Exception e)
                        {
                            Main.NewTextMultiline("Tile OOB: " + e.ToString());
                        }
                    }
                }

                // Search the map for these tiles, only update once in a while
                if ((int)Main.time % 60 == 0)
                {
                    shrineTiles.Clear();
                    for (int y = 1 + (int)Main.topWorld / 16; y < Main.bottomWorld / 16; y += 2)
                    {
                        for (int x = 1 + (int)Main.leftWorld / 16; x < Main.rightWorld / 16; x += 3)
                        {
                            Tile t = Main.tile[x, y];
                            if (t == null) continue;

                            if (y < Main.bottomWorld / 32) // Only above halfway
                            {
                                if (px.accShrineMap) AddShrine(y, x, t);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Main.NewTextMultiline("Everything's going wrong:" + e.ToString());
            }
        }

        private static void AddHeart(int y, int x, Tile t)
        {
            if (t.type == TileID.Heart)
            {
                // Top Left Corner
                if (t.frameX == 0 && t.frameY == 0)
                { heartTiles.Add(new Point(x, y)); }
                else
                // Top Right Corner
                if (t.frameX == 18 && t.frameY == 0)
                { heartTiles.Add(new Point(x - 1, y)); }
                else
                // Bot Left Corner
                if (t.frameX == 0 && t.frameY == 18)
                { heartTiles.Add(new Point(x, y - 1)); }
                else
                // Bot Right Corner
                if (t.frameX == 18 && t.frameY == 18)
                { heartTiles.Add(new Point(x - 1, y - 1)); }
            }
        }

        private static void AddFruit(int y, int x, Tile t)
        {
            if (t.type == TileID.LifeFruit)
            {
                bool left = ((t.frameX / 18) % 2 == 0);
                bool top = ((t.frameY / 18) % 2 == 0);
                // Top Left Corner
                if (left && top)
                { fruitTiles.Add(new Point(x, y)); }
                else
                // Top Right Corner
                if (!left && top)
                { fruitTiles.Add(new Point(x - 1, y)); }
                else
                // Bot Left Corner
                if (left && !top)
                { fruitTiles.Add(new Point(x, y - 1)); }
                else
                // Bot Right Corner
                if (!left && !top)
                { fruitTiles.Add(new Point(x - 1, y - 1)); }
            }
        }

        const int frameTile = 18;
        private static void AddShrine(int y, int x, Tile t)
        {
            // Fake/real sword embedded
            if (t.type == 186 || t.type == 187)
            {
                const int realOffsetX = 918;
                const int realOffsetY = 0;
                const int fakeOffsetX = 810;
                const int fakeOffsetY = 0;
                for (int height = 0; height < 2; height++)
                {
                    for (int width = 0; width < 3; width++)
                    {
                        int offsetX = fakeOffsetX;
                        int offsetY = fakeOffsetY;
                        if(t.type == 187)
                        {
                            offsetX = realOffsetX;
                            offsetY = realOffsetY;
                        }
                        if (t.frameX == offsetX + width * frameTile &&
                            t.frameY == offsetY + height * frameTile)
                        {
                            //Main.NewText("Add " + t.type + " at a shrine? " + t.frameX + " & " + t.frameY);
                            shrineTiles.Add(new Point(x - width, y - height));
                        }
                    }
                }
            }
        }

        private static void DrawIcons()
        {
            Texture2D heart = null;
            Texture2D fruit = null;
            Texture2D shrine = null;
            Texture2D star = null;
            Vector2 drawPosition = new Vector2(); ;
            try
            {
                heart = Main.itemTexture[ItemID.LifeCrystal];
                fruit = Main.itemTexture[ItemID.LifeFruit];
                shrine = Main.itemTexture[ItemID.PlatinumShortsword];
                star = Main.itemTexture[ItemID.FallenStar];
                drawPosition = new Vector2();
            }
            catch (Exception e) { Main.NewTextMultiline("Texture array: " + e.ToString()); }

            try
            {
                foreach (Point heartTile in heartTiles)
                {
                    drawPosition = CalculateDrawPos(new Vector2(heartTile.X + 1f, heartTile.Y + 1f));
                    DrawTextureOnMap(heart, drawPosition);
                }
                foreach (Point fruitTile in fruitTiles)
                {
                    drawPosition = CalculateDrawPos(new Vector2(fruitTile.X + 1f, fruitTile.Y + 1f));
                    DrawTextureOnMap(fruit, drawPosition);
                }
                foreach (Point shrineTile in shrineTiles)
                {
                    drawPosition = CalculateDrawPos(new Vector2(shrineTile.X + 1.5f, shrineTile.Y + 1f));
                    DrawTextureOnMap(shrine, drawPosition);
                }
                foreach (Vector2 fallenStar in fallenStarPos)
                {
                    drawPosition = CalculateDrawPos(new Vector2(fallenStar.X / 16, fallenStar.Y / 16));
                    DrawTextureOnMap(star, drawPosition);
                }
            }
            catch (Exception e) { Main.NewTextMultiline("Adding icons : " + e.ToString()); }
        }

        private static Vector2 CalculateDrawPos(Vector2 tilePos)
        {
            Vector2 halfScreen = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            Vector2 relativePos = tilePos - Main.mapFullscreenPos;
            relativePos *= Main.mapFullscreenScale / 16;
            relativePos = relativePos * 16 + halfScreen;

            Vector2 drawPosition = new Vector2(
                (int)relativePos.X,
                (int)relativePos.Y);
            return drawPosition;
        }

        private static void DrawTextureOnMap(Texture2D texture, Vector2 drawPosition)
        {
            Rectangle drawPos = new Rectangle((int)drawPosition.X, (int)drawPosition.Y, texture.Width, texture.Height);
            Main.spriteBatch.Draw(
                texture,
                drawPos,
                null,
                Color.White,
                0f,
                new Vector2(texture.Width / 2, texture.Height / 2),
                SpriteEffects.None,
                0f
                );
        }
    }
}
