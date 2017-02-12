using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class HeartLocket : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Neck);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Heart Locket";
            item.toolTip = "Temporarily increases maximum life by 20";
            item.toolTip2 = "'Practical and stylish'";
            item.width = 22;
            item.height = 22;
            item.accessory = true;
            item.rare = 1;
            item.value = Item.buyPrice(0, 1, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
        }
    }
}
