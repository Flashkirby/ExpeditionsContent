using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Moonstone
{
    public class MoonstoneStaff : ModItem
    {
        public static short customGlowMask = 0;
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glow/" + this.GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }
            return true;
        }
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

            item.glowMask = customGlowMask; // See Autoload
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
