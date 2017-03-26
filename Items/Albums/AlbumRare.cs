using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumRare : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Rare Sights, 1st ed.",
                "'It contains wonderful snapshots of rare creatures'",
                Item.sellPrice(0, 10, 0, 0), 3, 18
                );
        }
    }
}
