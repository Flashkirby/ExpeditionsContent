using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscPre
{
    class WizardMap : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Enchanted Grotto";
            SetNPCHead(NPCID.Wizard, true);
            expedition.difficulty = 4;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.Prismite);
            AddDeliverable(ItemID.Blinkroot);
            AddDeliverable(ItemID.HoneyBlock);

            AddRewardItem(mod.ItemType<Items.ShrineMap>(), 1);
        }
        public override string Description(bool complete)
        {
            if(complete)
            {
                return "Ah, I do believe I remember where it was. Or they were. Behold, I present you this magically enchanted map - it may or may not eat you! Oh, you'd rather have this regular old magic map? Ok. ";
            }
            return "I came across this wonderful shrine once, but I can't remember where. It had a magical sword in it... maybe? Or was it a rock? You know, a memory boost might do the trick! Fetch me these ingredients, perhaps I may recall something? ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return Main.hardMode && NPC.FindFirstNPC(NPCID.Wizard) >= 0;
        }
    }
}
