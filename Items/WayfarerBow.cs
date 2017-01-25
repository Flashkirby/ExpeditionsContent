using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class WayfarerBow : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.GoldBow);
            item.name = "Wayfarer's Bow";

            item.knockBack += 2f;
            item.shootSpeed += 3.5f;

            item.rare = 1;
            item.value = Item.buyPrice(0, 0, 50, 0);
        }
    }
}