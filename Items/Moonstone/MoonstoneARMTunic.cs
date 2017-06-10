using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Moonstone
{
    [AutoloadEquip(EquipType.Body)]
    public class MoonstoneARMTunic : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yutu Tunic");
            Tooltip.SetDefault("Provides 1 second of immunity to lava\n"
                + "10% increased mining speed");
        }
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.defense = 6;
            item.rare = 3;
            item.value = Item.sellPrice(0, 3, 60, 0);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<Moonstone>(), 15);
            recipe.AddIngredient(ItemID.Silk, 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateEquip(Player player)
        {
            // Better Mining
            player.pickSpeed -= 0.1f; // 10% pick speed

            // Lava protection
            player.lavaMax += 60; // 1 second lava protection
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
        }
    }
}
