using System;
using Terraria;
using Terraria.ID;
using Expeditions144;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Quests.Clerk
{
	class AlbumOmnibus2 : ModExpedition
	{
		public override void SetDefaults()
		{
			expedition.name = "Snap! Cavern Compilation";
			SetNPCHead(ExpeditionC144.NPCIDClerk);
			expedition.difficulty = 2;
			expedition.ctgCollect = true;
			expedition.repeatable = true;

			expedition.conditionDescription1 = "Flying Fish";
			expedition.conditionDescription2 = "Blue Jellyfish";
			expedition.conditionCountedMax = 2;
			expedition.conditionDescriptionCountable = "Take photos of listed creatures";
		}
		public override void AddItemsOnLoad()
		{
			AddDeliverable(ModContent.ItemType<Items.Albums.AlbumPredators>());
			AddDeliverable(ModContent.ItemType<Items.Albums.AlbumCavern>());
			AddDeliverable(ModContent.ItemType<Items.Albums.AlbumUndead2>());
			AddDeliverable(ModContent.ItemType<Items.Albums.AlbumSnow>());
			AddDeliverable(ModContent.ItemType<Items.Albums.AlbumAntlion>());
			AddDeliverable(ModContent.ItemType<Items.Albums.AlbumBee>());
			AddDeliverable(ModContent.ItemType<Items.Albums.AlbumFlora>());

			AddRewardItem(API.ItemIDExpeditionCoupon, 1, true);
			AddRewardItem(ModContent.ItemType<Items.Albums.AlbumPredators2>());
		}
		public override string Description(bool complete)
		{
			return "You've built up quite the selection, so let's keep going! For the next big one let's go for all of the cool stuff you can find underground. Now there's a ton of things to find out there so this one may take a little longer than the last - but that's part of the fun, right? ";
		}
		#region Photo Bools
		public static PhotoManager flyingFish = new PhotoManager(NPCID.FlyingFish);
		public static PhotoManager blueJelly = new PhotoManager(NPCID.BlueJellyfish);
		#endregion

		public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
		{
			return (API.FindExpedition<AlbumOmnibus1>(mod).completed)
				|| expedition.conditionCounted > 0;
		}

		public override void CheckConditionCountable(Player player, ref int count, int max)
		{
			count = 0;
			if (flyingFish.checkValid()) count++;
			if (blueJelly.checkValid()) count++;
		}

		public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
		{
			cond1 = flyingFish.checkValid();
			cond2 = blueJelly.checkValid();
			return cond1 && cond2;
		}

		public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
		{
			flyingFish.consumePhoto();
			blueJelly.consumePhoto();

			// Only reward the coupon once!
			if (expedition.completed)
			{ rewards[0] = new Item(); }
		}
	}
}