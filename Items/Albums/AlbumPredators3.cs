using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumPredators3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrarian Monster Almanac, 3rd ed.");
            Tooltip.SetDefault("'It holds information on monsters across the world'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 80, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 80, 0, 0), 3, 10
                );
        }
    }
}
