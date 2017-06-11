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
            expedition.conditionDescription2 = "Destroy all Creepers within " + timeLimit() + " seconds of each other";
            return "Looking for a boss challenge? Defeat all of the Brain of Cthulu's Creepers within " + timeLimit() + " seconds of each other. The timer starts after the first seeker is slain, and stops when all are defeated. You can summon the Brain of Cthulu by crafting vertebrae together at a crimson altar. ";
        }
        private static int timeLimit()
        {
            if (Main.expertMode) return 120;
            return 60;
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
                                "<", name, "> The first creeper is down. You have " + timeLimit() + " seconds to defeat the rest of them, starting now! "
                                ));
                        }
                    }
                }
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Boss summoned
            if (NPC.FindFirstNPC(NPCID.BrainofCthulhu) >= 0)
            {
                cond1 = true;
            }
            else
            {
                if (cond3 && !cond2) // Finsihed boss but failed the challenge?
                {
                    bool saveTracked = expedition.trackingActive;
                    expedition.ResetProgress();
                    expedition.trackingActive = saveTracked;
                    expedition.conditionCounted = 0;
                }
            }

            if (cond1 && !cond2)
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
                    if (Main.time < expedition.conditionCounted + (timeLimit() * 60))
                    {
                        if (count == 0 && NPC.FindFirstNPC(NPCID.BrainofCthulhu) >= 0)
                        {
                            cond2 = true;
                            // Completed if all defeated AND boss still around 
                            // Prevent save/exit cheesing
                        }

                        string name = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
                        if (name == "") name = "Guide";
                        switch ((int)Main.time - expedition.conditionCounted)
                        {
                            case 60 * 30:
                                Main.NewText(String.Concat(
                                    "<", name, "> 30 seconds left to defeat the remaining Creepers! "));
                                break;
                            case 60 * 50:
                                Main.NewText(String.Concat(
                                    "<", name, "> 10 seconds left to defeat the remaining Creepers! "));
                                break;
                            case 60 * 55:
                                Main.NewText(String.Concat(
                                    "<", name, "> 5 seconds left to defeat the remaining Creepers! "));
                                break;
                            case 60 * 56:
                                Main.NewText(String.Concat(
                                    "<", name, "> 4 seconds left to defeat the remaining Creepers! "));
                                break;
                            case 60 * 57:
                                Main.NewText(String.Concat(
                                    "<", name, "> 3 seconds left to defeat the remaining Creepers! "));
                                break;
                            case 60 * 58:
                                Main.NewText(String.Concat(
                                    "<", name, "> 2 seconds left to defeat the remaining Creepers! "));
                                break;
                            case 60 * 59:
                                Main.NewText(String.Concat(
                                    "<", name, "> 1 second left to defeat the remaining Creepers! "));
                                break;
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
                                "<", name, "> 60 seconds are up! You will have to retry this challenge after defeating the boss. "
                                ));
                        }
                    }
                }
                #endregion
            }

            return cond1 && cond2 && cond3;
        }
    }
}
