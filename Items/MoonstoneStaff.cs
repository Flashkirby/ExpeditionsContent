using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class MoonstoneStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.StaffoftheFrostHydra);
            item.name = "Yutu Staff";
            item.toolTip = "Summons an orb of moonlight to heal allies and damage enemies";
            item.width = 36;
            item.height = 20;

            item.damage = 0;
            item.knockBack = 0f;
            item.shoot = mod.ProjectileType<Projs.WayfarerMoonlight>();

            item.rare = 3;
            item.value = Item.buyPrice(0, 1, 0, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Moonstone>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            return true;
        }
    }
}
