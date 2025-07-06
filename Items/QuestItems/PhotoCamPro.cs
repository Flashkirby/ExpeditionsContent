using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.QuestItems
{
	public class PhotoCamPro : ModItem
	{
		public const int frameWidth = 180;
		public const int frameHeight = 120;
		public const float maxFreeCapture = 450; // Max capture distance not relying on light
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Hislops-3000");
            Tooltip.SetDefault("Takes photos of creatures\n"
                + "<right> to zoom out");*/
		}
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 24;
			Item.useAmmo = ModContent.ItemType<PhotoBlank>();
			Item.UseSound = SoundID.Camera; //****new LegacySoundStyle(SoundID.Camera, 0); ????

			Item.useStyle = 4;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.autoReuse = true;

			Item.rare = 5;
			Item.value = Item.buyPrice(0, 15, 0, 0);
		}

		// Flashing effect
		public override void HoldItem(Player player)
		{
			player.scope = true;
			if (player.itemAnimation > 0)
			{
				float brightness = (float)player.itemAnimation / player.itemAnimationMax;
				Lighting.AddLight(player.Top + new Vector2(32 * player.direction, 0),
					brightness * 1.2f,
					brightness * 1.35f,
					brightness * 1.5f);
			}
		}

		// This works because UI layer;
		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			PhotoCamera.DrawCameraFrame(spriteBatch, Item, frameWidth, frameHeight);
		}

		public override bool? UseItem(Player player)
		{
			return PhotoCamera.TakePhoto(player, Item, frameWidth, frameHeight, maxFreeCapture);
		}
	}
}
