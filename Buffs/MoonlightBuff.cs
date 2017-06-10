using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Buffs
{
    class MoonlightBuff : ModBuff
    {
        public Vector2 dustDisplace = new Vector2(0, -2);
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Guiding Moonlight");
            Description.SetDefault("Movement speed increased and provides life regeneration");
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.lifeRegen += 4;
            
            if(npc.velocity.Y == 0 && Main.time % 4 == 0 && !npc.noGravity)
            {
                Dust d = Main.dust[Dust.NewDust(npc.BottomLeft + dustDisplace, 
                    npc.width, 1, 264, 0f, -2f, 0, default(Color), 0.7f)];
                d.noGravity = true;
            }
        }
		
        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 4;
            player.moveSpeed += 0.2f;

            if (player.velocity.Y == 0 && Main.time % 4 == 0)
            {
                if(player.gravDir > 0)
                {
                    Dust d = Main.dust[Dust.NewDust(player.BottomLeft + dustDisplace, 
                        player.width, 1, 264, 0f, -2f, 0, default(Color), 0.7f)];
                    d.noGravity = true;
                }
                else
                {
                    Dust d = Main.dust[Dust.NewDust(player.TopLeft + dustDisplace, 
                        player.width, 1, 264, 0f, -2f, 0, default(Color), 0.7f)];
                    d.noGravity = true;
                }
            }
        }
    }
}
