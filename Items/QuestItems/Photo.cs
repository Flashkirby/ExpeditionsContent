using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameContent;
using ReLogic.Content;
using Terraria.DataStructures;

namespace ExpeditionsContent144.Items.QuestItems
{
	public class Photo : ModItem
	{
		public String npcModName = "";
		public String npcInteralName = "";
		public int npcType = 0;

		public Rectangle npcFrame = default(Rectangle);
		public Color npcColor = Color.White;
		public String npcDisplayName = "";
		public Asset<Texture2D> npcTexture = null;

		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Photo Film");
            Tooltip.SetDefault("Used in conjuction with a camera\n"
                + "<right> to clear the image");*/
		}
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.rare = 0;
			Item.value = Item.buyPrice(0, 0, 0, 0);
			Item.maxStack = 1;
			Item.holdStyle = ItemHoldStyleID.HoldFront;
			Item.scale = 2;

			npcDisplayName = "";
			npcType = 0;
			npcFrame = default(Rectangle);
			npcTexture = null;
		}

		public void SetupNPCInfo(int type, Rectangle frame, String displayName, Color col)
		{
			if (npcType < NPCID.Count)
			{
				this.npcModName = "Terraria";
				this.npcInteralName = "" + type;
			}
			else
			{
				ModNPC inst = NPCLoader.GetNPC(npcType);
				this.npcModName = inst.Mod.Name;
				this.npcInteralName = inst.Name;
			}
			this.npcType = type;
			this.npcFrame = frame;
			this.npcDisplayName = displayName;
			this.npcColor = col;

			npcTexture = TextureAssets.Npc[npcType];

			Item.SetNameOverride($"Photo of {npcDisplayName}, no. ({npcType})");
		}

		public override void SaveData(TagCompound tag)
		{
			tag.Add("npcName", npcInteralName);
			tag.Add("modName", npcModName);
			tag.Add("displayName", npcDisplayName);
			tag.Add("npcFrameRect", npcFrame);
			tag.Add("npcColor", npcColor);
		}

		public override void LoadData(TagCompound tag)
		{
			npcInteralName = tag.GetString("npcName");
			npcModName = tag.GetString("modName");
			npcDisplayName = tag.GetString("displayName");
			npcFrame = tag.Get<Rectangle>("npcFrameRect");
			npcColor = tag.Get<Color>("npcColor");

			npcTexture = null;
			npcType = 0;

			// Find out the current type either from base game or a mod
			if (npcModName != "Terraria")
			{
				Mod npcMod;
				ModNPC npcInst;
				if (ModLoader.TryGetMod(npcModName, out npcMod) && npcMod.TryFind<ModNPC>(npcInteralName, out npcInst))
					npcType = npcInst.Type;
				else
					return;
			}
			else
			{
				if (!int.TryParse(npcInteralName, out npcType))
				{
					npcType = 0;
					return;
				}
			}

			// Use the type to get the texture and frame count
			npcTexture = TextureAssets.Npc[npcType];

			// Set the name of the item
			Item.SetNameOverride($"Photo of {npcDisplayName}, no. ({npcType})");
		}

		public override void NetSend(BinaryWriter writer)
		{
			writer.Write7BitEncodedInt(npcType);
			writer.Write7BitEncodedInt(npcFrame.X);
			writer.Write7BitEncodedInt(npcFrame.Y);
			writer.Write7BitEncodedInt(npcFrame.Width);
			writer.Write7BitEncodedInt(npcFrame.Height);
			writer.Write(npcColor.PackedValue);
			writer.Write(npcDisplayName);
		}

		public override void NetReceive(BinaryReader reader)
		{
			int type = reader.Read7BitEncodedInt();
			int frameX = reader.Read7BitEncodedInt();
			int frameY = reader.Read7BitEncodedInt();
			int frameW = reader.Read7BitEncodedInt();
			int frameH = reader.Read7BitEncodedInt();
			Rectangle frame = new Rectangle(frameX, frameY, frameW, frameH);
			Color color = default(Color);
			color.PackedValue = reader.ReadUInt32();
			String displayName = reader.ReadString();
			SetupNPCInfo(type, frame, displayName, color);
		}


		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (npcType == 0)
			{
				tooltips.Clear();
				tooltips.Add(new TooltipLine(Mod, "PhotoImageUnloaded", "The image is clouded beyond recognition"));
				tooltips.Add(new TooltipLine(Mod, "PhotoImageMod", "Mod: " + npcModName));
				tooltips.Add(new TooltipLine(Mod, "PhotoImageMod", "Name: " + npcInteralName));
			}
		}

		#region Draw Photo
		// Going to use double size, since scale is half sized to fit NPC
		public const int viewPortWidth = 16 * 2;
		public const int viewPortHeight = 24 * 2;
		public Rectangle CalculateSourceRectangle()
		{
			// Make rectangle of first frame and get the centre point
			Rectangle rect = this.npcFrame;

			if (rect.Width > viewPortWidth)
			{
				rect.X += (rect.Width - viewPortWidth) / 2;
				rect.Width = viewPortWidth;
			}

			if (rect.Height > viewPortHeight)
			{
				rect.Y += (rect.Height - viewPortHeight) * 3 / 4;
				rect.Height = viewPortHeight;
			}

			return rect;
		}

		// Photo Clearing, actually to prevent stack fiddling
		public override bool CanRightClick()
		{
			return true;
		}
		public override void RightClick(Player player)
		{
			Item.SetDefaults();
			player.QuickSpawnItem(player.GetSource_ItemUse(Item), ModContent.ItemType<PhotoBlank>());
		}

		// Draw
		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			if (npcTexture == null) return;

			if (npcType < NPCID.Count)
				Main.instance.LoadNPC(npcType);

			Rectangle rect = CalculateSourceRectangle();

			spriteBatch.Draw(
				npcTexture.Value,
				position - origin + frame.Size() / 2,
				rect,
				this.npcColor.MultiplyRGBA(drawColor),
				0f,
				rect.Size() / 2,
				scale / 2,
				SpriteEffects.None, 0);
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			if (npcTexture == null) return;

			if (npcType < NPCID.Count)
				Main.instance.LoadNPC(npcType);

			Rectangle rect = CalculateSourceRectangle();

			spriteBatch.Draw(
				npcTexture.Value,
				Item.Center - Main.screenPosition,
				rect,
				this.npcColor.MultiplyRGBA(lightColor),
				rotation,
				rect.Size() / 2,
				scale / 2,
				SpriteEffects.None, 0);
		}

		#endregion

		public override void HoldItemFrame(Player player)
		{
			player.itemLocation += Vector2.UnitX * (-8 * player.direction);
			base.HoldItemFrame(player);
		}
	}


	public class PhotoImageLayer : PlayerDrawLayer
	{
		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{

			return drawInfo.heldItem != null
				&& drawInfo.heldItem.type == ExpeditionC144.ItemIDPhoto;
		}

		public override Position GetDefaultPosition()
		{
			return new AfterParent(PlayerDrawLayers.HeldItem);
		}

		protected override void Draw(ref PlayerDrawSet drawinfo)
		{
			Photo photo = (Photo)drawinfo.heldItem.ModItem;
			if (photo.npcTexture == null)
				return;

			if (photo.npcType < NPCID.Count)
				Main.instance.LoadNPC(photo.npcType);

			Rectangle rect = photo.CalculateSourceRectangle();

			Vector2 position = new Vector2((int)(drawinfo.ItemLocation.X - Main.screenPosition.X), (int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y));
			Rectangle itemDrawFrame = drawinfo.drawPlayer.GetItemDrawFrame(ExpeditionC144.ItemIDPhoto);
			Color col = drawinfo.heldItem.GetAlpha(drawinfo.itemColor);
			float num5 = drawinfo.drawPlayer.itemRotation;
			Vector2 origin = new Vector2((float)itemDrawFrame.Width * 0.5f - (float)itemDrawFrame.Width * 0.5f * (float)drawinfo.drawPlayer.direction, itemDrawFrame.Height);
			float adjustedItemScale = drawinfo.drawPlayer.GetAdjustedItemScale(drawinfo.heldItem);
			// drawinfo.itemEffect

			DrawData data = new DrawData(photo.npcTexture.Value, position + (itemDrawFrame.Size() / 2 - origin) * adjustedItemScale, rect, photo.npcColor.MultiplyRGBA(col), num5, rect.Size() / 2, adjustedItemScale / 2, drawinfo.itemEffect);
			drawinfo.DrawDataCache.Add(data);
		}
	}
}