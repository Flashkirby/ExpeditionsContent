using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscPre
{
    class AnglerGuide : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "banana for the errand monkey";
            SetNPCHead(NPCID.Angler);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Complete 70 fishing quests";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.FishermansGuide, 1);
        }
        public override string Description(bool complete)
        {
            return "Minion! Hmm, very good work so far... I shall bequeath unto you some of my greatest wisdom! Bask in my amazing knowledge! Now go away and catch more fishies. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = player.anglerQuestsFinished >= 70;
            return cond1;
        }
    }
}
