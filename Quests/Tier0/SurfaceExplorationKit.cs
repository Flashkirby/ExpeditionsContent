using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Tier0
{
    class SurfaceExplorationKit : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Surface Exploration Kit";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 0;
            expedition.ctgExplore = true;

            expedition.conditionDescriptionCountable = "Map out an area around spawn";
            expedition.conditionCountedMax = 100;
            expedition.conditionCountedTrackHalfCompleted = true;
        }
        public override void AddItemsOnLoad()
        {
            expedition.AddDeliverable(ItemID.CordageGuide, 1);

            AddRewardItem(ItemID.Aglet);
        }
        public override string Description(bool complete)
        {
            return "Map out the area so you have a good idea of where to build your house. ";
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
/*
                        Vector2 pos = Utils.ToWorldCoordinates(new Point(x, y));
                        if (
                            pos.X > Main.screenPosition.X &&
                            pos.Y > Main.screenPosition.Y &&
                            pos.X < Main.screenPosition.X + Main.screenWidth &&
                            pos.Y < Main.screenPosition.Y + Main.screenHeight
                            )
                        {
                            Dust d = Main.dust[Dust.NewDust(pos, 0, 0, 106)];
                            d.noGravity = true;
                            d.velocity = Vector2.Zero;
                        }
 */
/*
        string reveal = "";
        int tileX = (int)Main.leftWorld / 16;
        int tileY = (int)Main.rightWorld / 16;
        int checkWidth = 64;
        for (int i = 1; i < checkWidth; i++)
        {
            int playerY = Utils.ToTileCoordinates(player.Center).Y;
            if (Main.Map.IsRevealed(tileX + tileY / checkWidth * i, playerY))
            {
                reveal += "@";
            }
            else
            {
                reveal += "-";
            }
        }
        if (Main.time % 30 == 0) Main.NewText(reveal);
        return cond1;
 */
