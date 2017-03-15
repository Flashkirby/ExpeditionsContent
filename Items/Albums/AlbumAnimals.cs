using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAnimals : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Terrarian Critters, 2nd ed.",
                "'It contains plenty of cute animal photos'",
                Item.sellPrice(0, 6, 0, 0), 2, 1
                );
        }
    }
}
