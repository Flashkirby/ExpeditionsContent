using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumUndead : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Zombies";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Zombie Variants";
            expedition.conditionDescription2 = "Raincoat Zombie";
            expedition.conditionDescription3 = "Zombie Eskimo";
            expedition.conditionCountedMax = 9;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumUndead>());
        }
        public override string Description(bool complete)
        {
            return "This place is crawling with zombies at night. Frankly, it's worrying... where did they come from? Perhaps a better look at the different kinds you might encounter could help? ";
        }
        #region Photo Bools
        public static bool Z1
        { get { return PhotoManager.PhotoOfNPC[NPCID.Zombie] || PhotoManager.PhotoOfNPC[NPCID.ArmedZombie]; } }
        public static bool Z2
        { get { return PhotoManager.PhotoOfNPC[NPCID.BaldZombie]; } }
        public static bool Z3
        { get { return PhotoManager.PhotoOfNPC[NPCID.PincushionZombie] || PhotoManager.PhotoOfNPC[NPCID.ArmedZombiePincussion]; } }
        public static bool Z4
        { get { return PhotoManager.PhotoOfNPC[NPCID.SlimedZombie] || PhotoManager.PhotoOfNPC[NPCID.ArmedZombieSlimed]; } }
        public static bool Z5
        { get { return PhotoManager.PhotoOfNPC[NPCID.SwampZombie] || PhotoManager.PhotoOfNPC[NPCID.ArmedZombieSwamp]; } }
        public static bool Z6
        { get { return PhotoManager.PhotoOfNPC[NPCID.TwiggyZombie] || PhotoManager.PhotoOfNPC[NPCID.ArmedZombieTwiggy]; } }
        public static bool Z7
        { get { return PhotoManager.PhotoOfNPC[NPCID.FemaleZombie] || PhotoManager.PhotoOfNPC[NPCID.ArmedZombieCenx]; } }
        public static bool Raincoat
        { get { return PhotoManager.PhotoOfNPC[NPCID.ZombieRaincoat]; } }
        public static bool Eskimo
        { get { return PhotoManager.PhotoOfNPC[NPCID.ZombieEskimo] || PhotoManager.PhotoOfNPC[NPCID.ArmedZombieEskimo]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return PlayerExplorer.HoldingCamera(mod)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (Z1) count++;
            if (Z2) count++;
            if (Z3) count++;
            if (Z4) count++;
            if (Z5) count++;
            if (Z6) count++;
            if (Z7) count++;
            if (Raincoat) count++;
            if (Eskimo) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Z1 && Z2 && Z3 && Z4 && Z5 && Z6 && Z7;
            cond2 = Raincoat;
            cond3 = Eskimo;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            if (!PhotoManager.ConsumePhoto(NPCID.ArmedZombie))
            { PhotoManager.ConsumePhoto(NPCID.Zombie); }

            PhotoManager.ConsumePhoto(NPCID.BaldZombie);

            if (!PhotoManager.ConsumePhoto(NPCID.ArmedZombiePincussion))
            { PhotoManager.ConsumePhoto(NPCID.PincushionZombie); }

            if (!PhotoManager.ConsumePhoto(NPCID.ArmedZombieSlimed))
            { PhotoManager.ConsumePhoto(NPCID.SlimedZombie); }

            if (!PhotoManager.ConsumePhoto(NPCID.ArmedZombieSwamp))
            { PhotoManager.ConsumePhoto(NPCID.SwampZombie); }

            if (!PhotoManager.ConsumePhoto(NPCID.ArmedZombieTwiggy))
            { PhotoManager.ConsumePhoto(NPCID.TwiggyZombie); }

            if (!PhotoManager.ConsumePhoto(NPCID.ArmedZombieCenx))
            { PhotoManager.ConsumePhoto(NPCID.FemaleZombie); }

            PhotoManager.ConsumePhoto(NPCID.ZombieRaincoat);

            if (!PhotoManager.ConsumePhoto(NPCID.ArmedZombieEskimo))
            {
                PhotoManager.ConsumePhoto(NPCID.ZombieEskimo);
            }

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}