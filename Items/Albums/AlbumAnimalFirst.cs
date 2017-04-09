using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAnimalFirst : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Terrarian Critters, 1st ed.",
                "'It contains cute animal photos'",
                Item.sellPrice(0, 3, 0, 0), 1, 0
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3);
        }

        public static void SetDefaultAlbum(ModItem mi, string name, string tooltip, int value, int rare, int placeStyle)
        {
            Item item = mi.item;
            item.name = name;
            item.toolTip2 = tooltip;
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

            if (value >= Item.sellPrice(10, 0, 0, 0))
            { item.toolTip = "Fetches an unfathomable price at shops"; }
            else if(value >= Item.sellPrice(1, 0, 0, 0))
            { item.toolTip = "Fetches an insane price at shops"; }
            else if (value >= Item.sellPrice(0, 66, 0, 0))
            { item.toolTip = "Fetches a phenomenal price at shops"; }
            else if (value >= Item.sellPrice(0, 25, 0, 0))
            { item.toolTip = "Fetches a huge price at shops"; }
            else if (value >= Item.sellPrice(0, 10, 0, 0))
            { item.toolTip = "Fetches a great price at shops"; }
            else if (value >= Item.sellPrice(0, 1, 0, 0))
            { item.toolTip = "Fetches a good price at shops"; }
            else
            { item.toolTip = "Fetches a decent price at shops"; }
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
