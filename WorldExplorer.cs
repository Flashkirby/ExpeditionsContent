using System;
using System.IO;

using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ExpeditionsContent
{
    public class WorldExplorer : ModWorld
    {
        public static bool savedClerk = false;

        public override void Initialize()
        {
            if (Main.netMode == 2)
            {
                Console.WriteLine("Expeditions: World Initialising");
            }

            // Reset bools
            savedClerk = false;
        }

        #region SaveLoard overrides

        public override TagCompound Save()
        {
            return new TagCompound
            {
                { "savedClerk", savedClerk }
            };
        }

        public override void Load(TagCompound tag)
        {
            savedClerk = tag.GetBool("savedClerk");
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int _version = reader.ReadInt32();
            // Booleans
            BitsByte flags = reader.ReadByte();
            savedClerk = flags[0];
        }

        #endregion

    }
}