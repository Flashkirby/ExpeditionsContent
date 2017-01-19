using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CCBloodOfGods : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Going Gold";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 4;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.Ichor);

            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
            AddRewardItem(ItemID.SoulofNight, 3);
        }
        public override string Description(bool complete)
        {
            return "The sickly stickers of the crimson carry within them pools of ichor. This powerful liquid greatly lowers the defense of enemies it is inflicted upon as well as glowing, making it a valuable asset in any fight. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until plantera is defeated, or is done already
            if (!expedition.completed && NPC.downedPlantBoss) return false;
            
            if (!cond1 && WorldGen.crimson)
            {
                cond1 = player.ZoneCorrupt;
            }

            // Appears once altar smashing turned in chain starts and crimson world
            return API.FindExpedition<CBTracingSteps>(mod).completed && (WorldGen.crimson || cond1);
        }
    }
}
