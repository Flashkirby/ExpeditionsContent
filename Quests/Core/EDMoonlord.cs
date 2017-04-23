using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class EDMoonlord : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "???";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 10;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Defeat the Moonlord";
            //expedition.conditionDescription2 = "Face the moonlord";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 10, 0, 0));
        }
        public override string Description(bool complete)
        {
            if(!expedition.condition2Met)
            {
                return "Something's not right...";
            }
            return "The Moonlord is a powerful adversary. Be prepared for wide, sweeping lasers and beams flying your way. Its bite is worse than its bark, preventing you from healing, as well as stealing life.";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond2)
            {
                expedition.name = "\n\n\n...?";
                expedition.conditionDescription1 = "Prepare yourself";
            }
            else
            {
                expedition.name = "To Dethrone A Lord";
                expedition.conditionDescription1 = "Defeat the Moonlord";
            }
            return NPC.downedTowers;
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if(!cond2)
            {
                cond2 =
                    npc.type == NPCID.MoonLordCore ||
                    npc.type == NPCID.MoonLordFreeEye ||
                    npc.type == NPCID.MoonLordHand ||
                    npc.type == NPCID.MoonLordHead ||
                    npc.type == NPCID.MoonLordLeechBlob;
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1;
        }
    }
}
