using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class WayfarerMoonlight : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.StaffoftheFrostHydra);
            item.name = "Wayfarer's Moonlight";
            item.toolTip = "Summons an orb of moonlight to heal allies and damage enemies";
            item.width = 36;
            item.height = 20;

            item.damage = 0;
            item.knockBack = 0f;
            item.shoot = mod.ProjectileType<Projs.WayfarerMoonlight>();

            item.rare = 2;
            item.value = Item.buyPrice(0, 0, 40, 0);
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            return true;
        }
    }
}
