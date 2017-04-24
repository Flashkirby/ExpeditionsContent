using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class IntoOrbit : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Final Frontier";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 3;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Reach the top of the sky";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.WeatherRadio, 1);
        }
        public override string Description(bool complete)
        {
            return "Would you mind participating in an experiment? Don't worry, it's perfectly safe. All you need to do is go up. To space. As far as possible. Please? It's for science and knowledge! Also, useful for telling when to hang clothes out to dry, if it works. ";
        }
        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.FindExpedition<SkysTheLimit>(mod).completed;
        }
        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                if (player.position.Y <= Main.topWorld + 640f + 16f + 1f)
                {
                    cond1 = true;
                }
            }
            return cond1;
        }
    }
}
