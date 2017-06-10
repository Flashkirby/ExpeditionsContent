using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Wayfarer
{
    public class WayfarerPike : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wayfarer's Pike");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Trident);
            item.width = 32;
            item.height = 36;

            item.damage -= 1;
            item.useAnimation -= 2;
            item.useTime -= 2;
            item.knockBack += 0.5f;
            item.shoot = mod.ProjectileType("WayfarerPike");
            item.shootSpeed -= 0.4f;

            item.value = Item.buyPrice(0, 0, 50, 0);
        }
    }
}
