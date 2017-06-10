using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAnimalFirst : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrarian Critters, 1st ed.");
            Tooltip.SetDefault("'It contains cute animal photos'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 3, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 3, 0, 0), 1, 0
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3);
        }

        public static void SetDefaultAlbum(ModItem mi, int value, int rare, int placeStyle)
        {
            Item item = mi.item;
            item.width = 22;
            item.height = 30;
            item.maxStack = 99;

            item.autoReuse = true;
            item.useStyle = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.createTile = mi.mod.TileType<Tiles.PhotoAlbum>();
            item.placeStyle = placeStyle;
            item.consumable = true;

            item.rare = rare;
            item.value = value;
        }

        public static string Value2ToolTip(ModItem mi, int value)
        {
            if (value >= Item.sellPrice(10, 0, 0, 0))
            { return "\nFetches an unfathomable price at shops"; }
            else if (value >= Item.sellPrice(1, 0, 0, 0))
            { return "\nFetches an insane price at shops"; }
            else if (value >= Item.sellPrice(0, 66, 0, 0))
            { return "\nFetches a phenomenal price at shops"; }
            else if (value >= Item.sellPrice(0, 25, 0, 0))
            { return "\nFetches a huge price at shops"; }
            else if (value >= Item.sellPrice(0, 10, 0, 0))
            { return "\nFetches a great price at shops"; }
            else if (value >= Item.sellPrice(0, 1, 0, 0))
            { return "\nFetches a good price at shops"; }
            else
            { return "\nFetches a decent price at shops"; }
        }

        public static void AddCopyRecipes(ModItem mi, int filmCount)
        {
            Mod mod = mi.mod;
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mi, 1);
            recipe.AddIngredient(mod.ItemType<CopyPrint>(), 1);
            recipe.AddIngredient(mod.ItemType<QuestItems.PhotoBlank>(), filmCount);
            recipe.AddTile(TileID.DyeVat);
            recipe.SetResult(mi, 2);
            recipe.AddRecipe();
        }
    }
}
