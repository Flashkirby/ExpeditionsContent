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
            item.shootSpeed += 5.5f;

            item.value = Item.buyPrice(0, 0, 50, 0);
        }
    }
}