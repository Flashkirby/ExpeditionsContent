using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Moonstone
{
    public class MoonstoneArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yutu Arrow");
            Tooltip.SetDefault("Decreases target's defense");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WoodenArrow);

            item.shoot = mod.ProjectileType<Projs.MoonstoneArrow>();

            item.damage = 9;
            item.rare = 2;
            item.value = Item.buyPrice(0, 0, 0, 50);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Moonstone>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
