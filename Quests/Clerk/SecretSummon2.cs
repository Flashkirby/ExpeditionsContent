using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class SecretSummon2 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Beast Tamer";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverableAnyOf(new int[] { ItemID.RubyStaff, ItemID.DiamondStaff }, 1);
            AddDeliverable(ItemID.Terrarium, 3);
            //Fox
            AddDeliverable(ItemID.BlueBerries, 1);
            AddDeliverable(ItemID.Bunny, 1);
            //Chicken
            AddDeliverable(ItemID.GrassSeeds, 2);
            AddDeliverable(ItemID.Worm, 2);
            //Cat
            AddDeliverable(ItemID.Mouse, 1);
            AddDeliverableAnyOf(new int[] { ItemID.Tuna, ItemID.Trout }, 1);

            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
        }
        public override string Description(bool complete)
        {
            if (complete) return "Haha! Thanks to our combined effort, I have made a summon... thing? Well, I think, anyway. Check it out in my shop! It probably works! ";
            if (API.FindExpedition<SecretSummon>(mod).completed)
            {
                return "You remember that summoning thing you showed me? Well I've just had the craziest idea, but I'll need a few things. Don't ask, it'll make sense. Trust me! ";
            }
            else
            {
                return "No luck finding a summon weapon, huh? Man, I'd really like to see one though. Hey, I've got a great idea, but I'll need a few things. Don't ask, it'll make sense. Trust me! ";
            }
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!expedition.completed && player.statLifeMax <= 200) return false;
            // Appear after EoC, in evening
            if (!cond1)
            {
                cond1 = NPC.downedBoss1
                    && API.TimeEvening;
            }
            return cond1;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1;
        }

        public override void PostCompleteExpedition()
        {
            WayfarerWeapons.ShowItemUnlock("Wayfarer's Bell", expedition.difficulty);
        }
    }
}
