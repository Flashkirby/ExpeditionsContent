using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExpeditionsContent.Tiles
{
    public class PhotoAlbum : ModTile
    {
        public const int tileWidth = 1;
        public const int tileHeight = 1;
        public const int styleWrapLimit = 8;
        public override void SetDefaults()
        {
            //copy book stats in Main.Initialise
            Main.tileFrameImportant[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            AddMapEntry(new Color(170, 48, 114), "Album");
            dustType = 18;
            
            TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Books, 0));
            TileObjectData.newTile.RandomStyleRange = 0;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = styleWrapLimit;
            TileObjectData.addTile(Type);
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            try
            {
                Tile t = Main.tile[i, j];
                int style = t.frameX / 18;
                style += styleWrapLimit * t.frameY / 18;
                // Main.NewText("xys: " + t.frameX + ", " + t.frameY + ", " + style);
                int itemToDrop = ItemID.Book; // default to book
                switch(style)
                {
                    case 0: itemToDrop = mod.ItemType<Items.Albums.AlbumAnimalFirst>(); break;
                    case 1: itemToDrop = mod.ItemType<Items.Albums.AlbumAnimals>(); break;
                    case 2: itemToDrop = mod.ItemType<Items.Albums.AlbumAnimals3>(); break;
                    case 3: break;
                    case 4: itemToDrop = mod.ItemType<Items.Albums.AlbumSlimes>(); break;
                    case 5: itemToDrop = mod.ItemType<Items.Albums.AlbumSlimes2>(); break;
                    case 6: break;
                    case 7: break;
                    case 8: itemToDrop = mod.ItemType<Items.Albums.AlbumPredators>(); break;
                    case 9: itemToDrop = mod.ItemType<Items.Albums.AlbumPredators2>(); break;
                    case 10: break;
                    case 11: break;
                    case 12: itemToDrop = mod.ItemType<Items.Albums.AlbumUndead>(); break;
                    case 13: itemToDrop = mod.ItemType<Items.Albums.AlbumUndead2>(); break;
                    case 14: break;
                    case 15: break;
                    case 16: itemToDrop = mod.ItemType<Items.Albums.AlbumWater>(); break;
                    case 17: itemToDrop = mod.ItemType<Items.Albums.AlbumAntlion>(); break;
                    case 18: itemToDrop = mod.ItemType<Items.Albums.AlbumFlora>(); break;
                    case 19: itemToDrop = mod.ItemType<Items.Albums.AlbumBee>(); break;
                    case 20: itemToDrop = mod.ItemType<Items.Albums.AlbumDemons>(); break;
                    case 21: break;
                    case 22: break;
                    case 23: break;
                    case 24: itemToDrop = mod.ItemType<Items.Albums.AlbumSnow>(); break;
                    case 25: break;
                    case 26: itemToDrop = mod.ItemType<Items.Albums.AlbumCavern>(); break;
                    case 27: break;
                    case 28: itemToDrop = mod.ItemType<Items.Albums.AlbumMushroom>(); break;
                    case 29: break;
                    case 30: break;
                    case 31: break;
                }
                Item.NewItem(i * 16, j * 16, 16, 16, itemToDrop);
            }
            catch { }
        }
    }
}
