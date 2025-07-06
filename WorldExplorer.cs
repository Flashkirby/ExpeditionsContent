using System;
using System.IO;

using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ExpeditionsContent144
{
	public class WorldExplorer : ModSystem
	{
		public static bool savedClerk = false;

		public override void ClearWorld()
		{
			if (Main.netMode == 2)
			{
				Console.WriteLine("Expeditions: World Initialising");
			}

			// Reset bools
			savedClerk = false;
		}

		#region SaveLoard overrides

		public override void SaveWorldData(TagCompound tag)
		{
			tag.Add("savedClerk", savedClerk);
		}

		public override void LoadWorldData(TagCompound tag)
		{
			savedClerk = tag.GetBool("savedClerk");
		}

		/****public override void LoadLegacy(BinaryReader reader)
        {
            int _version = reader.ReadInt32();
            // Booleans
            BitsByte flags = reader.ReadByte();
            savedClerk = flags[0];
        }*/

		#endregion
	}
}