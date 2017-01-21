using System;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Events;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscPre
{
    class DD2BigFan : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Etherian Fan";
            SetNPCHead(NPCID.DD2Bartender);
            expedition.difficulty = 5;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Equip a full set of etherian armor";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.DefenderMedal);
        }
        public override string Description(bool complete)
        {
            return "Your style reminds me of one of folks back home, what with you being kitted out like that. Check out Etheria sometime, I think you might like it there. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Must have bartender around
            if (NPC.FindFirstNPC(NPCID.DD2Bartender) == -1) return false;
            if (!cond1)
            {
                // Any armours equipped are from the dd2 set
                if (player.armor[0].type >= 3797 && player.armor[0].type <= 3882 &&
                    player.armor[1].type >= 3797 && player.armor[1].type <= 3882 &&
                    player.armor[2].type >= 3797 && player.armor[2].type <= 3882)
                { cond1 = true; }
            }
            return cond1;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1;
        }
    }
}