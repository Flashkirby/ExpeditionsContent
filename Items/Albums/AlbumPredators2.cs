using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumPredators2 : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Monster Almanac, 2nd ed.";
            item.toolTip = "Fetches a great price at shops";
            item.toolTip2 = "'It contains all kinds of monster information'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 3;
            item.value = Item.sellPrice(0, 30, 0, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType<AlbumPredators>(), 1);
            recipe.AddIngredient(mod.ItemType<AlbumCavern>(), 1);
            recipe.AddIngredient(mod.ItemType<AlbumSnow>(), 1);
            recipe.AddIngredient(mod.ItemType<AlbumAntlion>(), 1);
            recipe.AddIngredient(mod.ItemType<AlbumBee>(), 1);
            recipe.AddIngredient(mod.ItemType<AlbumMushroom>(), 1);
            recipe.AddIngredient(mod.ItemType<AlbumFlora>(), 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
