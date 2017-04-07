using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
//thx 2 zoomo for shiny new sprite
namespace ExpeditionsContent.Items.QuestItems
{
    public class Telescope : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Stargazing Telescope";
            item.toolTip = "<right> to zoom when at a telescope";
            item.toolTip2 = "Watch falling stars from the world map";
            item.width = 14;
            item.height = 16;
            item.maxStack = 99;

            item.consumable = true;
            item.createTile = mod.TileType("Telescope");

            item.rare = 1;
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 0, 0, 20);
        }
    }
}