using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumUndead : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Risen from the Grave, 1st ed.",
                "'It contains photos of zombies with rotten fashion sense'",
                Item.sellPrice(0, 3, 0, 0), 1, 12
                );
        }
    }
}
