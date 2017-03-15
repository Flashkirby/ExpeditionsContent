using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumDemons : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Spirits and Demons, 1st ed.",
                "'It contains worrying photos of dark forces'",
                Item.sellPrice(0, 3, 0, 0), 1, 20
                );
        }
    }
}
