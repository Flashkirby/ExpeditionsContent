using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumMushroom2 : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Dangers of Spore Infestation, 2nd ed.",
                "'It contains weird images of mushroom-like foes'",
                Item.sellPrice(0, 6, 0, 0), 2, 28
                );
        }
    }
}
