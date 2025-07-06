using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
//thx 2 zoomo for shiny new sprite
namespace ExpeditionsContent144.Items.QuestItems
{
	public class Telescope : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Stargazing Telescope");
            Tooltip.SetDefault("<right> to zoom when at a telescope\n"
                + "Watch stars fall from the world map");*/
		}
		public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 16;
			Item.maxStack = 99;

			Item.consumable = true;
			Item.createTile = ModContent.TileType<Tiles.Telescope>();

			Item.rare = 1;
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.value = Item.sellPrice(0, 0, 0, 20);
		}
	}
}