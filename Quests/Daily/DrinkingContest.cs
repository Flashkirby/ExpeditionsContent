using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Daily
{
    class DrinkingContest : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Drinking Contest";
            SetNPCHead(NPCID.Demolitionist);
            expedition.difficulty = 0;
            expedition.ctgExplore = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Talk with the demolitionist";
            expedition.conditionDescription2 = "Take a swig of ale";
            expedition.conditionDescription3 = "Down your 10 mugs of ale!";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1, false, "3rd Prize or Better");
            AddRewardItem(ItemID.DepthMeter, 1, false, "< 2.8 Seconds");
            AddRewardItem(ItemID.StickyDynamite, 1, false, "< 3 Seconds");
            AddRewardItem(ItemID.Dynamite, 1, false, "< 3.2 Seconds");
            AddRewardItem(ItemID.Bomb, 1, false, "Consolation");
        }
        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            float timeTaken = expedition.conditionCounted / 60f;
            List<Item> contestRewards = new List<Item>();

            contestRewards.Add(rewards[0]);
            if (timeTaken <= 2.8f)
            { contestRewards.Add(rewards[1]); }
            else if (timeTaken <= 3.0f)
            { contestRewards.Add(rewards[2]); }
            else if (timeTaken <= 3.2f)
            { contestRewards.Add(rewards[3]); }
            else
            { contestRewards.Add(rewards[4]); }

            // Replace Item Pool
            rewards.Clear();
            rewards.AddRange(contestRewards);
        }
        public override string Description(bool complete)
        {
            if(expedition.condition3Met)
            {
                float timeTaken = expedition.conditionCounted / 60f;
                float showTime = (int)(timeTaken * 100f) / 100f;
                if (timeTaken <= 2.8f)
                {
                    return "Wow, you really blew me away with that performance - "+ showTime + " seconds! Come on, we'll do this again some time, huh? Take this, as a token of our friendship! ";
                }
                else if (timeTaken <= 3.0f)
                {
                    return showTime + " seconds? That's real impressive! Let's drink to celebrate again sometime! But for now, take this sticky dynamite, I trust you know how to use it! ";
                }
                else if (timeTaken <= 3.2f)
                {
                    return "You took " + showTime + " seconds for 10 mugs? Not bad, not bad! I might even call ya an honorary dwarf! Hahaha, just kiddin' but here, take this stick o' dynamite, for impressing me so! ";
                }
                else if (timeTaken <= 4f)
                {
                    return "Hmm. You took " + showTime + " seconds to down all 10 mugs. Eh, well it's alright, if a little slow. Here, a consolation bomb, may it lighten your day! ";
                }
                else
                {
                    return "It took you " + showTime + " seconds to down a measly 10 mugs? You're slower than a wet fuse! Well, I appreciate ya efforts, so take this as consolation prize. ";
                }
            }
            return Main.player[Main.myPlayer].name + "! I challenge you to the drink! Down 10 mugs of ale as fast as you can! Think you have the stomach for it? Come and talk to me directly when you're ready, drinks are on the house! ";
        }

        public override bool IncludeAsDaily()
        {
            return NPC.FindFirstNPC(NPCID.Demolitionist) >= 0;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition);
        }

        int aleCount = 0;
        int aleTimeStart = 0;
        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1 &&
                player.talkNPC >= 0 &&
                player.talkNPC == NPC.FindFirstNPC(NPCID.Demolitionist))
            {
                cond1 = true;
                aleCount = 0;
                expedition.trackingActive = true;
                player.QuickSpawnItem(ItemID.Ale, 10);
            }

            if (!cond3)
            {
                int index = player.FindBuffIndex(BuffID.Tipsy);
                if (index >= 0)
                {
                    if (player.buffTime[index] == 7200) // The time set when used
                    {
                        aleCount++;
                        if (!cond2)
                        {
                            cond2 = true;
                            aleTimeStart = (int)Main.time;
                        }
                        if (aleCount >= 10)
                        {
                            expedition.conditionCounted = (int)Main.time - aleTimeStart;
                            cond3 = true;
                        }
                        if (aleCount > 10)
                        {
                            aleCount = 0;
                            cond2 = false;
                            cond3 = false;
                        }
                    }

                }
            }
            return cond1 && cond2 && cond3;
        }
    }
}
