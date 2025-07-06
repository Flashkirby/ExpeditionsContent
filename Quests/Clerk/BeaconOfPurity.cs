using System;
using Terraria;
using Terraria.ID;
using Expeditions144;

namespace ExpeditionsContent144.Quests.Clerk
{
	class BeaconOfPurity : ModExpedition
	{
		public override void SetDefaults()
		{
			expedition.name = "Beacon of Purity";
			SetNPCHead(ExpeditionC144.NPCIDClerk);
			expedition.difficulty = 0;
			expedition.ctgExplore = true;

			expedition.conditionDescription1 = "Discover a Living Tree";
		}
		public override void AddItemsOnLoad()
		{
			AddRewardItem(ItemID.WandofSparking);
			AddRewardItem(ItemID.LivingWoodChest);
		}
		public override string Description(bool complete)
		{
			return "You found a what? A giant tree? That's awesome! I can tell you know I didn't expect something like that when I came here, no siree. ";
		}

		public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
		{
			if (!cond1)
			{
				cond1 = (Port_TileCounts.livingWood > 128);
			}
			return cond1;
		}

		public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
		{
			return cond1;
		}
	}
}
