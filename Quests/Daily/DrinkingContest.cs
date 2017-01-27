using System;
using Terraria;
using Terraria.ID;
using Expeditions;

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
            AddRewardItem(API.ItemIDExpeditionCoupon, 1, "3rd Prize or Better");
            AddRewardItem(ItemID.DepthMeter, 1, "< 2.8 Seconds");
            AddRewardItem(ItemID.StickyDynamite, 1, "< 3 Seconds");
            AddRewardItem(ItemID.Dynamite, 1, "< 3.2 Seconds");
            AddRewardItem(ItemID.Bomb, 1, "Consolation");
        }
        public override string Description(bool complete)
        {
            if(expedition.condition3Met)
            {
                float timeTaken = (expedition.conditionCounted * 10f) / 600;
                if (timeTaken <= 2.8f)
                {
                    return "Wow, you really blew me away with that performance - "+ timeTaken + " seconds! Come on, we'll do this again some time, huh? Take this, as a token of our friendship! ";
                }
                else if (timeTaken <= 3.0f)
                {
                    return timeTaken + " seconds? That's real impressive! Let's drink to celebrate again sometime! But for now, take this stikcy dynamite, I trust you know how to use it! ";
                }
                else if (timeTaken <= 3.2f)
                {
                    return "You took " + timeTaken + " seconds for 10 mugs? Not bad, not bad! I might even call ya an honourary dwarf! Hahaha, just kiddin' but here, take this stick o' dynamite, for impressing me so! ";
                }
                else if (timeTaken <= 4f)
                {
                    return "Hmm. You took " + timeTaken + " seconds to down all 10 mugs. Eh, well it's alright, if a little slow. Here, a consolation bomb, may it lighten your day! ";
                }
                else
                {
                    return "It took you " + timeTaken + " seconds to down a measly 10 mugs? You're slower than a wet fuse! Well, I appreciate ya efforts, so take this as consolation prize. ";
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
