using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumSlimes : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Know Your Slimes, 1st ed.",
                "'It contains photos of slimes and slime accessories'",
                Item.sellPrice(0, 3, 0, 0), 1, 4
                );
        }
    }
}
