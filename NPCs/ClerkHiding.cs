using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Expeditions;

namespace ExpeditionsContent.NPCs
{
    class ClerkHiding : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sleeping Clerk");
            Main.npcFrameCount[npc.type] = 5;
            NPCID.Sets.TownCritter[npc.type] = true;

        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 22;
            npc.friendly = true;
            npc.dontTakeDamage = true; //hide the health bar

            npc.aiStyle = -1;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            npc.rarity = 1;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // Skip if 'rescued' clerk already (no more natural spawn)
            if (WorldExplorer.savedClerk) return 0f;

            // Will only spawn once a player has armour, and has powered up at least once
            bool spawnCondition = false;
            foreach (Player p in Main.player)
            {
                if (p.statDefense >= 6 && p.statLifeMax > 100 && p.statManaMax > 20)
                {
                    spawnCondition = true;
                    break;
                }
            }
            if (!spawnCondition) return 0f;

            try
            {
                int third = Main.maxTilesX / 3;
                if (
                    // Within centre third of world
                    spawnInfo.spawnTileX > third && spawnInfo.spawnTileX < Main.maxTilesX - third &&
                    // in the overworld
                    spawnInfo.player.ZoneOverworldHeight &&
                    // Not near bad biomes
                    !spawnInfo.player.ZoneCorrupt &&
                    !spawnInfo.player.ZoneCrimson &&
                    // Can only spawn with no liquid (so in open air or grass tunnel)
                    (int)Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY - 1].liquid == 0 &&
                    // Not 'saved' yet
                    !WorldExplorer.savedClerk &&
                    // None of me exists
                    !NPC.AnyNPCs(npc.type) &&
                    !NPC.AnyNPCs(ExpeditionC.NPCIDClerk)
                    )
                {
                    //if (ExpeditionsContent.DEBUG) Main.NewText("Spawned succesfully!", 50, 255, 100);
                    return 1f; //guaranteed to spawn on next call (because we want to be found)
                }
            }
            catch { } //I hate array errors
            return 0f;
        }

        bool onSpawn = true;
        public override void AI()
        {
            //face away from player on spawn
            if (onSpawn)
            {
                onSpawn = false;
                npc.TargetClosest();
                npc.direction = npc.direction * -1;
                npc.spriteDirection = npc.direction;
            }
            
            //always invincible (to enemy npcs)
            npc.immune[255] = 30;
            
            // Set groud ID
            Point pos = (npc.Bottom + new Vector2(0, 8)).ToTileCoordinates();
            Tile t = Main.tile[pos.X, pos.Y];
            ushort type = t.type;
            if (t == null)
            { type = 0; }
            else { type = t.type; }

            npc.ai[3] = 0f;
            if (type == TileID.Grass)
                npc.ai[3] = 1f;
            if (type == TileID.SnowBlock || type == TileID.IceBlock)
                npc.ai[3] = 2f;
            if (type == TileID.JungleGrass)
                npc.ai[3] = 3f;
            if (type == TileID.Sand || type == TileID.HardenedSand)
                npc.ai[3] = 4f;


            npc.townNPC = false; //not a townNPC by default but this bool allows getChat
            //transform if someone be chatting me up
            foreach (Player p in Main.player)
            {
                if (!p.active) continue;

                if (!npc.townNPC)
                {
                    npc.townNPC = (Utils.CenteredRectangle(p.Center, 
                        new Vector2(NPC.sWidth, NPC.sHeight)
                        ).Intersects(npc.getRect()));
                }

                if (p.talkNPC == npc.whoAmI)
                {
                    WakeUp();
                }
            }

            // Also wake up if falling
            if(npc.velocity.Y != 0f)
            {
                WakeUp();
            }

            // Floor friction
            npc.velocity.X = npc.velocity.X * 0.93f;
            if (npc.velocity.X > -0.1 && npc.velocity.X < 0.1)
            {
                npc.velocity.X = 0f;
            }
        }
        private void WakeUp()
        {
            //Spawn grass
            if (npc.ai[3] > 0f)
            {
                int dust = DustID.GrassBlades;
                if (npc.ai[3] == 2f) dust = 51; // Snow
                if (npc.ai[3] == 3f) dust = 85; // Sand
                if (npc.ai[3] == 4f) dust = 40; // Jungle
                for (int i = 0; i < 40; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height,
                        dust, (i - 20) * 0.1f, -1.5f);
                }
                if (npc.ai[3] == 2f)
                {
                    Main.PlaySound(2, npc.Center, 51);
                }
                else
                {
                    Main.PlaySound(6, npc.Center);
                }
            }

            npc.dontTakeDamage = false;
            npc.Transform(mod.NPCType<Clerk>());
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameHeight * (int)npc.ai[3];
        }

        public override string GetChat()
        {
            WakeUp();
            switch (Main.rand.Next(3))
            {
                case 1:
                    return "Waah!? I wasn't sleeping on the job, honest. ";
                case 2:
                    return "Oh! Don't mind me, I was just taking a... power nap. Yes. ";
                case 3:
                    return "Mmhmmn, give me five more minutes.... What? You need something? ";
                default:
                    return "Y-yes sir? Wait a minute, you're not my boss. Eh, whatever. ";
            }
        }
    }
}
