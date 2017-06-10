using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Wayfarer
{
    public class WayfarerClaymore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wayfarer's Claymore");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.BladeofGrass);
            item.width = 32;
            item.height = 36;
            item.scale = 1.15f;

            item.damage -= 3;
            item.useAnimation -= 8;
            item.knockBack = 7f;
            item.autoReuse = true;

            item.rare = 2;
            item.value = Item.buyPrice(0, 0, 60, 0);
        }
    }
}
