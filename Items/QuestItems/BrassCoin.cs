using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.QuestItems
{
    public class BrassCoin : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Vintage Coin";
            item.toolTip = "'Its value is in the eye of the beholder'";
            item.maxStack = 100;
            item.width = 16;
            item.height = 18;
            item.rare = -1;
        }
    }
}
