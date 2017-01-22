using System;
using System.Collections.Generic;

using Terraria;

namespace ExpeditionsContent
{
    public static class PhotoManager
    {
        private static bool[] npcPhotos;
        public static bool[] PhotoOfNPC
        {
            get
            {
                if(!CheckedThisFrame)
                {
                    RecalculatePhotoList();
                }
                return npcPhotos;
            }
        }
        
        internal static bool CheckedThisFrame;

        public static void ResetFrame()
        {
            CheckedThisFrame = false;
        }

        private static void RecalculatePhotoList()
        {
            npcPhotos = new bool[Main.npcTexture.Length];
            CheckInventory();
        }

        private static void CheckInventory()
        {
            CheckedThisFrame = true;
            Player player = Main.player[Main.myPlayer];
            foreach (Item i in player.inventory)
            {
                if (i.type == ExpeditionC.ItemIDPhoto)
                {
                    npcPhotos[i.stack] = true;
                }
            }
        }

        public static int CountUniquePhotosInInventory(int[] NPCIDsToMatch)
        {
            int count = 0;
            foreach (int i in NPCIDsToMatch)
            {
                try
                {
                    // Add because player has it
                    if (PhotoOfNPC[i]) count++;
                }
                catch
                {
                    // Is unloaded npc/faded photo
                    // Main.NewText("Unloaded Photo " + i + "Could not be found");
                }
            }
            return count;
        }

        public static bool ConsumePhotos(int[] NPCIDsToMatch)
        {
            List<int> photosToConsume = new List<int>();
            foreach (int i in NPCIDsToMatch)
            {
                try
                {
                    // Player has no photo of npc?
                    if (!PhotoOfNPC[i])
                    {
                        return false;
                    }
                    else
                    {
                        // Add matches to list required
                        photosToConsume.Add(i);
                    }
                }
                catch
                {
                    // Is unloaded npc/faded photo
                    // Main.NewText("Unloaded Photo " + i + "Could not be found");
                    return false;
                }
            }

            // Begin removing from inventory
            foreach (Item i in Main.player[Main.myPlayer].inventory)
            {
                if (i.type == ExpeditionC.ItemIDPhoto)
                {
                    if (photosToConsume.Contains(i.stack))
                    {
                        photosToConsume.Remove(i.stack);
                        // Remove item
                        i.SetDefaults();
                        i.stack = 0;
                    }
                }
            }
            return true;
        }
    }
}
