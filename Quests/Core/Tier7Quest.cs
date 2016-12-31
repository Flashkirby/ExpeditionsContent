using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class Tier7Quest : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Thrashing Wilds";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 7;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;

        }
        public override void AddItemsOnLoad()
        {
            expedition.AddDeliverable(ItemID.ChlorophyteOre, 2);
            expedition.AddDeliverable(ItemID.LifeFruit, 1);

            AddRewardMoney(Item.buyPrice(0, 12, 0, 0));
            AddRewardItem(API.ItemIDRelicBox, 1);

            // Find more life fruit and chlorophyte
            AddRewardItem(ItemID.MiningPotion, 3);
            AddRewardItem(ItemID.SpelunkerPotion, 3);
            
            // Mushrooms for ranged stuff, enough for a small biome
            AddRewardItem(ItemID.DarkBlueSolution, 20);

            // Always need chests
            AddRewardItem(ItemID.IvyChest);
        }
        public override string Description(bool complete)
        {
            if (complete) return "Some kind of... super tough, organic ore? I wonder if it can grow like other plants? Oh and those life fruits - fascinating! Please, keep a lookout and see if there's anything else of note. I'd greatly appreciate it. ";
            return "Hey, have you been to the jungle recently? Only defeating that mechanical boss seems to have triggered a certain... change to the jungle. Don't suppose you could take a peek? You'll be fine! ";
        }

        public override bool CheckPrerequisites(Player player)
        {
            return
                API.FindExpedition(mod, "Tier6Quest").completed
                && (NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3);
        }
    }
}
