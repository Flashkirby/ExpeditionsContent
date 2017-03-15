using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumPredators : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Monster Almanac, 1st ed.",
                "'It holds information about surface monsters'",
                Item.sellPrice(0, 15, 0, 0), 1, 8
                );
        }
    }
}
