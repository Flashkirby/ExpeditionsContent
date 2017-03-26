using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumCrimson2 : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Disaster Report: Corruption, 2nd ed.",
                "'It documents the horrors born of ancient spirits'",
                Item.sellPrice(0, 6, 0, 0), 2, 35
                );
        }
    }
}
