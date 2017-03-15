using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAntlion : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Antlion Studies, 1st ed.",
                "'Full of studies and diagrams about Antlions'",
                Item.sellPrice(0, 6, 0, 0), 1, 17
                );
        }
    }
}
