using ExpeditionsContent144.Projs.Familiars;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Wayfarer
{
	/// <summary>
	/// Summons 3 types of familiars:
	/// Fox guards the player's space
	/// Chicken chases enemies normally
	/// Cat attacks away from the player
	/// </summary>
	public class WayfarerSummon : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Wayfarer's Bell");
            Tooltip.SetDefault("Summons a familiar to fight for you");*/
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.HornetStaff);
			Item.UseSound = SoundID.Item25;

			Item.damage = 11;
			Item.knockBack = 3f;
			Item.shoot = ModContent.ProjectileType<MinionFox>();

			// Create buff that manages the modPlayer's minion bool
			Item.buffType = ModContent.BuffType<Buffs.FamiliarMinion>();

			Item.value = Item.buyPrice(0, 10, 0, 0);
			Item.rare = 2;

			ItemID.Sets.StaffMinionSlotsRequired[Item.type] = 1;
		}
		/****public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(Item.buffType, 3600, true); ???? need to manually apply the buff?
            return true;
        }*/
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			int foxes = player.ownedProjectileCounts[ModContent.ProjectileType<MinionFox>()];
			int chickens = player.ownedProjectileCounts[ModContent.ProjectileType<MinionChicken>()];
			int cats = player.ownedProjectileCounts[ModContent.ProjectileType<MinionCat>()];
			if (foxes > chickens)
			{
				type = ModContent.ProjectileType<MinionChicken>();
			}
			else if (chickens > cats)
			{
				type = ModContent.ProjectileType<MinionCat>();
			}
			position = Main.MouseWorld - new Vector2(12, 10);
			velocity = Vector2.Zero;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2();
		}
	}
}