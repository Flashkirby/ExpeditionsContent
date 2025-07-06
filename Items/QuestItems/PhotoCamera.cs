using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.QuestItems
{
	public class PhotoCamera : ModItem
	{
		public const int frameWidth = 180;
		public const int frameHeight = 120;
		public const float maxFreeCapture = 350; // Max capture distance not relying on light
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("PhotoTron");
            Tooltip.SetDefault("Takes photos of creatures\n"
                + "'Say cheese!'");*/
		}
		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 26;
			Item.useAmmo = ModContent.ItemType<PhotoBlank>();
			Item.UseSound = SoundID.Camera; //****new LegacySoundStyle(SoundID.Camera, 0); ????

			Item.useStyle = 4;
			Item.useAnimation = 40;
			Item.useTime = 40;

			Item.rare = 2;
			Item.value = Item.buyPrice(0, 3, 0, 0);
		}

		// Flashing effect
		public override void HoldItem(Player player)
		{
			if (player.itemAnimation > 0)
			{
				float brightness = (float)player.itemAnimation / player.itemAnimationMax;
				Lighting.AddLight(player.Top,
					brightness * 1f,
					brightness * 0.9f,
					brightness * 0.8f);
			}
		}

		// This works because UI layer;
		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			PhotoCamera.DrawCameraFrame(spriteBatch, Item, frameWidth, frameHeight);
		}

		public override bool? UseItem(Player player)
		{
			Lighting.AddLight(player.Top,
				1f,
				0.9f,
				0.8f);
			return PhotoCamera.TakePhoto(player, Item, frameWidth, frameHeight, maxFreeCapture);
		}

		#region Static Methods

		public static Rectangle GetCameraFrame(int width, int height)
		{
			return new Rectangle(
				Main.mouseX + (int)Main.screenPosition.X - frameWidth / 2,
				Main.mouseY + (int)Main.screenPosition.Y - frameHeight / 2,
				frameWidth,
				frameHeight);
		}

		public static void DrawCameraFrame(SpriteBatch spriteBatch, Item item, int width, int height)
		{
			DrawCameraFrame(spriteBatch, item, GetCameraFrame(width, height));
		}
		public static void DrawCameraFrame(SpriteBatch spriteBatch, Item item, Rectangle r)
		{
			if (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem] == item)
			{
				// Draw camera frame
				spriteBatch.Draw(
				ExpeditionC144.CameraFrameTexture.Value,
				r.Location.ToVector2() - Main.screenPosition,
				Color.White
				);
			}
		}

		public static bool TakePhoto(Player player, Item item, int width, int height, float range)
		{
			return TakePhoto(player, item, GetCameraFrame(width, height), range);
		}
		public static bool TakePhoto(Player player, Item item, Rectangle cameraFrame, float range)
		{
			if (player.whoAmI != Main.myPlayer) return true;

			bool canTakePicture = false;

			NPC npc = Main.npc[0];
			float distance = 1000f;
			foreach (NPC n in Main.npc)
			{
				if (!n.active) continue;
				if (cameraFrame.Intersects(n.getRect()))
				{
					// Can't take pictures if too dark
					Point centre = n.Center.ToTileCoordinates();
					int darkness = (int)((1f - Lighting.Brightness(centre.X, centre.Y)) * 255);
					if (darkness > 240)
					{
						// too dark, if player can see below that range
						if (npc.Distance(player.Center) > range ||
							!Collision.CanHit(npc.position, npc.width, npc.height,
							player.position, player.width, player.height))
						{
							continue;
						}
					}

					// Get the closest
					float dist = n.Distance(cameraFrame.Center.ToVector2());
					if (dist < distance)
					{
						distance = dist;
						npc = n;
						canTakePicture = true;
					}
				}
			}

			if (!canTakePicture) return true;

			// Check for camera roll
			foreach (Item i in player.inventory)
			{
				if (i.ammo == item.useAmmo)
				{
					i.stack--;
					canTakePicture = true;
					break;
				}
			}
			if (!canTakePicture) return true;

			// Spawn the item
			int number = Item.NewItem(player.GetSource_ItemUse_WithPotentialAmmo(item, item.useAmmo), (int)player.position.X, (int)player.position.Y, player.width, player.height, ExpeditionC144.ItemIDPhoto, 1, false, -1, false, false);

			// **** Get the NPC's frame
			Rectangle npcFrame = npc.frame;
			Vector2 relCursor = cameraFrame.Center.ToVector2() - npc.VisualPosition + npc.Size / 2;
			// **** - Clip the frame to the section clicked on
			/*relCursor.X *= npc.spriteDirection;
			relCursor = relCursor.RotatedBy(npc.rotation);
			relCursor = (relCursor / npc.scale) + npcFrame.Size()/2;
			// **** - Bound to within the orignal frame
			Rectangle realFrame = new Rectangle((int)(relCursor.X - Photo.viewPortWidth / 2), (int)(relCursor.Y - Photo.viewPortHeight / 2), Photo.viewPortWidth, Photo.viewPortHeight);
			if(realFrame.Width > npcFrame.Width)
			{
				realFrame.X = npcFrame.X;
				realFrame.Width = npcFrame.Width;
			}
			if (realFrame.Height > npcFrame.Height)
			{
				realFrame.Y = npcFrame.Y;
				realFrame.Height = npcFrame.Height;
			}
			if (realFrame.X < npcFrame.X) realFrame.X = npcFrame.X;
			if (realFrame.Y < npcFrame.Y) realFrame.Y = npcFrame.Y;
			if (realFrame.X + realFrame.Width > npcFrame.X + npcFrame.Width) realFrame.X = npcFrame.X + npcFrame.Width - realFrame.Width;
			if (realFrame.Y + realFrame.Height> npcFrame.Y + npcFrame.Height) realFrame.Y = npcFrame.Y + npcFrame.Height - realFrame.Height;*/
			// **** Get the NPC's color
			Color npcColor = npc.GetAlpha(npc.color != Color.Transparent ? npc.color : Color.White);
			// Set the details!
			((Photo)Main.item[number].ModItem).SetupNPCInfo(npc.type, npcFrame, npc.FullName, npcColor);

			// Send the item
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
			}

			return true;
		}

		#endregion
	}
}
