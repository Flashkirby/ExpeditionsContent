using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Daily
{
    class BossBoC : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Brainpower";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 2;
            expedition.ctgSlay = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Summon the Brain of Cthulu";
            expedition.conditionDescription2 = "Destroy all Creepers within 60 seconds of each other";
            expedition.conditionDescription3 = "Defeat the Brain of Cthulu";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 2);
        }
        public override string Description(bool complete)
        {
            return "Looking for a boss challenge? Defeat all of the Brain of Cthulu's Creepers within 60 seconds of each other. The timer starts after the first seeker is slain, and stops when all are defeated. You can summon the Brain of Cthulu by crafting vertebrae together at a crimson altar. ";
        }

        public override bool IncludeAsDaily()
        {
            return NPC.downedBoss2 && WorldGen.crimson;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition);
        }

        public override void OnAnyNPCDeath(NPC npc, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            {
                if (npc.type == NPCID.BrainofCthulhu)
                {
                    cond3 = true;
                }

                if (npc.type == NPCID.Creeper)
                {
                    if(expedition.conditionCounted == 0)
                    {
                        expedition.conditionCounted = (int)Main.time;
                        if (expedition.trackingActive)
                        {
                            string name = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
                            if (name == "") name = "Guide";
                            Main.NewText(String.Concat(
                                "<", name, "> The first creeper is down. You have 60 seconds to defeat the rest of them, starting now! "
                                ));
                        }
                    }
                }
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                cond1 = NPC.FindFirstNPC(NPCID.BrainofCthulhu) >= 0;
            }
            if (!cond2)
            {
                #region Creeper Counting
                if (expedition.conditionCounted != 0) // Timer is set!
                {
                    int count = 0;
                    for (int i = 0; i < 200; i++)
                    {
                        if (!Main.npc[i].active) continue;
                        if (Main.npc[i].type == NPCID.Creeper) count++;
                    }
                    if (Main.time < expedition.conditionCounted + 3600)
                    {
                        if (count == 0 && NPC.FindFirstNPC(NPCID.BrainofCthulhu) >= 0)
                        {
                            cond2 = true; 
                            // Completed if all defeated AND boss still around 
                            // Prevent save/exit cheesing
                        }
                        else if ((int)Main.time == expedition.conditionCounted + 3000)
                        {
                            string name = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
                            if (name == "") name = "Guide";
                            Main.NewText(String.Concat(
                                "<", name, "> 10 seconds left to defeat the remaining Creepers! "
                                ));
                        }
                    }
                    else
                    {
                        // FAIL!
                        if (expedition.trackingActive)
                        {
                            string name = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
                            if (name == "") name = "Guide";
                            Main.NewText(String.Concat(
                                "<", name, "> 60 seconds are up! You can no longer compelte this challenge. "
                                ));
                            expedition.trackingActive = false;
                        }
                    }
                }
                #endregion
            }

            return base.CheckConditions(player, ref cond1, ref cond2, ref cond3, condCount);
        }
    }
}
