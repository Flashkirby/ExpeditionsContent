using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumMushi : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Weird Mushrooms";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Anomura Fungus";
            expedition.conditionDescription2 = "Mushroom Zombie";
            expedition.conditionDescription3 = "Fungi Bulb";
            expedition.conditionCountedMax = 3;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumMushroom>());
        }
        public override string Description(bool complete)
        {
            return "Have you seen any of the monsters found in mushroom biomes? I hear they look really weird, like they've been taken over by spores or something. Take some photos! ";
        }
        #region Photo Bools
        public static bool Anomura
        { get { return PhotoManager.PhotoOfNPC[NPCID.AnomuraFungus]; } }
        public static bool Zombie
        { get { return PhotoManager.PhotoOfNPC[NPCID.ZombieMushroom] || PhotoManager.PhotoOfNPC[NPCID.ZombieMushroomHat]; } }
        public static bool FungiB
        { get { return PhotoManager.PhotoOfNPC[NPCID.FungiBulb]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (PlayerExplorer.HoldingCamera(mod)
                && API.FindExpedition<AlbumOmnibus1>(mod).completed)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (Anomura) count++;
            if (Zombie) count++;
            if (FungiB) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Anomura;
            cond2 = Zombie;
            cond3 = FungiB;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.AnomuraFungus);
            if (!PhotoManager.ConsumePhoto(NPCID.ZombieMushroom))
            {
                PhotoManager.ConsumePhoto(NPCID.ZombieMushroomHat);
            }
            PhotoManager.ConsumePhoto(NPCID.GlowingSnail);

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
