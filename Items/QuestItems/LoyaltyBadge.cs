using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.QuestItems
{
	public class LoyaltyBadge : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Loyalty Badge");
            Tooltip.SetDefault("Increases the damage of your minions by 7%");*/
		}
		public override void SetDefaults()
		{
			// Round off that 23% by bee armour, hence making the slime staff deal a whole
			// 10 instead of 9 damage. WOW!
			Item.width = 24;
			Item.height = 24;
			Item.accessory = true;
			Item.rare = 2;
			Item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Summon) += 0.07f; //**** was minionDamage before
		}
	}
}
