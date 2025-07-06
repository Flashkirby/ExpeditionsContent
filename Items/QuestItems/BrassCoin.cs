using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.QuestItems
{
	public class BrassCoin : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Vintage Coin");
            Tooltip.SetDefault("'Its value is in the eye of the beholder'");*/
		}
		public override void SetDefaults()
		{
			Item.maxStack = 100;
			Item.width = 16;
			Item.height = 18;
			Item.rare = -1;
		}
	}
}
