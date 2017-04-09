using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumUndead2 : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "No Bones About It, 1st ed.",
                "'The spine-tingling sequel to Risen from the Grave'",
                Item.sellPrice(0, 6, 0, 0), 2, 13
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 9 + 9);
        }
    }
}
