using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumPredators2 : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Terrarian Monster Almanac, 2nd ed.",
                "'It holds information on surface and cavern monsters'",
                Item.sellPrice(0, 35, 0, 0), 2, 9
                );
        }
    }
}
