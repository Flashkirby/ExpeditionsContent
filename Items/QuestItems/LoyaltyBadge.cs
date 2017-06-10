using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.QuestItems
{
    public class LoyaltyBadge : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Loyalty Badge");
            Tooltip.SetDefault("Increases the damage of your minions by 7%");
        }
        public override void SetDefaults()
        {
            // Round off that 23% by bee armour, hence making the slime staff deal a whole
            // 10 instead of 9 damage. WOW!
            item.width = 24;
            item.height = 24;
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
