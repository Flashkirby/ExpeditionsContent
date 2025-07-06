using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Wayfarer
{
	public class WayfarerBeam : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Wayfarer's Beam");
            Tooltip.SetDefault("Fires a concentrated beam");*/
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.AquaScepter);
			Item.width = 46;
			Item.height = 16;
			Item.UseSound = SoundID.Item12;

			Item.mana = 11;
			Item.damage = 30;
			Item.useAnimation = 20;
			Item.useTime = 20; // **** was 19 instead of 20 before
			Item.knockBack = 2f;
			Item.shoot = ModContent.ProjectileType<Projs.WayBeam>();
			Item.shootSpeed = 3f;

			Item.rare = 2;
			Item.value = Item.sellPrice(0, 1, 0, 0);
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 2);
		}
	}
}