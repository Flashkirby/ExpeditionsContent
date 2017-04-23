using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class EBLunatic : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Cult of Lunacy";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 9;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Defeat the dungeon cultists";
            //expedition.conditionDescription2 = "Defeat the Lunatic Cultist";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 3, 0, 0));
        }
        public override string Description(bool complete)
        {
            string message = "With the golem's defeat, cultists have moved into the dungeon. They don't seem to be aggressive, rather they would much prefer worshipping a mysterious tablet. ";
            bool foolish = (Main.player[Main.myPlayer].statLifeMax < 500 ||
                (
                Main.player[Main.myPlayer].armor[0].rare < 8 ||
                Main.player[Main.myPlayer].armor[1].rare < 8 ||
                Main.player[Main.myPlayer].armor[2].rare < 8
                ));

            if (expedition.condition1Met)
            {
                message = "The lunatic cultist has a wide array of spells at its disposal. ";
                if (foolish)
                {
                    message += "You would do well to obtain 20 golden hearts and equip more powerful gear before challenging the cultist again. ";
                }
                else
                {
                    if (expedition.conditionCounted < 3)
                    {
                        message = "If you are having difficulties, be sure to stop attacking when the summon circle appears, and look for the real cultist.";
                    }
                    else if (expedition.conditionCounted < 6)
                    {
                        message = "The summoning circle can be beaten by looking at the cultist's masks. Additionally, only the fake cultists emit light.";
                    }
                    else
                    {
                        message = "Be wary of its summoning circle. Hit the wrong cultist, and you'll not only have to deal with multiple copies, but a lightning fast dragon.";
                    }
                }
            }
            else
            {
                if (foolish)
                {
                    message += "I would strongly recommend maxing out with 20 golden hearts, and some high-powered gear before trying anything reckless. ";
                }
            }

            return message;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                expedition.conditionDescription2 = "";
            }
            else
            { expedition.conditionDescription2 = "Defeat the Lunatic Cultist"; }

            return NPC.downedGolemBoss;
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                cond1 = npc.type == NPCID.CultistBoss;
            }
        }

        public override void OnAnyNPCDeath(NPC npc, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond2)
            {
                cond2 = npc.type == NPCID.CultistBoss;
            }
        }

        bool checkDead = false;
        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Count deaths during lunatic cultist battle
            if (!cond2)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (!npc.active) continue;
                    if (npc.type == NPCID.CultistBoss)
                    {
                        if (Main.player[Main.myPlayer].dead)
                        {
                            if (!checkDead)
                            {
                                expedition.conditionCounted++;
                                checkDead = true;
                            }
                        }
                    }
                }
                // Reset checkDead after each revive
                if (!(Main.player[Main.myPlayer].dead))
                {
                    checkDead = false;
                }
            }
            return cond1 && cond2;
        }
    }
}
