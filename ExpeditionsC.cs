using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Expeditions;
using System.IO;

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

        private static int npcidclerk;
        public static int NPCIDClerk { get { return npcidclerk; } }
        private static int itemidphoto;
        public static int ItemIDPhoto { get { return itemidphoto; } }


        public static Texture2D CameraFrameTexture;

        public const int InvasionIDGoblins = 1;
        public const int InvasionIDFrostLegion = 2;
        public const int InvasionIDPirates = 3;
        public const int InvasionIDMartians = 4;

        public override void Load()
        {
            npcidclerk = NPCType("Clerk");
            itemidphoto = ItemType<Items.Photo>();

            if (Main.netMode != 2)
            {
                CameraFrameTexture = GetTexture("Gores/CameraFrame");
            }

            FullMapInitialise();

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
            API.AddExpedition(this, new Quests.Core.ADMushrooms());
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

            #region Core Quests Hard
            //Block 4
            API.AddExpedition(this, new Quests.Core.CAHardMode());
            API.AddExpedition(this, new Quests.Core.CASnowArmy());
            API.AddExpedition(this, new Quests.Core.CBAltarBlessing());
            API.AddExpedition(this, new Quests.Core.CBTracingSteps());
            API.AddExpedition(this, new Quests.Core.CBLivingLoot());
            API.AddExpedition(this, new Quests.Core.CBSoaringSkies());
            API.AddExpedition(this, new Quests.Core.CCCrystalShards());
            API.AddExpedition(this, new Quests.Core.CCGreenFlames());
            API.AddExpedition(this, new Quests.Core.CCBloodOfGods());
            API.AddExpedition(this, new Quests.Core.CCPirates());
            API.AddExpedition(this, new Quests.Core.CCMonsterLoot());
            API.AddExpedition(this, new Quests.Core.CCAvatarOfFrost());
            API.AddExpedition(this, new Quests.Core.CCForbiddenSun());
            //Block 5
            API.AddExpedition(this, new Quests.Core.DAMechaWorm());
            API.AddExpedition(this, new Quests.Core.DAMechaEyes());
            API.AddExpedition(this, new Quests.Core.DAMechaPrime());
            API.AddExpedition(this, new Quests.Core.DBFruitsOfLabour());
            API.AddExpedition(this, new Quests.Core.DBHallowed());
            API.AddExpedition(this, new Quests.Core.DBFungalFunk());
            //Block 6
            API.AddExpedition(this, new Quests.Core.DCPlanterror());
            API.AddExpedition(this, new Quests.Core.EASolarEclipse());
            API.AddExpedition(this, new Quests.Core.EAGhostBusters());
            #endregion

            #region Clerk
            // Clerk Sidequests
            API.AddExpedition(this, new Quests.Clerk.ShopInventory());
            API.AddExpedition(this, new Quests.Clerk.BeaconOfPurity());
            API.AddExpedition(this, new Quests.Clerk.SunkenTreasure());
            API.AddExpedition(this, new Quests.Clerk.CrystalHeart());
            API.AddExpedition(this, new Quests.Clerk.BloodMoonDefence());
            API.AddExpedition(this, new Quests.Clerk.DarkBlade());

            // Clerk SOS
            API.AddExpedition(this, new Quests.Clerk.SOSAngler());
            API.AddExpedition(this, new Quests.Clerk.SOSStylist());
            API.AddExpedition(this, new Quests.Clerk.SOSTinkerer());
            API.AddExpedition(this, new Quests.Clerk.SOSMechanic());
            API.AddExpedition(this, new Quests.Clerk.SOSWizard());

            // Album Builders
            API.AddExpedition(this, new Quests.Clerk.AlbumCritters());
            #endregion

            // DD2
            API.AddExpedition(this, new Quests.MiscPre.DryadDD2());
            API.AddExpedition(this, new Quests.MiscPre.DD2InvasionT1());
            API.AddExpedition(this, new Quests.MiscPre.DD2InvasionT2());
            API.AddExpedition(this, new Quests.MiscPre.DD2InvasionT3());
            API.AddExpedition(this, new Quests.MiscPre.DD2BigFan());


            #region Travelling Merchant Trades
            API.AddExpedition(this, new Quests.TravMerch.Compass());
            API.AddExpedition(this, new Quests.TravMerch.Blowpipe());
            API.AddExpedition(this, new Quests.TravMerch.PrePair1BandOfStarpower());
            API.AddExpedition(this, new Quests.TravMerch.PrePair1PanicNecklace());
            API.AddExpedition(this, new Quests.TravMerch.PrePair2BallOHurt());
            API.AddExpedition(this, new Quests.TravMerch.PrePair2TheRottedFork());
            API.AddExpedition(this, new Quests.TravMerch.PrePair3Vilethorn());
            API.AddExpedition(this, new Quests.TravMerch.PrePair3CrimsonRod());
            API.AddExpedition(this, new Quests.TravMerch.PostPair1ClingerStaff());
            API.AddExpedition(this, new Quests.TravMerch.PostPair1LifeDrain());
            API.AddExpedition(this, new Quests.TravMerch.PostPair2PutridScent());
            API.AddExpedition(this, new Quests.TravMerch.PostPair2FleshKnuckles());
            API.AddExpedition(this, new Quests.TravMerch.PostPair3ChainGuillotines());
            API.AddExpedition(this, new Quests.TravMerch.PostPair3FetidBaghnaks());
            #endregion
        }

        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            PhotoManager.ResetFrame();
        }

        public static List<Point> heartTiles;
        public static List<Point> fruitTiles;
        public static List<Vector2> fallenStarPos;
        private void FullMapInitialise()
        {
            heartTiles = new List<Point>();
            fruitTiles = new List<Point>();
            fallenStarPos = new List<Vector2>();
        }
        public override void PostDrawFullscreenMap(ref string mouseText)
        {
            Player player = Main.player[Main.myPlayer];
            PlayerExplorer px = PlayerExplorer.Get(player, this);
            if (px.accHeartCompass ||
                px.accFruitCompass ||
                px.stargazer)
            {
                UpdateMapLocations(player, px);
                DrawIcons();
            }
        }
        
        #region Mapping

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
            }
            catch (Exception e)
            {
                Main.NewTextMultiline("Everythings going wrong:" + e.ToString());
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
            Texture2D heart = null;
            Texture2D fruit = null;
            Texture2D star = null;
            Vector2 drawPosition = new Vector2(); ;
            try
            {
                heart = Main.itemTexture[ItemID.LifeCrystal];
                fruit = Main.itemTexture[ItemID.LifeFruit];
                star = Main.itemTexture[ItemID.FallenStar];
                drawPosition = new Vector2();
            }
            catch(Exception e) { Main.NewTextMultiline("Texture array: " + e.ToString()); }

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

        #endregion

    }
}