using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class BCBoss2 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Evil Boss Round 2";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 2;
            expedition.ctgSlay = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Face the evil creature";
            expedition.conditionDescriptionCountable = "Smash 3 evil objects";
            expedition.conditionCountedMax = 3;
            expedition.conditionCountedTrackHalfCompleted = true;
        }
        public override void AddItemsOnLoad()
        {
            if (WorldGen.crimson)
            {
                expedition.name = "Crimson Heart and Mind";
                expedition.conditionDescription1 = "Face the crimson maw";
                expedition.conditionDescriptionCountable = "Smash 3 crimson hearts";
            }
            else
            {
                expedition.name = "The Worm that Turned";
                expedition.conditionDescription1 = "Face the world eater";
                expedition.conditionDescriptionCountable = "Smash 3 shadow orbs";
            }

            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            string initial = String.Concat("Every ",
                WorldGen.crimson ? "crimson heart" : "shadow orb",
                " broken brings you a step closer to drawing the ire of a dangerous monster. ");

            if (Main.player[Main.myPlayer].statLifeMax < 300)
            {
                return String.Concat(initial,
                    "I would strongly recommend at least fifteen hearts before fighting the beast. ");
            }

            return String.Concat(initial,
                "You will need its ",
                WorldGen.crimson ? "tissue" : "scales",
                "  to craft a powerful set of armour and a pickaxe with at least 65% power, strong enough to mine both obsidian and ",
                WorldGen.crimson ? "crimstone. " : "ebonstone. ");
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) 
            {
                expedition.conditionDescription2 = "";
                if (WorldGen.crimson)
                { cond1 = API.LastHitNPC.type == NPCID.BrainofCthulhu; }
                else
                {
                    cond1 = (
                        API.LastHitNPC.type == NPCID.EaterofWorldsHead ||
                        API.LastHitNPC.type == NPCID.EaterofWorldsBody ||
                        API.LastHitNPC.type == NPCID.EaterofWorldsTail);
                }
            }
            else
            {
                if (WorldGen.crimson)
                { expedition.conditionDescription2 = "Defeat the Brain of Cthulu"; }
                else
                { expedition.conditionDescription2 = "Defeat the Eater of Worlds"; }
            }

            // Only appears until hardmode, or is done already
            if (!expedition.completed && Main.hardMode) return false;

            return cond1 || NPC.downedBoss1 || API.FindExpedition<BBHarbinger>(mod).completed;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            if (count < max) count = WorldGen.shadowOrbCount;
            if(WorldGen.shadowOrbCount == 0 && WorldGen.shadowOrbSmashed)
            {
                count = max;
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1 && !cond2) cond2 = NPC.downedBoss2;
            return cond1 && cond2;
        }
    }
}
