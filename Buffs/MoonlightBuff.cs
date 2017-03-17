using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Buffs
{
    class MoonlightBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffName[Type] = "Guiding Moonlight";
            Main.buffTip[Type] = "Movement speed increased and provides life regeneration";
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.lifeRegen += 4;
        }
        private int lastLife = int.MaxValue;
        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 4;
            player.moveSpeed += 0.2f;

            // Show every 0.25 seconds
            if (Main.time % 15 == 0)
            {
                if (player.statLife > lastLife)
                {
                    player.HealEffect(player.statLife - lastLife, false);
                }
                lastLife = player.statLife;
            }
        }
    }
}
