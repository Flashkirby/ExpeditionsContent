using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class BAEvilEye : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "An Evil Presence";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 1;
            expedition.ctgSlay = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Face against the demonic watcher";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 1, 0, 0));
        }
        public override string Description(bool complete)
        {
            int townies = 0;
            for (int i = 0; i < 200; i++)
            {
                if (!Main.npc[i].active || Main.npc[i].type == NPCID.OldMan) continue;
                if (Main.npc[i].townNPC && !Main.npc[i].homeless) townies++;
            }

            if(Main.player[Main.myPlayer].statLifeMax < 200)
            {
                return "This won't do at all. I insist that you have at least 10 hearts before trying anything reckless. ";
            }
            else if (townies < 3)
            {
                return "You will soon face your first major battle. Have you tried expanding your town a bit more? Many people offer invaluable services, such as the nurse's healing.";
            }
            else if(Main.player[Main.myPlayer].statDefense < 10)
            {
                return "You will soon face your first major battle. I strongly suggest you invest in decent armor and a ranged weapon, like a boomerang or bow. ";
            }
            return "I think you're ready major battle. A powerful monster will awaken every few nights, until it is slain. If you wish to summon it yourself, you'll need to craft together 6 lenses at a " + (WorldGen.crimson ? "crimson" : "demon") +" altar. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                expedition.conditionDescription2 = "";
            }
            else
            { expedition.conditionDescription2 = "Defeat the Eye of Cthulu"; }

            // Only appears until hardmode, or is done already
            if (!expedition.completed && Main.hardMode) return false;

            return player.statLifeMax >= 200 || cond1;
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit)
        {
            if (!expedition.condition1Met)
                expedition.condition1Met = npc.type == NPCID.EyeofCthulhu;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1 && !cond2) cond2 = NPC.downedBoss1;
            return cond1 && cond2;
        }
    }
}
