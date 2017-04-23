using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class EBMartianMadness : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "War of the Worlds";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 9;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Encounter a Martian Probe";
            //expedition.conditionDescription2 = "Face the martian invaders";
            //expedition.conditionDescription2 = "Defeat the martian invaders";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 3, 0, 0));
        }
        public override string Description(bool complete)
        {
            string message = "Mysterious alien probes can sometimes be found hovering high up near space. If you wish to encounter its owners, you should let the probe scan you. ";
            if (expedition.condition1Met)
            {
                message = "The martians bring with them all kinds of powerful electric and beam weapons. Take down out for their saucers, and perhaps you can salvage something useful. ";
            }
            return message;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescription2 = "Face the martian invaders"; }
            else
            { expedition.conditionDescription2 = ""; }

            if (cond2)
            { expedition.conditionDescription3 = "Defeat the martian invaders"; }
            else
            { expedition.conditionDescription3 = ""; }

            // Only appears until moonlord, or is done already
            if (!expedition.completed && NPC.downedMoonlord) return false;

            if (!cond1)
            {
                foreach(NPC npc in Main.npc)
                {
                    if(npc.type == NPCID.MartianProbe)
                    {
                        cond1 = true;
                        break;
                    }
                }
            }

            // Appears during an eclipse, or after other the other two moons are encountered at least once
            // ALSO Must have defeated golem
            return cond1 || (
                API.FindExpedition<EAPumpkinMoon>(mod).condition1Met &&
                API.FindExpedition<EAFrostMoon>(mod).condition1Met && 
                NPC.downedGolemBoss);
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond2) cond2 = Main.invasionType == InvasionID.MartianMadness;
            if (!cond3) cond3 = NPC.downedMartians;
            return cond1 && cond2 && cond3;
        }
    }
}
