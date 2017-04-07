using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Moonstone
{
    public class MoonstoneBow : ModItem
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
            item.CloneDefaults(ItemID.WoodenBow);
            item.name = "Yutu Moonshot";
            item.toolTip = "Wooden arrows turn into yutu arrows";
            item.width = 30;
            item.height = 30;

            item.damage = 26;
            item.knockBack += 2f;
            item.shootSpeed = 8.5f;
            item.useAnimation = 20;
            item.useTime = 20;

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
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6f, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == ProjectileID.WoodenArrowFriendly) type = mod.ProjectileType<Projs.MoonstoneArrow>();
            return true;
        }
    }
}
