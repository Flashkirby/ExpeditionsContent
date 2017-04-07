using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Wayfarer
{
    public class WayfarerRepeater : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Musket);
            item.name = "Wayfarer's Repeater";
            item.width = 46;
            item.height = 20;

            item.damage -= 1;
            item.useAnimation += 6;
            item.useTime += 5;
            item.knockBack += 1.5f;
            item.shootSpeed += 2f;

            item.value = Item.buyPrice(0, 4, 0, 0);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2();
        }
    }
}