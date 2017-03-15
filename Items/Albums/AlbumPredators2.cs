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
                "Monster Almanac, 2nd ed.",
                "'It holds information on surface and caverns monsters'",
                Item.sellPrice(0, 30, 0, 0), 2, 9
                );
        }
    }
}
