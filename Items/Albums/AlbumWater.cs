using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumWater : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Aquatic Wildlife, 1st ed.",
                "'It contains photos of colourful aquatic creatures'",
                Item.sellPrice(0, 3, 0, 0), 1, 16
                );
        }
    }
}
