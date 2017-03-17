using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Buffs
{
    class MoonlightDeBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffName[Type] = "Piercing Moonlight";
            Main.buffTip[Type] = "Taking damage and drop more money on death ";
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.lifeRegen -= 2 + Math.Min(npc.defDefense, npc.lifeMax / 10); // Same as venom/cursed/frost
            npc.midas = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen -= 2 + Math.Min(player.statDefense, player.statLifeMax / 10);
        }
    }
}
