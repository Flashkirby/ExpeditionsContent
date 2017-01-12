using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ABMapping : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Finding Your Way";
            expedition.difficulty = 0;
            expedition.ctgExplore = true;
        }
        public override void AddItemsOnLoad()
        {
        }
        public override string Description(bool complete)
        {
            return "Your map will automatically record the surroundings as you explore. Try exploring this starting area to get a feel for it. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.FindExpedition<AAWelcomeQuest>(mod).completed;
        }


    }
}
