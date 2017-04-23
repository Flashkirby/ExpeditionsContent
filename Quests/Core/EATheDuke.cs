using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class EATheDuke : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Duking It Out";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 8;
            expedition.ctgCollect = true;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Summon the Drowner of Souls and Quencher of Life Itself";
            //expedition.conditionDescription2 = "Defeat Duke Fishron, Leader of the Aquarian Horde";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 5, 0, 0));
            AddRewardItem(ItemID.GreaterHealingPotion, 5);
            AddRewardItem(ItemID.GreaterManaPotion, 5);
            AddRewardItem(ItemID.IronskinPotion, 1);
            AddRewardItem(ItemID.RegenerationPotion, 1);
            AddRewardItem(ItemID.LifeforcePotion, 1);
        }
        public override string Description(bool complete)
        {
            string message = "Bug nets can be used to catch relusive truffle worms, found deep underground in mushroom biomes. Using one as bait in the ocean will attract quite a challenging foe. ";
            if(expedition.condition1Met)
            {
                message = "Truffle worms from the underground mushroom biomes make great bait for Duke Fishron. You will need speed and strategy to defeat him however, as simply running away from the ocean will only make him go beserk. ";
            }
            return message;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescription2 = "Defeat Duke Fishron, Leader of the Aquarian Horde"; }
            else
            { expedition.conditionDescription2 = ""; }

            // Appears after golem, or if the player has a truffle or fought duke
            return NPC.downedGolemBoss || cond3 || cond1;
        }

        public override void OnPickupItem(Item item, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond3 = item.type == ItemID.TruffleWorm;
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                cond1 = npc.type == NPCID.DukeFishron;
            }
        }

        public override void OnAnyNPCDeath(NPC npc, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond2)
            {
                cond2 = npc.type == NPCID.DukeFishron;
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1 && cond2;
        }
    }
}
