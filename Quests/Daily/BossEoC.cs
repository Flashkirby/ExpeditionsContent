using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Daily
{
    class BossEoC : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Eyesore";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 2;
            expedition.ctgSlay = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Summon the Eye of Cthulu";
            expedition.conditionDescription2 = "Keep the number of Servants of Cthulu under 6";
            expedition.conditionDescription3 = "Defeat the Eye of Cthulu before midnight";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 2);
        }
        public override string Description(bool complete)
        {
            return "I have a challenge for you. Defeat the Eye of Cthulu before midnight. Sound too easy? Well, in addition, there can't be more than 5 Servants of Cthulu flying about at any one time. Make sure to have a clock or watch handy, so you can keep in eye on the time. ";
        }

        public override bool IncludeAsDaily()
        {
            return NPC.downedBoss1;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition) || true;
        }

        public override void OnAnyNPCDeath(NPC npc, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if(cond1)
            {
                if(npc.type == NPCID.EyeofCthulhu && API.TimeNightPreMid)
                {
                    cond3 = true;
                }
            }
        }

        public override void OnNewNight(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond3)
            {
                cond1 = false;
                cond2 = false;
            }
        }

        int prevCount = 0;
        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Boss summoned
            if (NPC.FindFirstNPC(NPCID.EyeofCthulhu) >= 0)
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
                }
            }

            if (cond1)
            {
                #region Servant Counting
                // Servant of Cthulu spawn counting
                if (cond2)
                {
                    int count = 0;
                    for (int i = 0; i < 200; i++)
                    {
                        if (!Main.npc[i].active) continue;
                        if (Main.npc[i].type == NPCID.ServantofCthulhu) count++;
                    }
                    // FAIL!
                    if (count > 5)
                    {
                        cond2 = false;
                    }

                    // Ignore tracking after failure
                    if (!cond2 && count > 6)
                    {
                        prevCount = count;
                    }

                    // Tracking
                    if (expedition.trackingActive)
                    {
                        if(prevCount != count && count >= 3)
                        {
                            string name = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
                            if (name == "") name = "Guide";
                            string lastText = "";
                            if (count == 5) lastText = "Slay them quickly! ";
                            if (!cond2) 
                            {
                                lastText = "You will have to retry this challenge after defeating the boss. ";
                            }
                            Main.NewText(String.Concat(
                                "<", name, "> There are ", count, " Servants of Cthulu. ", 
                                lastText
                                ));
                        }
                    }

                    prevCount = count;
                }
                #endregion

                #region Midnight Check
                if (!cond3 && cond2)
                {
                    if (expedition.trackingActive)
                    {
                        string name = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
                        if (name == "") name = "Guide";


                        if (Main.time == (int)(TimeChecker.RawMidnightTime * 0.5f))
                        {
                            Main.NewText(
                                String.Concat("<", name, "> You're half way to midnight. "));
                        }
                        else if (Main.time == (int)(TimeChecker.RawMidnightTime * 0.75f))
                        {
                            Main.NewText(
                                String.Concat("<", name, "> You must defeat the Eye of Cthulu soon to complete the challenge! "));
                        }
                        else if (Main.time == TimeChecker.RawMidnightTime)
                        {
                            Main.NewText(
                                String.Concat("<", name, "> It's now past midnight. You can no longer complete this challenge. "));
                        }
                    }
                }
                #endregion
            }

            return cond1 && cond2 && cond3;
        }
    }
}
