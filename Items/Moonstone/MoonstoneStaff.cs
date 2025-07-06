using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Moonstone
{
	public class MoonstoneStaff : ModItem
	{
		public static short customGlowMask = 0;
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Yutu Staff");
            Tooltip.SetDefault("Summons an orb of moonlight to heal allies and damage enemies'");*/
			if (Main.netMode != 2)
			{
				Asset<Texture2D>[] glowMasks = new Asset<Texture2D>[TextureAssets.GlowMask.Length + 1];
				for (int i = 0; i < TextureAssets.GlowMask.Length; i++)
				{
					glowMasks[i] = TextureAssets.GlowMask[i];
				}
				glowMasks[glowMasks.Length - 1] = Mod.Assets.Request<Texture2D>("Glow/" + this.GetType().Name + "_Glow");
				customGlowMask = (short)(glowMasks.Length - 1);
				TextureAssets.GlowMask = glowMasks;
			}
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.StaffoftheFrostHydra);
			Item.width = 36;
			Item.height = 20;

			Item.damage = 0;
			Item.knockBack = 0f;
			Item.shoot = ModContent.ProjectileType<Projs.WayfarerMoonlight>();

			Item.glowMask = customGlowMask; // See Autoload
			Item.rare = 3;
			Item.value = Item.buyPrice(0, 1, 0, 0);
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Moonstone>(), 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			position = Main.MouseWorld;
		}
	}
}
