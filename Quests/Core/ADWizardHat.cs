using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ADWizardHat : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Arcane Attire";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.partyShare = true;

            expedition.conditionDescription1 = "Obtain a Wizard Hat";
            expedition.conditionDescription2 = "Put on your robe and wizard hat";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.Diamond, 1);
        }
        public override string Description(bool complete)
        {
            if(!expedition.condition1Met)
            {
                return "An elusive wizard of times long past still wanders the caverns as a skeleton. Should you put it to rest, you may inherit some of its strength. Elusive that it may be, donning a magical robe may cause it to seek you out. ";
            }
            return "The power of an ancient wizard flows through that hat, but its full potential only manifest itself if you wear the right cloth to match. Perhaps you should try making something at a loom - a fine silk robe would do nicely. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until hardmode, or is done already
            if (!expedition.completed && Main.hardMode) return false;

            return API.FindExpedition<ACUnderground>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = API.InInventory[ItemID.WizardHat];
            if (!cond2)
            {
                if (player.head == 14 && ((player.body >= 58 && player.body <= 63) || player.body == 167))
                {
                    cond2 = true;
                }
            }
            return cond1 && cond2;
        }
    }
}
