using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class MoonstonePick : ModItem
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
            item.CloneDefaults(ItemID.MoltenPickaxe);
            item.name = "Yutu Pickaxe";
            item.width = 34;
            item.height = 30;

            item.damage = 10;
            item.knockBack = 4f;
            item.pick = 80;
            item.useAnimation = 15;
            item.useTime = 12;

            item.glowMask = customGlowMask; // See Autoload
            item.rare = 3;
            item.value = Item.buyPrice(0, 2, 50, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Moonstone>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            float length = 20 + 24f * item.scale;
            float radius = 10;
            float angle = player.itemRotation + (0.785f * player.direction) - 1.57f;
            if (player.gravDir < 0) angle += 1.57f;
            Vector2 dustCentre = player.Center - new Vector2(2, 2);
            Vector2 tip;
            Dust d;
            for (int i = 0; i < 4; i++)
            {
                tip = dustCentre + new Vector2(
                        (float)Math.Cos(angle) * length,
                        (float)Math.Sin(angle) * length);

                d = Main.dust[Dust.NewDust(
                    tip - new Vector2(radius, radius),
                    (int)radius * 2, (int)radius * 2, 20)];
                d.velocity *= 0.1f;
                d.noGravity = true;
            }
        }
    }
}
