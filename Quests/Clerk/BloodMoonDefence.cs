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
            expedition.ctgImportant = true; // Challenger indeed
            
            expedition.conditionDescription1 = "Survive a Blood Moon without losing anyone";
            expedition.conditionDescription2 = "Prevent any town deaths";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
            AddRewardItem(ItemID.MoneyTrough);
        }
        public override string Description(bool complete)
        {
            return "Just out of curiosity, how secure is this town against a blood moon? I mean if we can all survive an entire night, during a blood moon, I can definitely reward you for your preparation! ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return Main.bloodMoon || expedition.completed || cond1 || cond2;
        }

        public override void OnNewDay(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // If player prevented any town deaths, huzzah!
            if (!expedition.condition1Met && expedition.condition2Met)
            {
                expedition.condition1Met = true;
            }
        }

        public override void OnNewNight(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // On a blood moon, when you have 6 ore more NPCs, being quest
            if (Main.bloodMoon && !expedition.condition1Met)
            {
                int townieCount = 0;
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].active || Main.npc[i].type == NPCID.OldMan) continue;
                    if (Main.npc[i].townNPC && !Main.npc[i].homeless) townieCount++;
                }
                if (townieCount > 5) // 5 NPCs + Clerk. Not too hard to get.
                {
                    expedition.condition2Met = true;
                }
            }
        }
        
        public override void OnAnyNPCDeath(NPC npc, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Oh no, a friend died. Oh well, better luck next time.
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
