using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAnimalFirst : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Terrarian Critters, 1st ed.",
                "'It contains cute animal photos'",
                Item.sellPrice(0, 3, 0, 0), 1, 0
                );
        }

        public static void SetDefaultAlbum(ModItem mi, string name, string tooltip, int value, int rare, int placeStyle)
        {
            Item item = mi.item;
            item.name = name;
            item.toolTip2 = tooltip;
            item.width = 22;
            item.height = 30;
            item.maxStack = 99;

            item.autoReuse = true;
            item.useStyle = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.createTile = mi.mod.TileType<Tiles.PhotoAlbum>();
            item.placeStyle = placeStyle;
            item.consumable = true;

            item.rare = rare;
            item.value = value;
            
            if (value >= Item.buyPrice(1, 0, 0, 0))
            { item.toolTip = "Fetches an insane price at shops"; }
            else if (value >= Item.buyPrice(0, 50, 0, 0))
            { item.toolTip = "Fetches a huge price at shops"; }
            else if (value >= Item.buyPrice(0, 10, 0, 0))
            { item.toolTip = "Fetches a great price at shops"; }
            else if (value >= Item.buyPrice(0, 1, 0, 0))
            { item.toolTip = "Fetches a good price at shops"; }
            else
            { item.toolTip = "Fetches a decent price at shops"; }
        }
    }
}
