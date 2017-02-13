using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumSlimes2 : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Know Your Slimes, 2nd ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'Full of random slime trivia!'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 2;
            item.value = Item.sellPrice(0, 6, 0, 0);
        }
    }
}
