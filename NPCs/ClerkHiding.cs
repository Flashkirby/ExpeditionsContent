using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Expeditions144;
using Terraria.Audio;

namespace ExpeditionsContent144.NPCs
{
	class ClerkHiding : ModNPC
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Sleeping Clerk");
			Main.npcFrameCount[NPC.type] = 5;
			NPCID.Sets.TownCritter[NPC.type] = true;

		}
		public override void SetDefaults()
		{
			NPC.width = 32;
			NPC.height = 22;
			NPC.friendly = true;
			NPC.dontTakeDamage = true; //hide the health bar

			NPC.aiStyle = -1;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
			NPC.rarity = 1;
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
					spawnInfo.SpawnTileX > third && spawnInfo.SpawnTileX < Main.maxTilesX - third &&
					// in the overworld
					spawnInfo.Player.ZoneOverworldHeight &&
					// Not near bad biomes
					!spawnInfo.Player.ZoneCorrupt &&
					!spawnInfo.Player.ZoneCrimson &&
					// Can only spawn with no liquid (so in open air or grass tunnel)
					(int)Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY - 1].LiquidAmount == 0 &&
					// Not 'saved' yet
					!WorldExplorer.savedClerk &&
					// None of me exists
					!NPC.AnyNPCs(NPC.type) &&
					!NPC.AnyNPCs(ExpeditionC144.NPCIDClerk)
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
				NPC.TargetClosest();
				NPC.direction = NPC.direction * -1;
				NPC.spriteDirection = NPC.direction;
			}

			//always invincible (to enemy npcs)
			NPC.immune[255] = 30;

			// Set groud ID
			Point pos = (NPC.Bottom + new Vector2(0, 8)).ToTileCoordinates();
			Tile t = Main.tile[pos.X, pos.Y];
			ushort type = t.TileType;
			if (t == null)
			{ type = 0; }
			else { type = t.TileType; }

			NPC.ai[3] = 0f;
			if (type == TileID.Grass)
				NPC.ai[3] = 1f;
			if (type == TileID.SnowBlock || type == TileID.IceBlock)
				NPC.ai[3] = 2f;
			if (type == TileID.JungleGrass)
				NPC.ai[3] = 3f;
			if (type == TileID.Sand || type == TileID.HardenedSand)
				NPC.ai[3] = 4f;


			NPC.townNPC = false; //not a townNPC by default but this bool allows getChat
								 //transform if someone be chatting me up
			foreach (Player p in Main.player)
			{
				if (!p.active) continue;

				if (!NPC.townNPC)
				{
					NPC.townNPC = (Utils.CenteredRectangle(p.Center,
						new Vector2(NPC.sWidth, NPC.sHeight)
						).Intersects(NPC.getRect()));
				}

				if (p.talkNPC == NPC.whoAmI)
				{
					WakeUp();
				}
			}

			// Also wake up if falling
			if (NPC.velocity.Y != 0f)
			{
				WakeUp();
			}

			// Floor friction
			NPC.velocity.X = NPC.velocity.X * 0.93f;
			if (NPC.velocity.X > -0.1 && NPC.velocity.X < 0.1)
			{
				NPC.velocity.X = 0f;
			}
		}
		private void WakeUp()
		{
			//Spawn grass
			if (NPC.ai[3] > 0f)
			{
				int dust = DustID.GrassBlades;
				if (NPC.ai[3] == 2f) dust = 51; // Snow
				if (NPC.ai[3] == 3f) dust = 85; // Sand
				if (NPC.ai[3] == 4f) dust = 40; // Jungle
				for (int i = 0; i < 40; i++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height,
						dust, (i - 20) * 0.1f, -1.5f);
				}
				if (NPC.ai[3] == 2f)
				{
					SoundEngine.PlaySound(SoundID.Item51, NPC.Center);
				}
				else
				{
					SoundEngine.PlaySound(SoundID.Grass, NPC.Center);
				}
			}

			NPC.dontTakeDamage = false;
			NPC.Transform(ModContent.NPCType<Clerk>());
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frame.Y = frameHeight * (int)NPC.ai[3];
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
