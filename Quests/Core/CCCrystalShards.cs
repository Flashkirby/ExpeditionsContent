using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CCCrystalShards : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Great Crystal Maze";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 4;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.CrystalShard);

            AddRewardMoney(Item.buyPrice(0, 3, 0, 0));
            AddRewardItem(ItemID.SoulofLight, 3);
        }
        public override string Description(bool complete)
        {
            return "The crystals growing in the underground hallow are extraordinarily useful. They are required to make greater healing and mana potions, can be used to upgrade phasesabers, and their value makes them an effective source of income. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until plantera is defeated, or is done already
            if (!expedition.completed && NPC.downedPlantBoss) return false;

            if (!expedition.condition1Met)
            {
                expedition.condition1Met = player.ZoneHoly && (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight);
            }

            // Appears once altar smashing turned in chain starts and in the hallow
            return Main.hardMode && expedition.condition1Met;
        }
    }
}