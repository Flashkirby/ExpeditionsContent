using System;
using Terraria;
using Terraria.ID;
using Expeditions144;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Quests.Clerk
{
	class AlbumSlimes : ModExpedition
	{
		public override void SetDefaults()
		{
			expedition.name = "Snap! Slime Tour";
			SetNPCHead(ExpeditionC144.NPCIDClerk);
			expedition.difficulty = 0;
			expedition.ctgCollect = true;
			expedition.ctgExplore = true;
			expedition.repeatable = true;

			expedition.conditionDescription1 = "Umbrella Slime";
			expedition.conditionDescription2 = "Ice Slime";
			expedition.conditionDescription3 = "Sand Slime";
			expedition.conditionCountedMax = 3;
			expedition.conditionDescriptionCountable = "Take photos of listed creatures";
		}
		public override void AddItemsOnLoad()
		{
			AddRewardItem(API.ItemIDExpeditionCoupon, 1, true);
			AddRewardItem(ModContent.ItemType<Items.Albums.AlbumSlimes>());
		}
		public override string Description(bool complete)
		{
			return "For some reason, the camera can't seem to capture the color of normal slimes, so out next best bet is to capture special slimes found throughout " + Main.worldName + "! These ones should be simple enough, right? ";
		}
		#region Photo Bools
		public static bool Umbrella
		{ get { return PhotoManager.PhotoOfNPC[NPCID.UmbrellaSlime]; } }
		public static bool Ice
		{ get { return PhotoManager.PhotoOfNPC[NPCID.IceSlime] || PhotoManager.PhotoOfNPC[NPCID.SpikedIceSlime]; } }
		public static bool Sand
		{ get { return PhotoManager.PhotoOfNPC[NPCID.SandSlime]; } }
		#endregion

		public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
		{
			return PlayerExplorer.HoldingCamera(mod)
				|| expedition.conditionCounted > 0;
		}

		public override void CheckConditionCountable(Player player, ref int count, int max)
		{
			count = 0;
			if (Umbrella) count++;
			if (Ice) count++;
			if (Sand) count++;
		}

		public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
		{
			cond1 = Umbrella;
			cond2 = Ice;
			cond3 = Sand;
			return cond1 && cond2 && cond3;
		}

		public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
		{
			PhotoManager.ConsumePhoto(NPCID.UmbrellaSlime);

			if (!PhotoManager.ConsumePhoto(NPCID.IceSlime))
			{ PhotoManager.ConsumePhoto(NPCID.SpikedIceSlime); }

			PhotoManager.ConsumePhoto(NPCID.SandSlime);

			// Only reward the coupon once!
			if (expedition.completed)
			{ rewards[0] = new Item(); }
		}
	}
}
