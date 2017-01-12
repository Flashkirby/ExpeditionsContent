using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class T0WelcomeQuest : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Welcome To Terraria";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.Torch);
        }
        public override string Description(bool complete)
        {
            return "Need some direction? The first thing you'll need to do is start collecting wood, and building yourself som shelter. Slimes drop gel when killed, which can be combined with wood to make torches. ";
        }


    }
}
