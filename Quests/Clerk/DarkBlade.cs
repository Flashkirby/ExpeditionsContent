using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class DarkBlade : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Dark Blade";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 3;
            expedition.ctgCollect = true;

            expedition.conditionDescription2 = "Craft a weapon from powerful swords";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
        }
        public override string Description(bool complete)
        {
            if (complete)
            {
                if (Main.hardMode)
                {
                    return "Ooh, now that looks fancy! With a weapon like that you could take on anything! ";
                }
                else
                {
                    return "Ooh, now that looks fancy! With a weapon like that you could take on... almost anything? ";
                }
            }
            string name = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
            if (name == "") name = "the guide";
            return "What do you suppose would happen if you get some of the most powerful swords... and fuse them together at an altar? I'd ask " + name + ", but where's the fun in that? Do it for science! ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Hide after mech boss fights
            if (!expedition.completed && NPC.downedMechBossAny) return false;

            if(!cond1)
            {
                if (API.TimeAfternoon && NPC.downedBoss3)
                {
                    cond1 = true;
                }
            }
            return cond1;
        }

        public override void OnCraftItem(Item item, Recipe recipe)
        {
            if(!expedition.condition2Met)
            {
                expedition.condition2Met = item.type == ItemID.NightsEdge;
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond2;
        }
    }
}
