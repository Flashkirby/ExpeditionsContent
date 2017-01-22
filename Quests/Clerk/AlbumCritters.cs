using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumCritters : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Critter Album";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;

            expedition.conditionDescriptionCountable = "Collect photos";
            expedition.conditionCountedMax = 2;
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.Keybrand);
        }
        public override string Description(bool complete)
        {
            return "I need pictures. Pictures of: Bunny, Bird";
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (API.InInventory[mod.ItemType<Items.Photo>()])
            {
                foreach(Item i in player.inventory)
                {
                    if(i.type == ExpeditionC.ItemIDPhoto)
                    {
                        if (i.stack == NPCID.Bunny) count++;
                        if (i.stack == NPCID.Bird) count++;
                    }
                }
            }
        }
    }
}
