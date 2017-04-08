using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Moonstone
{
    public class MoonstoneARMPants: ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            //equips.Add(EquipType.Legs);
            return true;
        }
        public override void SetDefaults()
        {
            item.name = "Yutu Pants";
            item.toolTip = "Does something";
            item.width = 22;
            item.height = 20;
            item.maxStack = 30;
            item.rare = 2;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Moonstone>(), 10);
            recipe.AddIngredient(ItemID.Silk, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
