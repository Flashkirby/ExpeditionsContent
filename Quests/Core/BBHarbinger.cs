using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class BBHarbinger : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Harbinger of Doom";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 1;
            expedition.ctgExplore = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Discover the biome";
            expedition.conditionDescription2 = "Smash a bad object";
        }
        public override void AddItemsOnLoad()
        {
            if (WorldGen.crimson)
            {
                expedition.name = "Harbinger of Blood";
                expedition.conditionDescription1 = "Discover the crimson";
                expedition.conditionDescription2 = "Smash a crimson heart";
            }
            else
            {
                expedition.name = "Harbinger of Decay";
                expedition.conditionDescription1 = "Discover the corruption";
                expedition.conditionDescription2 = "Smash a shadow orb";
            }

            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            if (Main.player[Main.myPlayer].statLifeMax < 200)
            {
                return String.Concat("The ",
                    WorldGen.crimson ? "crimson" : "corruption",
                    " is a dangerous place. You would do well to avoid it until you have better equipment - if you must cross it, do so quickly to avoid being overwhelmed. Be sure to bring rope and other climbing tools, as the stone resists pickaxes. ");
            } 
            else if(!NPC.downedBoss1)
            {
                return String.Concat("You will need to deal with the ",
                    WorldGen.crimson ? "crimson" : "corruption",
                    " soon, but I suggest you ready yourself for a different fight first. If you wish to follow your curiosity, remember not to break more than 2 ",
                    WorldGen.crimson ? "crimson hearts. " : "shadow orbs. ");
            }
            else if(Main.player[Main.myPlayer].statLifeMax < 300)
            {
                return String.Concat("Your next challenge lies within the ",
                    WorldGen.crimson ? "crimson" : "corruption",
                    ", but you will want at least fifteen hearts before venturing forth. If you wish to follow your curiosity, remember not to break more than 2 ",
                    WorldGen.crimson ? "crimson hearts. " : "shadow orbs. ");
            }

            return String.Concat("You should aim to break the ",
                WorldGen.crimson ? "crimson hearts" : "shadow orbs",
                " embedded within the ",
                WorldGen.crimson ? "ventricles" : "chasms",
                " of the ",
                WorldGen.crimson ? "crimson" : "corruption",
                ". You will need the dryad's purification powder, or explosives, to break through. Ropes and platforms will also be handy for getting around. ");
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) 
            {
                if (WorldGen.crimson)
                { cond1 = player.ZoneCrimson; }
                else
                { cond1 = player.ZoneCorrupt; }
            }
            // Appears once entering the biome or defeating the eye
            return cond1 || NPC.downedBoss1;
        }
        
        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond2) cond2 = WorldGen.shadowOrbSmashed;
            return cond1 && cond2;
        }
    }
}
