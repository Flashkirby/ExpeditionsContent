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

            ModMapController.FullMapInitialise();

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
            API.AddExpedition(this, new Quests.Core.ACHouseHome());
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
            API.AddExpedition(this, new Quests.Core.DBLifeFruit());
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
            API.AddExpedition(this, new Quests.Clerk.WayfarerWeapons());
            API.AddExpedition(this, new Quests.Clerk.WayfererGuns());
            API.AddExpedition(this, new Quests.Clerk.BeaconOfPurity());
            API.AddExpedition(this, new Quests.Clerk.SunkenTreasure());
            API.AddExpedition(this, new Quests.Clerk.CrystalHeart());
            API.AddExpedition(this, new Quests.Clerk.SecretSummon());
            API.AddExpedition(this, new Quests.Clerk.SkysTheLimit());
            API.AddExpedition(this, new Quests.Clerk.BloodMoonDefence());
            API.AddExpedition(this, new Quests.Clerk.SecretSummon2());
            API.AddExpedition(this, new Quests.Clerk.RoseByAnyName());
            API.AddExpedition(this, new Quests.Clerk.DarkBlade());
            API.AddExpedition(this, new Quests.Clerk.FruitsOfLabour());

            // Clerk SOS
            API.AddExpedition(this, new Quests.Clerk.SOSAngler());
            API.AddExpedition(this, new Quests.Clerk.SOSStylist());
            API.AddExpedition(this, new Quests.Clerk.SOSTinkerer());
            API.AddExpedition(this, new Quests.Clerk.SOSMechanic());
            API.AddExpedition(this, new Quests.Clerk.SOSWizard());

            // Album Builders
            API.AddExpedition(this, new Quests.Clerk.ProCamSkill());
            API.AddExpedition(this, new Quests.Clerk.AlbumCritters());
            API.AddExpedition(this, new Quests.Clerk.AlbumCritters2());
            API.AddExpedition(this, new Quests.Clerk.AlbumCritters3());
            #endregion

            #region DD2
            // DD2
            API.AddExpedition(this, new Quests.MiscPre.DryadDD2());
            API.AddExpedition(this, new Quests.MiscPre.DD2InvasionT1());
            API.AddExpedition(this, new Quests.MiscPre.DD2InvasionT2());
            API.AddExpedition(this, new Quests.MiscPre.DD2InvasionT3());
            API.AddExpedition(this, new Quests.MiscPre.DD2BigFan());
            #endregion

            #region Travelling Merchant Trades
            API.AddExpedition(this, new Quests.TravMerch.Compass()); // INFACC: Compass
            API.AddExpedition(this, new Quests.TravMerch.Blowpipe());
            API.AddExpedition(this, new Quests.TravMerch.PinkGel());
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

            #region Daily
            API.AddExpedition(this, new Quests.Daily.DrinkingContest()); // INFACC: Depth Meter

            API.AddExpedition(this, new Quests.Daily.BossEoC());
            API.AddExpedition(this, new Quests.Daily.BossEoW());
            API.AddExpedition(this, new Quests.Daily.BossBoC());
            #endregion
            #region Daily - Super Snap
            API.AddExpedition(this, new Quests.Daily.SnapPreBee());
            API.AddExpedition(this, new Quests.Daily.SnapPreBoneSerpent());
            API.AddExpedition(this, new Quests.Daily.SnapPreGoblin());
            API.AddExpedition(this, new Quests.Daily.SnapPreLacBeetle());
            API.AddExpedition(this, new Quests.Daily.SnapPreShark());
            API.AddExpedition(this, new Quests.Daily.SnapPreSnowFlinx());
            API.AddExpedition(this, new Quests.Daily.SnapPreSporeZombie());
            API.AddExpedition(this, new Quests.Daily.SnapPreUndeadMiner());
            API.AddExpedition(this, new Quests.Daily.SnapSpider());

            API.AddExpedition(this, new Quests.Daily.SnapHardBloodJelly());
            API.AddExpedition(this, new Quests.Daily.SnapHardChaosElemental());
            API.AddExpedition(this, new Quests.Daily.SnapHardCorruptSlime());
            API.AddExpedition(this, new Quests.Daily.SnapHardDesertSpirit());
            API.AddExpedition(this, new Quests.Daily.SnapHardMedusa());
            API.AddExpedition(this, new Quests.Daily.SnapHardSnapHardPigron());
            API.AddExpedition(this, new Quests.Daily.SnapHardWolf());
            API.AddExpedition(this, new Quests.Daily.SnapHardWyvern());

            API.AddExpedition(this, new Quests.Daily.SnapHardFlyingSerpent());
            API.AddExpedition(this, new Quests.Daily.SnapHardDungeonSpirit());
            #endregion
            #region Daily - Merchant Moneygrabs
            API.AddExpedition(this, new Quests.Daily.MerchGel());
            API.AddExpedition(this, new Quests.Daily.MerchFishbowl());
            API.AddExpedition(this, new Quests.Daily.MerchPinkGel());
            API.AddExpedition(this, new Quests.Daily.MerchTopWatch());
            API.AddExpedition(this, new Quests.Daily.MerchStucco());
            API.AddExpedition(this, new Quests.Daily.MerchFadBoreal());
            API.AddExpedition(this, new Quests.Daily.MerchFadCactus());
            API.AddExpedition(this, new Quests.Daily.MerchFadEbonwood());
            API.AddExpedition(this, new Quests.Daily.MerchFadGranite());
            API.AddExpedition(this, new Quests.Daily.MerchFadMarble());
            API.AddExpedition(this, new Quests.Daily.MerchFadMahogany());
            API.AddExpedition(this, new Quests.Daily.MerchFadPalm());
            API.AddExpedition(this, new Quests.Daily.MerchFadShadewood());
            API.AddExpedition(this, new Quests.Daily.MerchFadFrozen());
            API.AddExpedition(this, new Quests.Daily.MerchFadGlass());
            API.AddExpedition(this, new Quests.Daily.MerchFadLivingWood());
            API.AddExpedition(this, new Quests.Daily.MerchFadMeteor());
            API.AddExpedition(this, new Quests.Daily.MerchFadSkyware());
            API.AddExpedition(this, new Quests.Daily.MerchFadSlime());

            API.AddExpedition(this, new Quests.Daily.MerchCrystals());
            API.AddExpedition(this, new Quests.Daily.MerchFadPearlwood());
            API.AddExpedition(this, new Quests.Daily.MerchFadSpookyWood());
            API.AddExpedition(this, new Quests.Daily.MerchFadMartian());
            #endregion
        }

        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            PhotoManager.ResetFrame();
        }

        public override void PostDrawFullscreenMap(ref string mouseText)
        {
            ModMapController.DrawFullscreenMap(this, ref mouseText);
        }
    }
}