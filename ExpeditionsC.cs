using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Expeditions;

namespace ExpeditionsContent {
    public class ExpeditionC : Mod
    {
        public ExpeditionC()
        {
            Properties = new ModProperties()
            {
                Autoload = true
            };
        }

        private static int _npcClerk;
        public static int npcClerk { get { return _npcClerk; } }

        public override void Load()
        {
            _npcClerk = NPCType("Clerk");

            heartTiles = new List<Point>();

            API.AddExpedition(this, new Quests.Tier0.MakingBase());
            API.AddExpedition(this, new Quests.Tier0.BeaconOfPurity());
        }

        public static List<Point> heartTiles;
        public override void PostDrawFullscreenMap(ref string mouseText)
        {
            UpdateHeartLocations();

            DrawHeartIcons();
        }

        private static void UpdateHeartLocations()
        {
            if ((int)Main.time % 30 == 0)
            { heartTiles.Clear(); }
            else { return; }



            Player player = Main.player[Main.myPlayer];

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

            try
            {
                for (int y = topTiles + 1; y < bottomTiles; y += 2)
                {
                    for (int x = leftTiles + 1; x < rightTiles; x += 2)
                    {
                        try
                        {
                            Tile t = Main.tile[x, y];
                            if (t == null) continue;

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
                        catch (Exception e)
                        {
                            Main.NewTextMultiline(e.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Main.NewTextMultiline(e.ToString());
            }
        }

        private static void DrawHeartIcons()
        {
            Texture2D heart = Main.itemTexture[ItemID.LifeCrystal];
            Point drawPosition = new Point();

            foreach (Point heartTile in heartTiles)
            {
                Vector2 tilePos = new Vector2(heartTile.X + 1f, heartTile.Y + 1f);
                Vector2 halfScreen = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);

                Vector2 relativePos = tilePos - Main.mapFullscreenPos;
                relativePos *= Main.mapFullscreenScale / 16;
                relativePos = relativePos * 16 + halfScreen;

                drawPosition = new Point(
                    (int)relativePos.X,
                    (int)relativePos.Y);

                Rectangle drawPos = new Rectangle(drawPosition.X, drawPosition.Y, heart.Width, heart.Height);
                Main.spriteBatch.Draw(
                    heart,
                    drawPos,
                    null,
                    Color.White,
                    0f,
                    new Vector2(heart.Width / 2, heart.Height / 2),
                    SpriteEffects.None,
                    0f
                    );
            }
        }
    }
}