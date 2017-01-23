using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class BloodMoonDefence : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Perfect Partisan";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgSlay = true;
            
            expedition.conditionDescription1 = "Survive a Blood Moon without any town deaths";
            expedition.conditionDescription2 = "Prevent any town deaths";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
            AddRewardItem(ItemID.MoneyTrough);
        }
        public override string Description(bool complete)
        {
            return "Just out of curiosity, how secure is this town against a blood moon? I mean if we can all survive an entire night, during a blood moon, I can definitely reward you for your preparation. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return Main.bloodMoon || expedition.completed || cond1;
        }

        public override void OnNewDay()
        {
            if(!expedition.condition1Met && expedition.condition2Met)
            {
                expedition.condition1Met = true;
            }
        }

        public override void OnNewNight()
        {
            if (Main.bloodMoon && !expedition.condition1Met)
            {
                expedition.condition2Met = true;
            }
        }

        public override void OnAnyNPCDeath(NPC npc)
        {
            if(npc.townNPC && npc.friendly)
            {
                expedition.condition2Met = false;
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1;
        }
    }
}
