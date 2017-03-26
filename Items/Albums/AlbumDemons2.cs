using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumDemons2 : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Spirits and Demons, 2nd ed.",
                "'It contains terrible signs of evil forces'",
                Item.sellPrice(0, 6, 0, 0), 2, 20
                );
        }
    }
}
