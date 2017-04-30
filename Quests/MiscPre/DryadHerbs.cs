using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscPre
{
    class DryadHerbs : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Botanical Herbage";
            SetNPCHead(NPCID.Dryad);
            expedition.difficulty = 2;
            expedition.ctgExplore = true;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.DaybloomSeeds);
            AddDeliverable(ItemID.MoonglowSeeds);
            AddDeliverable(ItemID.WaterleafSeeds);
            AddDeliverable(ItemID.ShiverthornSeeds);

            AddRewardItem(ItemID.HerbBag, 2);
            AddRewardItem(ItemID.DayBloomPlanterBox, 8);
        }
        public override string Description(bool complete)
        {
            if (complete) return "I'll let you in on a little secret - there's a special staff found in the underground jungle that can be used to harvest seeds from herbs as soon as they are mature - even if they are not in full bloom! It makes herbs like blinkroot much nicer to grow and harvest. ";
            return "It is important to wait for herbs to bloom before harvesting them. However, sometimes patience isn't always the answer; some herbs will only bloom under certain conditions. For instance, Waterleaf will only bloom in the rain, and Fireblossom will bloom at sunset. Show me some understanding of this, and I will share with you some of my work. ";
        }
    }
}
