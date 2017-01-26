using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class SecretSummon : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Secret Summoner";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgExplore = true;

            expedition.conditionDescription2 = "Obtain a 'summon weapon'";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
        }
        public override string Description(bool complete)
        {
            if (complete) return "Get this, I've submitted a report on summon weapons, and you'll never guess what just arrived in my inventory - a shiny badge! Apparently, it increases the power of your faithful minions. Neat! ";
            return "Guess what? I've heard rumors of a strange class of weapons that don't actually attack directly, but instead summon allies to your aid. I have no record on these, it's up to you to find them! ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!expedition.completed && player.statLifeMax >= 200) return false;
            if (!cond1)
            {
                cond1 = NPC.FindFirstNPC(NPCID.TravellingMerchant) >= 0
                    && API.TimeAfternoon;
            }
            return cond1;
        }

        public override void OnPickupItem(Item item, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if(expedition.condition1Met)
            {
                if (!expedition.condition2Met) expedition.condition2Met = item.summon;
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1 && cond2;
        }

        public override void PostCompleteExpedition()
        {
            WayfarerWeapons.ShowItemUnlock("Loyalty Badge", expedition.difficulty);
        }
    }
}
