using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Moonstone
{
	[AutoloadEquip(EquipType.Body)]
	public class MoonstoneARMTunic : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Yutu Tunic");
            Tooltip.SetDefault("Provides 1 second of immunity to lava\n"
                + "10% increased mining speed");*/
			ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;

		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.defense = 6;
			Item.rare = 3;
			Item.value = Item.sellPrice(0, 3, 60, 0);
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Moonstone>(), 15);
			recipe.AddIngredient(ItemID.Silk, 6);
			recipe.AddTile(TileID.Anvils);

			recipe.Register();
		}

		public override void UpdateEquip(Player player)
		{
			// Better Mining
			player.pickSpeed -= 0.1f; // 10% pick speed

			// Lava protection
			player.lavaMax += 60; // 1 second lava protection
		}
	}
}
