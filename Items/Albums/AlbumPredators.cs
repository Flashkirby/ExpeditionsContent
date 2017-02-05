using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumPredators : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Enemy Almanac, 1st ed.";
            item.toolTip = "Increased damage and defense from certaub enemies";
            item.toolTip2 = "'Full of information about surface assailants'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;
            item.accessory = true;

            item.rare = 3;
            item.value = Item.sellPrice(0, 15, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.NPCBannerBuff[NPCID.BlueSlime] = true;
        }
    }
}
