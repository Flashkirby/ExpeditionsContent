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
            Main.buffTip[Type] = "Reduced defense and taking damage";
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetModInfo<ModNPCInfo>(mod).moonlight = true;

            if (npc.lifeRegen > 0) npc.lifeRegen = 0;
            npc.lifeRegen -= 1 + Math.Min(npc.defDefense, npc.lifeMax / 10); // Same as venom/cursed/frost
            Dust d = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 264, 0f, -1f)];
            d.noGravity = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PlayerExplorer>(mod).moonlit = true;

            if (player.lifeRegen > 0) player.lifeRegen = 0;
            player.lifeRegen -= 8 + Math.Min(player.statDefense, player.statLifeMax / 10);
            Dust d = Main.dust[Dust.NewDust(player.position, player.width, player.height, 264, 0f, -1f)];
            d.noGravity = true;
        }
    }
}
