using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Daily
{
    class BossEoW : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Toothache";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 2;
            expedition.ctgSlay = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Summon the Eater of Worlds";
            expedition.conditionDescription2 = "Keep the number of Eater heads at 3 or less";
            expedition.conditionDescription3 = "Defeat the Eater of Worlds";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 2);
        }
        public override string Description(bool complete)
        {
            expedition.conditionDescription2 = "Do not split more than " + headNumber() + " heads at once";
            return "Looking for a boss challenge? Try defeating the Eater of Worlds, but without letting it split too many times. That means no more than " + headNumber() + " heads, or you will fail the challenge. You can summon the Eater of Worlds by crafting rotten chunks together at a demon altar. ";
        }
        private static int headNumber()
        {
            if (Main.expertMode) return 7;
            return 3;
        }

        public override bool IncludeAsDaily()
        {
            return NPC.downedBoss2 && !WorldGen.crimson;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition);
        }

        public override void OnAnyNPCDeath(NPC npc, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (npc.type == NPCID.EaterofWorldsHead ||
                npc.type == NPCID.EaterofWorldsBody ||
                npc.type == NPCID.EaterofWorldsTail)
            {
                // Last segment to die is the only boss
                if (npc.boss) cond3 = true;
            }
        }

        int prevCount = 0;
        bool resetFrame = false; // Wait a frame when number changes, to give the game time to modify heads
        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Boss summoned
            if (NPC.FindFirstNPC(NPCID.EaterofWorldsHead) >= 0)
            {
                if (!cond1)
                {
                    cond1 = true;
                    cond2 = true;
                }
            }
            else
            {
                if (cond3 && !cond2) // Finsihed boss but failed the challenge?
                {
                    bool saveTracked = expedition.trackingActive;
                    expedition.ResetProgress();
                    expedition.trackingActive = saveTracked;
                    resetFrame = false;
                    prevCount = 0;
                }
            }

            if (cond1)
            {
                #region Head and Length Counting
                // Head and length counting
                if (cond2 && !cond3)
                {
                    int headCount = 0;
                    for (int i = 0; i < 200; i++)
                    {
                        if (!Main.npc[i].active) continue;
                        if (Main.npc[i].type == NPCID.EaterofWorldsHead)
                        {
                            headCount++;
                        }
                    }
                    if (expedition.conditionCounted < headCount)
                    {
                        if (!resetFrame)
                        {
                            resetFrame = true;
                        }
                        else
                        {
                            resetFrame = false;
                            expedition.conditionCounted = headCount;
                        }
                    }
                    headCount = expedition.conditionCounted;
                    // FAIL!
                    if (headCount > headNumber()) 
                    {
                        cond2 = false;
                    }

                    // Tracking
                    if (expedition.trackingActive)
                    {
                        if (prevCount != headCount && headCount > 1)
                        {
                            string name = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
                            if (name == "") name = "Guide";
                            string lastText = "";
                            if (headCount == headNumber()) lastText = "Any more and you will be unable to complete the challenge! ";
                            if (!cond2)
                            {
                                lastText = "You will have to retry this challenge after defeating the boss. ";
                            }
                            Main.NewText(String.Concat(
                                "<", name, "> The Eater of Worlds is now split into ", headCount, " individuals. ",
                                lastText
                                ));
                        }
                    }
                    prevCount = headCount;
                }
                #endregion
            }

            return cond1 && cond2 && cond3;
        }
    }
}
