using ExpeditionsContent144.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144
{
	public class Port_TileCounts : ModSystem
	{
		public static int livingWood = 0;
		public static int breakableIce = 0;
		public static int cloud = 0;
		public static int rainCloud = 0;
		public static int extractinator = 0;
		public static int hellforge = 0;
		public static int mythrilAnvil = 0;
		public static int adamantiteForge = 0;
		public static int sandstoneBrick = 0;

		public static int telescope = 0;

		public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
		{
			livingWood = tileCounts[TileID.LivingWood];
			breakableIce = tileCounts[TileID.BreakableIce];
			cloud = tileCounts[TileID.Cloud];
			rainCloud = tileCounts[TileID.RainCloud];
			extractinator = tileCounts[TileID.Extractinator];
			hellforge = tileCounts[TileID.Hellforge];
			mythrilAnvil = tileCounts[TileID.MythrilAnvil];
			adamantiteForge = tileCounts[TileID.AdamantiteForge];
			sandstoneBrick = tileCounts[TileID.SandstoneBrick];

			telescope = tileCounts[ModContent.TileType<Telescope>()];
		}
	}
}
