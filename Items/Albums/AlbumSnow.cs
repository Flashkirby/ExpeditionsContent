using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumSnow : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Arctic Animals, 1st ed.",
                "'It contains frosted photos of furry foes'",
                Item.sellPrice(0, 3, 0, 0), 1, 24
                );
        }
    }
}
