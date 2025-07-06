using System;
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
	public class MoonstonePick : ModItem
	{
		public static short customGlowMask = 0;
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Yutu Pickaxe");
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
			Item.CloneDefaults(ItemID.MoltenPickaxe);
			Item.width = 34;
			Item.height = 30;

			Item.damage = 10;
			Item.knockBack = 4f;
			Item.pick = 80;
			Item.useAnimation = 15;
			Item.useTime = 12;

			Item.glowMask = customGlowMask; // See Autoload
			Item.rare = 3;
			Item.value = Item.buyPrice(0, 2, 50, 0);
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<Moonstone>(), 10);
			recipe.AddTile(TileID.Anvils);

			recipe.Register();
		}
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<Buffs.MoonlightDeBuff>(), ExpeditionC144.MoonDebuffTime);
		}
		public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
		{
			target.AddBuff(ModContent.BuffType<Buffs.MoonlightDeBuff>(), 10 * 60);
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			float length = 20 + 24f * Item.scale;
			float radius = 10;
			float angle = player.itemRotation + (0.785f * player.direction) - 1.57f;
			if (player.gravDir < 0) angle += 1.57f;
			Vector2 dustCentre = player.Center - new Vector2(2, 2);
			Vector2 tip;
			Dust d;
			for (int i = 0; i < 4; i++)
			{
				tip = dustCentre + new Vector2(
						(float)Math.Cos(angle) * length,
						(float)Math.Sin(angle) * length);

				d = Main.dust[Dust.NewDust(
					tip - new Vector2(radius, radius),
					(int)radius * 2, (int)radius * 2, 20)];
				d.velocity *= 0.1f;
				d.noGravity = true;
			}
		}
	}
}
