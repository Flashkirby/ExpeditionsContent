using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CCPirates : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Swashbuckling Sailors";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 5;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Face the pirate crew";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 3, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "There's a band of pirates in flying ships roaming these lands looking for a treasure map. The last I heard, it was hidden on a beach somewhere, but any creature would have have picked it up by now. If you have one, they will certainly come and try to pillage it from you. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescription2 = "Defeat the pirates"; }
            else
            { expedition.conditionDescription2 = ""; }

            if (!cond3)
            {
                foreach (Item i in player.inventory) if (i.type == ItemID.PirateMap) cond3 = true;
            }
            return WorldGen.altarCount > 0 || (cond1 || cond3 || Main.invasionType == InvasionID.PirateInvasion);
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!expedition.condition1Met)
            {
                expedition.condition1Met =
                    npc.type == NPCID.PirateCaptain ||
                    npc.type == NPCID.PirateCrossbower ||
                    npc.type == NPCID.PirateDeadeye ||
                    npc.type == NPCID.PirateCorsair ||
                    npc.type == NPCID.PirateDeckhand ||
                    npc.type == NPCID.PirateShip ||
                    npc.type == NPCID.PirateShipCannon;
            }
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            if (Main.invasionType == InvasionID.PirateInvasion)
            {
                expedition.conditionCounted = Main.invasionProgress;
                expedition.conditionCountedMax = Main.invasionProgressMax;
                expedition.conditionDescriptionCountable = "Slay pirates";
            }
            else
            {
                expedition.conditionCounted = 0;
                expedition.conditionCountedMax = 0;
                expedition.conditionDescriptionCountable = "";
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1 && !cond2)
            {
                cond2 = NPC.downedPirates;
            }
            return cond1 && cond2;
        }
    }
}
