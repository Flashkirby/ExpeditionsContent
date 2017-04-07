using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;

namespace ExpeditionsContent.Tiles
{
    class Telescope : ModTile
    {
        public const int tileWidth = 2;
        public const int tileHeight = 3;
        public override void SetDefaults()
        {
            //extra info
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            AddMapEntry(new Color(49, 121, 221), "Telescope");
            dustType = 7;
            disableSmartCursor = true;

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
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16,
                tileWidth * 16, tileHeight * 16,
                mod.ItemType<Items.QuestItems.Telescope>());
        }
    }
}