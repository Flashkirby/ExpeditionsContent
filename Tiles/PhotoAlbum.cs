using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExpeditionsContent144.Tiles
{
	public class PhotoAlbum : ModTile
	{
		public const int tileWidth = 1;
		public const int tileHeight = 1;
		public const int styleWrapLimit = 8;
		public override void SetStaticDefaults()
		{
			//copy book stats in Main.Initialise
			Main.tileFrameImportant[Type] = true;
			Main.tileNoFail[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			/****ModTranslation name = CreateMapEntryName();
            name.SetDefault("Album");*/
			AddMapEntry(new Color(170, 48, 114));
			DustType = 18;

			TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Books, 0));
			TileObjectData.newTile.RandomStyleRange = 0;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = styleWrapLimit;
			TileObjectData.addTile(Type);


			//**** Set drops based on style, but I'm lazy
			for (int i = 0; i < 32; i++)
			{
				int itemToDrop = ItemID.Book; // default to book
				switch (i)
				{
					case 0: itemToDrop = ModContent.ItemType<Items.Albums.AlbumAnimalFirst>(); break;
					case 1: itemToDrop = ModContent.ItemType<Items.Albums.AlbumAnimals>(); break;
					case 2: itemToDrop = ModContent.ItemType<Items.Albums.AlbumAnimals3>(); break;
					case 3: break;
					case 4: itemToDrop = ModContent.ItemType<Items.Albums.AlbumSlimes>(); break;
					case 5: itemToDrop = ModContent.ItemType<Items.Albums.AlbumSlimes2>(); break;
					case 6: break;
					case 7: break;
					case 8: itemToDrop = ModContent.ItemType<Items.Albums.AlbumPredators>(); break;
					case 9: itemToDrop = ModContent.ItemType<Items.Albums.AlbumPredators2>(); break;
					case 10: break;
					case 11: break;
					case 12: itemToDrop = ModContent.ItemType<Items.Albums.AlbumUndead>(); break;
					case 13: itemToDrop = ModContent.ItemType<Items.Albums.AlbumUndead2>(); break;
					case 14: break;
					case 15: break;
					case 16: itemToDrop = ModContent.ItemType<Items.Albums.AlbumWater>(); break;
					case 17: itemToDrop = ModContent.ItemType<Items.Albums.AlbumAntlion>(); break;
					case 18: itemToDrop = ModContent.ItemType<Items.Albums.AlbumFlora>(); break;
					case 19: itemToDrop = ModContent.ItemType<Items.Albums.AlbumBee>(); break;
					case 20: itemToDrop = ModContent.ItemType<Items.Albums.AlbumDemons>(); break;
					case 21: break;
					case 22: break;
					case 23: break;
					case 24: itemToDrop = ModContent.ItemType<Items.Albums.AlbumSnow>(); break;
					case 25: break;
					case 26: itemToDrop = ModContent.ItemType<Items.Albums.AlbumCavern>(); break;
					case 27: break;
					case 28: itemToDrop = ModContent.ItemType<Items.Albums.AlbumMushroom>(); break;
					case 29: break;
					case 30: break;
					case 31: break;
				}
				RegisterItemDrop(itemToDrop, i);
			}
		}

	}
}
