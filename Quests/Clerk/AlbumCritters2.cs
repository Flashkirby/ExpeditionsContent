using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumCritters2 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Birds and Beasts";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Squirrel, Red Squirrel";
            expedition.conditionDescription2 = "Cardinal, Blue Jay";
            expedition.conditionDescription3 = "Duck, Mallard Duck";
            expedition.conditionCountedMax = 6;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(mod.ItemType<Items.Albums.AlbumAnimalFirst>());

            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumAnimals>());
        }
        public override string Description(bool complete)
        {
            return "Interested in compiling more albums? Well if you are looking for more critters, here's a list for you. All of these animals live in the forests so you should be able to find them all without too much problem! ";
        }
        #region Photo Bools
        public static bool Squirrel
        { get { return PhotoManager.PhotoOfNPC[NPCID.Squirrel] || PhotoManager.PhotoOfNPC[NPCID.SquirrelGold]; } }
        public static bool SquirrelRed
        { get { return PhotoManager.PhotoOfNPC[NPCID.SquirrelRed] || PhotoManager.PhotoOfNPC[NPCID.SquirrelGold]; } }
        public static bool BlueJay
        { get { return PhotoManager.PhotoOfNPC[NPCID.BirdBlue]; } }
        public static bool Cardinal
        { get { return PhotoManager.PhotoOfNPC[NPCID.BirdRed]; } }
        public static bool Duck
        { get { return PhotoManager.PhotoOfNPC[NPCID.Duck] || PhotoManager.PhotoOfNPC[NPCID.Duck2]; } }
        public static bool DuckWhite
        { get { return PhotoManager.PhotoOfNPC[NPCID.DuckWhite] || PhotoManager.PhotoOfNPC[NPCID.DuckWhite2]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (PlayerExplorer.HoldingCamera(mod) && API.FindExpedition<AlbumCritters>(mod).completed)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (Squirrel) count++;
            if (SquirrelRed) count++;
            if (BlueJay) count++;
            if (Cardinal) count++;
            if (Duck) count++;
            if (DuckWhite) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Squirrel && SquirrelRed;
            cond2 = BlueJay && Cardinal;
            cond3 = Duck && DuckWhite;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            if (!PhotoManager.ConsumePhoto(NPCID.SquirrelGold))
            {
                PhotoManager.ConsumePhoto(NPCID.Squirrel);
                PhotoManager.ConsumePhoto(NPCID.SquirrelRed);
            }

            PhotoManager.ConsumePhoto(NPCID.BirdBlue);

            PhotoManager.ConsumePhoto(NPCID.BirdRed);

            if (!PhotoManager.ConsumePhoto(NPCID.Duck))
            { PhotoManager.ConsumePhoto(NPCID.Duck2); }

            if (!PhotoManager.ConsumePhoto(NPCID.DuckWhite))
            { PhotoManager.ConsumePhoto(NPCID.DuckWhite2); }

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
