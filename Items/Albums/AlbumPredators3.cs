using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumPredators3 : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Monster Almanac, 3rd ed.",
                "'It holds information on native terrarian monsters'",
                Item.sellPrice(0, 80, 0, 0), 3, 10
                );
        }
    }
}
