using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class WayfarerClaymore : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.BladeofGrass);
            item.name = "Wayfarer's Claymore";
            item.toolTip = "";
            item.toolTip2 = "";
            item.width = 32;
            item.height = 36;
            item.scale = 1.15f;

            item.damage += 3; //poisoned is 2/s for 7s on 25% change so average 3.5
            item.useAnimation -= 3;
            item.knockBack = 7f;
            item.autoReuse = true;

            item.rare = 2;
            item.value = Item.buyPrice(0, 0, 60, 0);
        }
    }
}
