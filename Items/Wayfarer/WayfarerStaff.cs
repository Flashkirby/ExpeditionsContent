using ExpeditionsContent144.Projs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Wayfarer
{
	public class WayfarerStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Wayfarer's Cane");
            Tooltip.SetDefault("Shoots an explosive bolt");*/
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.AquaScepter);
			Item.width = 36;
			Item.height = 36;
			Item.UseSound = SoundID.Item72;
			Item.staff[Type] = true;

			Item.mana = 12;
			Item.damage = 24;
			Item.useAnimation = 33;
			Item.useTime = 33;
			Item.knockBack = 3.5f;
			Item.shoot = ModContent.ProjectileType<VacuumOrb>();
			Item.shootSpeed = 7f;

			Item.value = Item.buyPrice(0, 5, 0, 0);
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2();
		}
	}
}
