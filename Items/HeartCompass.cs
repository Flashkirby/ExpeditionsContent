using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class HeartCompass : ModItem
    {
        /// <summary>
        /// Powerful accessory that reveals life crystals
        /// </summary>
        public override void SetDefaults()
        {
            item.name = "Heart Compass";
            item.toolTip = "Reveals life crystals on the Full Map";
            item.width = 28;
            item.height = 30;
            item.rare = 2;
            item.accessory = true;
            item.value = Item.sellPrice(0, 2, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            PlayerExplorer.Get(player, mod).accHeartCompass = true;
        }

        public override void UpdateInventory(Player player)
        {
            if(Main.hardMode && NPC.downedMechBossAny)
            { item.toolTip2 = "Also reveals life fruit"; }
            else
            { item.toolTip2 = ""; }
            PlayerExplorer.Get(player, mod).accHeartCompass = true;
        }

    }
}
