using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumMushroom : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Dangers of Spore Infestation, 1st ed.",
                "'It contains strange images of mushroom-like foes'",
                Item.sellPrice(0, 6, 0, 0), 1, 28
                );
        }
    }
}
