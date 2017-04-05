using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;

namespace ExpeditionsContent
{
    public class PhotoManager
    {
        public bool requireAll;
        public List<int> photoIDs;

        public PhotoManager(int id)
        {
            photoIDs = new List<int>();
            photoIDs.Add(id);
        }
        public PhotoManager(bool requireAll, params int[] ids)
        {
            photoIDs = new List<int>();
            foreach(int i in ids)
            {
                photoIDs.Add(i);
            }
        }

        public bool checkValid()
        {
            foreach (int id in photoIDs)
            {
                // If no require and is found, or require all and not found
                if (PhotoManager.PhotoOfNPC[id] == !requireAll)
                {
                    // TRUE AND !FALSE, !FALSE
                    // FALSE AND !TRUE, !TRUE
                    return !requireAll;
                }
            }
            return requireAll;
        }
        public void consumePhoto()
        {
            foreach (int id in photoIDs)
            {
                if (PhotoManager.ConsumePhoto(id) && requireAll)
                {
                    //Stop after first match
                    return;
                }
            }
        }

        #region static methods
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
                    // Only add photos within of NPCs that exist and not 'unloaded'
                    if (i.stack < npcPhotos.Length &&
                        i.prefix == 0) 
                    {
                        npcPhotos[i.stack] = true;
                    }
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

        /// <summary>
        /// Removes photos form the player's inventory
        /// </summary>
        /// <param name="NPCIDsToMatch">Array of NPC IDs to remove</param>
        /// <param name="matchAll">Will not run unless all photos are found</param>
        /// <returns></returns>
        public static bool ConsumePhotos(int[] NPCIDsToMatch, bool matchAll = false)
        {
            List<int> photosToConsume = new List<int>();
            foreach (int i in NPCIDsToMatch)
            {
                if (i < 0)
                {
                    //Main.NewText("Warning: Check ID " + i + " is a netID, and should not be used.");
                    continue;
                }

                try
                {
                    // Player has no photo of npc?
                    if (!PhotoOfNPC[i])
                    {
                        //Main.NewText("Player has no photo of " + i);
                        if (matchAll) return false;
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
                    //Main.NewText("Unloaded Photo " + i + "Could not be found");
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
                        //Main.NewText("remove this " + i.stack);
                        i.SetDefaults();
                        i.stack = 0;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Removes a single photo from the player's inventory
        /// </summary>
        /// <param name="NPCIDsToMatch">Array of NPC IDs to remove</param>
        /// <param name="matchAll">Will not run unless all photos are found</param>
        /// <returns></returns>
        public static bool ConsumePhoto(int NPCID)
        {
            return ConsumePhotos(new int[] { NPCID }, true);
        }
        #endregion
    }
}
