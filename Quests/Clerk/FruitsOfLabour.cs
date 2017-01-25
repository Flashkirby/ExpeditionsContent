using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class FruitsOfLabour : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Fruits Of Labor";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 7;
            expedition.ctgExplore = true;
            expedition.partyShare = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.LifeFruit, 3);
            AddDeliverable(ItemID.SpelunkerPotion, 1);
        }
        public override string Description(bool complete)
        {
            if (complete) return "Huzzah! It worked! Check it out in store, I'm sure you'll appreciate it. Of course, searching for the fruit can be done almost as easily with other things I'm sure, but hopefully this item will make things a little more convenient. Happy hunting!  ";
            string guide = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
            if (guide != null) guide = "the guide";
            string doctor = NPC.GetFirstNPCNameOrNull(NPCID.WitchDoctor);
            if (doctor != null) doctor = "that weird lihzahrd";

            if(API.FindExpedition<CrystalHeart>(mod).completed)
            {
                return "I've been chatting with " + guide + ", and we think that with the help of " + doctor + "'s mysterious dark arts, it's possible to make some kind of item that senses the strange fruit growing in the jungle as of late - much like that heart compass from before! We'll need some samples though. That's your job! ";
            }
            else
            {
                return "I've been chatting with " + guide + ", and we think that with the help of " + doctor + "'s mysterious dark arts, it's possible to make some kind of item that senses the strange fruit growing in the jungle as of late. It's just an idea though, and we'll need some samples to see if it works. That's your job! ";
            }
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Needs to have completed the previus quest,
            // Have all these NPCs present, and be in hardmode after defeating a mech
            return expedition.completed
                || (NPC.FindFirstNPC(NPCID.WitchDoctor) >= 0
                && NPC.FindFirstNPC(NPCID.Guide) >= 0
                && Main.hardMode
                && NPC.downedMechBossAny);
        }
    }
}
