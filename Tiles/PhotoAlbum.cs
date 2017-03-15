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
        public override void SetDefaults()
        {
            //copy book stats in Main.Initialise
            Main.tileFrameImportant[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            AddMapEntry(new Color(170, 48, 114), "Photo Album");
            dustType = 18;
            
            TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.Books, 0));
            TileObjectData.newTile.RandomStyleRange = 0;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 8;
            TileObjectData.addTile(Type);
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            try
            {
                Tile t = Main.tile[i, j];
                int style = t.frameX / 18;
                style += 144 * t.frameY / 18;
                int itemToDrop = ItemID.Book; // default to book
                switch(style)
                {
                    case 0: itemToDrop = mod.ItemType<Items.Albums.AlbumAnimalFirst>(); break;
                    case 1: itemToDrop = mod.ItemType<Items.Albums.AlbumAnimals>(); break;
                    case 2: itemToDrop = mod.ItemType<Items.Albums.AlbumAnimals3>(); break;
                }
                Item.NewItem(i * 16, j * 16, 16, 16, itemToDrop);
            }
            catch { }
        }
    }
}
