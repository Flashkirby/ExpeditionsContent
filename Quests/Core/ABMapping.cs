using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ABMapping : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Finding Your Way";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 0;
            expedition.ctgExplore = true;

            expedition.conditionDescriptionCountable = "Map out the spawn area";
            expedition.conditionCountedMax = 100;
            expedition.conditionCountedTrackHalfCompleted = true;
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 0, 5, 0));
            AddRewardItem(ItemID.Wood, 50);
        }
        public override string Description(bool complete)
        {
            return "Your map will automatically record the surroundings as you explore. Try exploring this starting area to get a feel for it. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.FindExpedition<AAWelcomeQuest>(mod).completed;
        }
        
        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            // Check only once every 2 seconds
            if (Main.time % 120 != 0) { return; }
            if (count >= max)
            {
                count = max;
                return;
            }

            // Revealed chunks, as 8x8 tiles.
            // On smallest screen possible, this is roughly equivalent to 20 on the surface
            // 128 is roughly the equivalent to the spawn width with mostly flat ground
            int requiredChunks = 128;
            int revealed = 0;

            // Area around spawn to check
            int spawnWidth = 256;
            int spawnHeight = 256;
            int spawnX = Main.spawnTileX - (spawnWidth / 2);
            int spawnY = Main.spawnTileY - (spawnHeight / 2);

            // Iterate through the map
            for (int y = spawnY; y < spawnY + spawnHeight; y += 8)
            {
                for (int x = spawnX; x < spawnX + spawnWidth; x += 8)
                {
                    try
                    {
                        // Increase if map is revealed
                        if (Main.Map.IsRevealed(x, y)) revealed++;
                    }
                    catch
                    {
                        // If this is off the map, it's not a required chunk anymore
                        requiredChunks--;
                    }
                }
            }
            count = (revealed * 100) / requiredChunks;
            return;
        }
    }
}
