using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Moonstone
{
    public class LootBagMoonstone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moongem Box");
            Tooltip.SetDefault("Right click to open");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.maxStack = 30;
            item.rare = 2;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(mod.ItemType<Moonstone>(), Main.rand.Next(8, 13));
        }
    }
}
