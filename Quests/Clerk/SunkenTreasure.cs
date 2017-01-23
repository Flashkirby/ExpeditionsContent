using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class SunkenTreasure : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Sunken Treasure";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.IronCrate);

            AddRewardItem(API.ItemIDRustedBox);
        }
        public override string Description(bool complete)
        {
            if (complete) return "Thanks, I'll have it sent off. If you're wondering what I gave you, it's an old delivery box we used to use to send items around, so it could contain almost anything! Exciting huh? ";
            return "Have you tried out fishing yet? As well as being a source of potion ingredients, you can also fish up crates of old or lost goods. If you wouldn't mind, I'd like to send one for examination of it's in good enough condition. ";
        }
    }
}
