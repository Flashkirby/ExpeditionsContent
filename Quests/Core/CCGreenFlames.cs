using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CCGreenFlames : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Cursed Inferno";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 4;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.CursedFlame);

            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
            AddRewardItem(ItemID.SoulofNight, 3);
        }
        public override string Description(bool complete)
        {
            return "Certain enemies, and worms in particular, drop cursed flames in the corruption. These flames are great for upgrading the raw damage of ranged ammo, and can apply a little bit of extra damage to afflicted foes. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until plantera is defeated, or is done already
            if (!expedition.completed && NPC.downedPlantBoss) return false;

            if(!cond1 && !WorldGen.crimson)
            {
                cond1 = player.ZoneCrimson;
            }

            // Appears once altar smashing turned in chain starts and corruption world
            return API.FindExpedition<CBTracingSteps>(mod).completed && (!WorldGen.crimson || cond1);
        }
    }
}
