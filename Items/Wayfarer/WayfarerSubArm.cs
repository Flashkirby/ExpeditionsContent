using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Wayfarer
{
	public class WayfarerSubArm : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Wayfarer's Subber");
            Tooltip.SetDefault("50% chance not to consume ammo\n"
                + "'Spray and pray'");*/
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Minishark);
			Item.width = 42;
			Item.height = 24;

			Item.UseSound = SoundID.DD2_CrystalCartImpact; //**** new LegacySoundStyle(42, 194); // 100 is GoblinBomberThrow2. 194 is CrystalCartImpact1?
			Item.damage = 4;
			Item.knockBack = 0.5f;
			Item.useAnimation = 5;
			Item.useTime = 5;
			Item.shootSpeed += 2f;

			Item.value = Item.sellPrice(0, 1, 0, 0);
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			velocity.X += Main.rand.NextFloatDirection() * 2f;
			velocity.Y += Main.rand.NextFloatDirection() * 2f;
		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextBool();
		}

		public override void HoldItem(Player player)
		{
			if (player.itemAnimation == player.itemAnimationMax - 1)
			{
				player.itemRotation += Main.rand.NextFloatDirection() * 0.13f;
			}
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 3);
		}
	}
}