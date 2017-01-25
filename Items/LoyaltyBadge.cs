using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class LoyaltyBadge : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Loyalty Badge";
            item.toolTip = "Increases the damage of your minions by 7%"; 
            // Round off that 23% by bee armour, hence making the slime staff deal a whole
            // 10 instead of 9 damage. WOW!
            item.width = 22;
            item.height = 30;
            item.accessory = true;
            item.rare = 2;
            item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.minionDamage += 0.07f;
        }
    }
}
