using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ACHouseHome : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Making a House a Home";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 0;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Use a bed to set a spawn point";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 0, 15, 0));
        }
        public override string Description(bool complete)
        {
            return "When you have a base, it's important to make sure that you can always return there if you run into trouble. To move your spawn point, you will need a bed. Beds are crafted out of wood and silk at a sawmill, and the silk itself is crafted at a loom. Both require wood, and the sawmill also requires iron and chains. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.FindExpedition<ABStartTown>(mod).completed;
        }
        
        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCont)
        {
            if(!cond1)
            {
                if (Main.time % 60 == 0)
                {
                    // Code from FindSpawn() in player.cs
                    for (int i = 0; i < 200; i++)
                    {
                        if (player.spN[i] == null) 
                        {
                            // Reached the end of array
                            cond1 = false;
                            break;
                        }
                        if (player.spN[i] == Main.worldName && player.spI[i] == Main.worldID)
                        {
                            cond1 = true;
                            break;
                        }
                    }
                }
            }
            return cond1;
        }
    }
}
