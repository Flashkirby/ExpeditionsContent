using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumFairy : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Tales from the Fair Folk, 1st ed.",
                "'It contains photos of innately magical creatures'",
                Item.sellPrice(0, 6, 0, 0), 2, 18
                );
        }
    }
}
