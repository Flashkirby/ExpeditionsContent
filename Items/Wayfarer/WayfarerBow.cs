using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Wayfarer
{
    public class WayfarerBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wayfarer's Bow");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.GoldBow);

            item.knockBack += 2f;
            item.shootSpeed += 3.5f;

            item.rare = 1;
            item.value = Item.buyPrice(0, 0, 50, 0);
        }
    }
}