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
                Autoload = true,
                AutoloadGores = true
            };
        }

        private static int _npcClerk;
        public static int npcClerk { get { return _npcClerk; } }

        public const int InvasionIDGoblins = 1;
        public const int InvasionIDFrostLegion = 2;
        public const int InvasionIDPirates = 3;
        public const int InvasionIDMartians = 4;

        public override void Load()
        {
            _npcClerk = NPCType("Clerk");

            heartTiles = new List<Point>();
            fruitTiles = new List<Point>();
            

            API.AddExpedition(this, new Quests.MiscPre.MakingBase());

            #region Core Quests

            //Block 1
            API.AddExpedition(this, new Quests.Core.AAWelcomeQuest());
            API.AddExpedition(this, new Quests.Core.ABSmeltOres());
            API.AddExpedition(this, new Quests.Core.ABStartTown());
            API.AddExpedition(this, new Quests.Core.ABMapping());
            API.AddExpedition(this, new Quests.Core.ACMakeMagic());
            API.AddExpedition(this, new Quests.Core.ACUnderground());
            API.AddExpedition(this, new Quests.Core.ACTownfolk());
            API.AddExpedition(this, new Quests.Core.ADHooks());
            API.AddExpedition(this, new Quests.Core.ADLifeCrystals());
            API.AddExpedition(this, new Quests.Core.ADLifeCrystals());
            API.AddExpedition(this, new Quests.Core.ADKingSlime());
            //Block 2
            API.AddExpedition(this, new Quests.Core.BAEvilEye());
            API.AddExpedition(this, new Quests.Core.BBHarbinger());
            API.AddExpedition(this, new Quests.Core.BCBoss2());
            API.AddExpedition(this, new Quests.Core.BCGoblins());
            API.AddExpedition(this, new Quests.Core.BCMeteorite());
            //Block 3
            API.AddExpedition(this, new Quests.Core.BDJungles());
            API.AddExpedition(this, new Quests.Core.BDFossils());
            API.AddExpedition(this, new Quests.Core.BDQBee());
            API.AddExpedition(this, new Quests.Core.BDDungeonSkell());
            API.AddExpedition(this, new Quests.Core.BDHellArmour());
            API.AddExpedition(this, new Quests.Core.BETheWall());

            #endregion

            API.AddExpedition(this, new Quests.Clerk.BeaconOfPurity());
            API.AddExpedition(this, new Quests.Clerk.SOSAngler());
            API.AddExpedition(this, new Quests.Clerk.SOSStylist());
            API.AddExpedition(this, new Quests.Clerk.CrystalHeart());
            API.AddExpedition(this, new Quests.Clerk.SOSTinkerer());
            API.AddExpedition(this, new Quests.Clerk.SOSMechanic());
            API.AddExpedition(this, new Quests.MiscPre.DryadDD2());

            // Hard Mode

            #region Core Quests Hard

            //Block 4
            API.AddExpedition(this, new Quests.Core.CAHardMode());
            //Block 5

            #endregion

            API.AddExpedition(this, new Quests.Clerk.SOSWizard());
        }

        public static List<Point> heartTiles;
        public static List<Point> fruitTiles;
        public override void PostDrawFullscreenMap(ref string mouseText)
        {
            Player player = Main.player[Main.myPlayer];
            PlayerExplorer px = PlayerExplorer.Get(player, this);
            if (px.accHeartCompass ||
                px.accFruitCompass)
            {
                UpdateMapLocations(player, px);
                DrawIcons();
            }
        }

        #region Mapping

        private static void UpdateMapLocations(Player player, PlayerExplorer px)
        {
            if ((int)Main.time % 30 == 0)
            {
                heartTiles.Clear();
                fruitTiles.Clear();
            }
            else { return; }

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

                            if (px.accHeartCompass) AddHeart(y, x, t);
                            if (px.accFruitCompass) AddFruit(y, x, t);
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

        private static void DrawIcons()
        {
            Texture2D heart = Main.itemTexture[ItemID.LifeCrystal];
            Texture2D fruit = Main.itemTexture[ItemID.LifeFruit];
            Point drawPosition = new Point();

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
        }

        private static Point CalculateDrawPos(Vector2 tilePos)
        {
            Vector2 halfScreen = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            Vector2 relativePos = tilePos - Main.mapFullscreenPos;
            relativePos *= Main.mapFullscreenScale / 16;
            relativePos = relativePos * 16 + halfScreen;

            Point drawPosition = new Point(
                (int)relativePos.X,
                (int)relativePos.Y);
            return drawPosition;
        }

        private static void DrawTextureOnMap(Texture2D texture, Point drawPosition)
        {
            Rectangle drawPos = new Rectangle(drawPosition.X, drawPosition.Y, texture.Width, texture.Height);
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

        #endregion
    }
}