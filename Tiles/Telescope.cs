using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;

namespace ExpeditionsContent144.Tiles
{
	class Telescope : ModTile
	{
		public static int itemType;

		public const int tileWidth = 2;
		public const int tileHeight = 3;
		public override void SetStaticDefaults()
		{
			//extra info
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			/****ModTranslation name = CreateMapEntryName();
            name.SetDefault("Telescope");****/
			AddMapEntry(new Color(49, 121, 221), Mod.GetLocalization("Tiles.Telescope"));
			DustType = 7;
			TileID.Sets.DisableSmartCursor[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Mannequin, 0));
			TileObjectData.newTile.Width = tileWidth;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);

			//offset into ground
			TileObjectData.newTile.DrawYOffset = 2;

			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);

			TileObjectData.addTile(Type);

			itemType = ModContent.ItemType<Items.QuestItems.Telescope>();
			RegisterItemDrop(itemType, 0, 1);
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => false;


		public override void MouseOver(int i, int j)
		{
			// Unfixable issue - always uses mouseover of most recent player
			// current comprimise is to remove when in multiplayer altogether
			if (Main.netMode == NetmodeID.Server) return; // **** changed to "== NetmodeID.Server", was "> 0"

			Player player = Main.LocalPlayer;

			player.noThrow = 2;
			player.cursorItemIconID = itemType;
			player.cursorItemIconEnabled = true;
		}
	}
}