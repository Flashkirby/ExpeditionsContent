using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class WayfarerCarbine : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.TheUndertaker);
            item.name = "Wayfarer's Carbine";
            item.width = 36;
            item.height = 18;

            item.damage += 1;
            item.useAnimation += 2;
            item.useTime += 2;
            item.knockBack += 1f;
            item.shootSpeed += 1.5f;

            item.value = Item.buyPrice(0, 3, 0, 0);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2();
        }
    }
}