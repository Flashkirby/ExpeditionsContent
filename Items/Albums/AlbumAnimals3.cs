using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAnimals3 : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Terrarian Critters, 3rd ed.",
                "'It's chock full of cute animal photos!'",
                Item.sellPrice(0, 8, 0, 0), 2, 2
                );
        }
    }
}
