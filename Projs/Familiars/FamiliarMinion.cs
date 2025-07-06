using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Projs.Familiars
{
	abstract class FamiliarMinion : ModProjectile
	{
		// Animation Frames
		public int idleFrame = 0;
		public int idleFrameCount = 1;
		public int idleFrameSpeed = 0;
		public int attackFrame = 1;
		public int attackFrameCount = 3;
		public int runFrame = 1;
		public int runFrameCount = 7;
		public float runFrameSpeedMod = 1f; //frames incremented per 2px
		public int flyFrame = 8;
		public int flyFrameCount = 4;
		public int flyFrameSpeed = 6;
		public float flyRotationMod = 0.7f;
		public int fallFrame = 12;
		public int fallFrameCount = 1;
		public int fallFrameSpeed = 0;

		// Code Constants
		public const float separatedDistance = 700f; // Distance before returning
		public const float separatedReturnAccel = 10f; // Max return speed, inceases to player speed
		public const float joinAccel = 0.1f; // Acceleration to catching up
		public const int joinDistance = 200; // Distance at which can stop flying
		public const int joinCloseDistance = 60; // Distance to stay close to

		public const float targetRange = 800f; // Range to find NPCs
		public const float chaseRange = 600f; // Range to begin chasing

		public const int attackAnimationMax = 15; //Time spent in attack state
		public const float attackRange = 30f; // Range to start attack state

		public const float topSpeed = 3f; // Max ground speed, inceases to player speed
		public const float slowStopAccel = 0.1f; // Speedup/Slowing down speed
		public const float quickStopAccel = 0.3f; // Speedup/Slowing down if moving wrong way
		public const float quickStopFastAccel = 0.5f; // Speedup/Slowing down if moving wrong way, if moving faster than normal (see topSpeed)

		/// <summary> Targetting uses player not Projectile </summary>
		public bool AIPrioritiseNearPlayer = false;
		/// <summary> Targetting constantly looks for furthest away enemy </summary>
		public bool AIPrioritiseFarEnemies = false;

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			// CHeck if the player still uses Projectile minion type
			if (!CheckActive(player)) return;

			LogicManager(player);

			// Set the frames used by the minion
			ManageFrames(ref Projectile.frame, ref Projectile.frameCounter);

			// Natural "Friction"
			//Projectile.velocity *= 0.99f;

			if (aiState == 0)
			{
				// Apply gravity
				Projectile.velocity.Y = Math.Min(Projectile.velocity.Y + 0.4f, 10f);
			}
		}

		private bool CheckActive(Player player)
		{
			if (!player.active)
			{
				Projectile.active = false;
				return false;
			}
			PlayerExplorer modPlayer = player.GetModPlayer<PlayerExplorer>();
			if (player.dead)
			{
				modPlayer.familiarMinion = false;
			}
			if (modPlayer.familiarMinion)
			{
				// Minion will disappear 2 frames after bool is false
				Projectile.timeLeft = 2;
			}
			return true;
		}

		private int aiState
		{
			get { return (int)Projectile.ai[0]; }
			set { Projectile.ai[0] = value; }
		}
		private int aiAttackAnimation
		{
			get { return (int)Projectile.ai[1]; }
			set { Projectile.ai[01] = value; }
		}
		private void LogicManager(Player player)
		{
			// Calculate idle positon behind player
			Vector2 goalVector = player.Center;
			goalVector.X -= (15 + player.width / 2) * player.direction;
			goalVector.X -= Projectile.minionPos * 40 * player.direction;
			NPC target = null;

			// Find an enemy target
			//!/Main.NewText("SelectTarget Fine");
			if (aiState == 0)
			{
				target = SelectTarget(target);
			}
			// Catch up with players when separated
			//!/Main.NewText("CatchUpWithPlayer Fine");
			if (aiState == 1)
			{
				CatchUpWithPlayer(player);
			}
			// Attacking, or damaging state
			//!/Main.NewText("ExecuteAttackState Fine");
			if (aiState == 2)
			{
				ExecuteAttackState();
			}

			// Manage ai states for attacking and goal vector
			//!/Main.NewText("SetGoalToTargetAndAttack Fine");
			if (target != null)
			{
				goalVector = SetGoalToTargetAndAttack(goalVector, target, player);
			}
			// Manage ai state for being separated from the player
			//!/Main.NewText("CheckSeparated Fine");
			if (aiState == 0)
			{
				if (target == null) CheckSeparated(player);

				Projectile.tileCollide = true;

				//!/Main.NewText("ControlMovementPhysics Fine");
				ControlMovementPhysics(player, goalVector);
			}
		}

		#region LogicManager Methods

		private NPC SelectTarget(NPC target)
		{
			// Start with ownerMinion, then cycle through first 200 if not
			NPC npc = Projectile.OwnerMinionAttackTargetNPC;
			if (npc != null)
			{
				//!/ Main.NewText("<Fox> Looking at targetted foe!");
				if (npc.CanBeChasedBy(Projectile, false))
				{
					float distance = (npc.Center - Projectile.Center).Length();
					if (distance < targetRange)
					{
						target = npc;
					}
				}
			}
			//!/ Main.NewText("<Fox> Target is still null...");
			if (target == null)
			{
				float maxChase = targetRange;
				if (AIPrioritiseFarEnemies) maxChase = 0;
				float distance = targetRange;
				// Try the same but for everything else
				for (int i = 0; i < 200; i++)
				{
					// Only select if not -1
					npc = Main.npc[i];
					if (npc.CanBeChasedBy(Projectile, false))
					{
						if (AIPrioritiseNearPlayer)
						{
							distance = (npc.Center - Main.player[Projectile.owner].Center).Length();
						}
						else
						{
							distance = (npc.Center - Projectile.Center).Length();
						}
						if (AIPrioritiseFarEnemies)
						{
							if (distance > maxChase && distance < targetRange)
							{
								target = npc;
								maxChase = distance;
							}
						}
						else
						{
							if (distance < maxChase)
							{
								target = npc;
								maxChase = distance;
							}
						}
					}
				}
			}
			//!/ Main.NewText("<Fox> Found a target!");
			return target;
		}

		private void CatchUpWithPlayer(Player player)
		{
			Projectile.tileCollide = false;

			Vector2 vectorToPlayer = (player.Center - Projectile.Center);
			float distance = vectorToPlayer.Length();

			// Get the target accel to reach to catch up to player
			float targetAccel = Math.Max(separatedReturnAccel, Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y));

			// Too far away, just teleport
			TeleportIfFarAway(player, distance);

			if (distance < joinDistance && // Within Join distance
				player.velocity.Y == 0f && //Player is grounded
				Projectile.Center.Y <= player.BottomLeft.Y && // Above the player
				!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height) // Not in a tile
				)
			{
				// Update to 0 and stop gaining height past 6 velocity
				aiState = 0;
				Projectile.netUpdate = true;
				Projectile.velocity.Y = Math.Max(Projectile.velocity.Y, -6f);
			}

			// Speed up until close enough, if not within join distance
			if (distance >= joinCloseDistance)
			{
				//!/Main.NewText("<Fox> Too far!");
				vectorToPlayer.Normalize();
				vectorToPlayer *= targetAccel;

				if (Projectile.velocity.X < vectorToPlayer.X)
				{
					Projectile.velocity.X = Projectile.velocity.X + joinAccel;
					if (Projectile.velocity.X < 0f)
					{
						Projectile.velocity.X = Projectile.velocity.X + joinAccel * 1.5f;
					}
				}
				if (Projectile.velocity.X > vectorToPlayer.X)
				{
					Projectile.velocity.X = Projectile.velocity.X - joinAccel;
					if (Projectile.velocity.X > 0f)
					{
						Projectile.velocity.X = Projectile.velocity.X - joinAccel * 1.5f;
					}
				}
				if (Projectile.velocity.Y < vectorToPlayer.Y)
				{
					Projectile.velocity.Y = Projectile.velocity.Y + joinAccel;
					if (Projectile.velocity.Y < 0f)
					{
						Projectile.velocity.Y = Projectile.velocity.Y + joinAccel * 1.5f;
					}
				}
				if (Projectile.velocity.Y > vectorToPlayer.Y)
				{
					Projectile.velocity.Y = Projectile.velocity.Y - joinAccel;
					if (Projectile.velocity.Y > 0f)
					{
						Projectile.velocity.Y = Projectile.velocity.Y - joinAccel * 1.5f;
					}
				}
			}
		}
		private bool TeleportIfFarAway(Player player, float distance)
		{
			if (distance > 2000f)
			{
				Projectile.Center = player.Center;
				return true;
			}
			return false;
		}

		private void ExecuteAttackState()
		{
			Projectile.friendly = true;

			// Apply gravity
			Projectile.velocity.Y = Math.Min(Projectile.velocity.Y + 0.4f, 10f);

			// Count down and end attack state/animation
			aiAttackAnimation--;
			if (aiAttackAnimation <= 0)
			{
				aiAttackAnimation = 0;
				aiState = 0;
				Projectile.friendly = false;
				Projectile.netUpdate = true;
			}
		}

		private Vector2 SetGoalToTargetAndAttack(Vector2 goalVector, NPC target, Player player)
		{
			// Define chase range, reduced underground for reasons of space/visibility
			float maxChase = chaseRange;
			if (Projectile.position.Y > Main.worldSurface * 16.0)
			{ maxChase *= 0.7f; };

			// Chase if the enemy is visibly within chase distance
			Vector2 vectorToNPC = (target.Center - Projectile.Center);
			float distance = vectorToNPC.Length();

			//// Check Projectile can see the npc
			bool canHit = Collision.CanHit(
				Projectile.position - new Vector2(0, 4),
				Projectile.width, Projectile.height,
				target.position, target.width, target.height);
			//// If can't see the npc, but I'm close to player, carry on.
			//!/ Main.NewText("<Fam> " + canHit + " & " + Vector2.Distance(Projectile.Center, player.Center) + " < " + separatedDistance);
			if (!canHit)
			{
				// Too far away then go back to player otherwise carry on
				if (!CheckSeparated(player))
				{
					canHit = true;
				}
			}

			if (distance < maxChase && canHit)
			{
				goalVector = target.Center;
				// Jump if the goal is too high
				JumpToReach(goalVector);
			}
			if (distance < attackRange)
			{
				aiState = 2;
				aiAttackAnimation = attackAnimationMax;
				Projectile.netUpdate = true;
			}

			return goalVector;
		}
		private void JumpToReach(Vector2 goalVector)
		{
			float jumpDistance = goalVector.Y - Projectile.Center.Y;
			if (Projectile.velocity.Y == 0f) // Am grounded
			{
				if (jumpDistance < -10) Projectile.velocity.Y = -6f;
				if (jumpDistance < -70) Projectile.velocity.Y = -10f;
				if (jumpDistance < -120f) Projectile.velocity.Y = -13f;
				if (jumpDistance < -210f) Projectile.velocity.Y = -15f;
				if (jumpDistance < -270f) Projectile.velocity.Y = -17f;
				if (jumpDistance < -310f) Projectile.velocity.Y = -18f;
			}
		}

		private bool CheckSeparated(Player player)
		{
			float distance = Vector2.Distance(Projectile.Center, player.Center);

			// Too far away, just teleport
			if (!TeleportIfFarAway(player, distance))
			{
				// Otherwise become seperated, or if flying
				if (distance > separatedDistance
					|| Math.Abs(Projectile.Center.Y - player.Center.Y) > 300f
					|| player.rocketDelay2 > 0)
				{
					aiState = 1;
					Projectile.netUpdate = true;
					Projectile.velocity.Y = 0f;
					return true;
				}
			}
			return false;
		}

		private void ControlMovementPhysics(Player player, Vector2 goalVector)
		{
			// Calculate movement values
			float quickStop = quickStopAccel;
			float maxTopSpeed = Math.Max(topSpeed, Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y));
			if (maxTopSpeed > topSpeed) quickStop = quickStopFastAccel;

			// Used for checking for tiles in the way
			bool pathBlocked = false;

			float diffX = goalVector.X - Projectile.Center.X;
			int dirMod = Math.Sign(diffX);
			// Over 5px away
			if (Math.Abs(diffX) > 5f)
			{
				// Moving backwards fast
				if (Projectile.velocity.X * dirMod < topSpeed)
				{
					Projectile.velocity.X += quickStop * dirMod;
				}
				else // Not moving backwards fast, or moving forwards
				{
					Projectile.velocity.X += slowStopAccel * dirMod;
				}

				pathBlocked = CheckForPathBlocking(pathBlocked, dirMod);
				//if (pathBlocked) Main.NewText("<Fox> Path is blocked!");


			}
			else //Very close to goal
			{
				// Slowdown, then stop
				Projectile.velocity.X *= 0.9f;
				if (Math.Abs(Projectile.velocity.X) < quickStop * 2f)
				{
					Projectile.velocity.X = 0f;
				}
			}

			// Step up collision physics
			Collision.StepUp(ref Projectile.position, ref Projectile.velocity,
				Projectile.width, Projectile.height, ref Projectile.stepSpeed,
				ref Projectile.gfxOffY, 1, false, 0);

			// Jump over blocked path if grounded
			TryJumpingOverObstacle(pathBlocked, dirMod);

			// Clamp velocity X
			Math.Min(Math.Max(Projectile.velocity.X, maxTopSpeed), -maxTopSpeed);

			// Set direction
			Projectile.direction = Math.Sign(Projectile.velocity.X);
			if (Projectile.velocity.X * dirMod > quickStop * dirMod)
			{
				Projectile.direction = dirMod;
			}
		}
		private bool CheckForPathBlocking(bool pathBlocked, int direction)
		{
			// Check ahead to see if there is a tile blocking me
			Point checkTile = Utils.ToTileCoordinates(Projectile.Top);
			checkTile.X += direction + (int)Projectile.velocity.X;
			for (int y = checkTile.Y; y < checkTile.Y + Projectile.height / 16 + 1; y++)
			{
				if (WorldGen.SolidTile(checkTile.X, y))
				{
					pathBlocked = true;
					break;
				}
			}

			return pathBlocked;
		}
		private void TryJumpingOverObstacle(bool pathBlocked, int dirMod)
		{
			if (Projectile.velocity.Y == 0f && pathBlocked)
			{
				for (int i = 0; i < 3; i++)
				{
					Vector2 checkPoint = Projectile.position;
					checkPoint.X += i * Projectile.width / 2;
					checkPoint.X += dirMod * 8 + (int)Projectile.velocity.X;
					Point cTile = Utils.ToTileCoordinates(checkPoint);

					/*
                    if (pathBlocked) Main.NewText("<Fox> Thinking of jumping!");
                    for (int wd = 0; wd < 12; wd++)
                    { Dust d = Main.dust[Dust.NewDust(cTile.ToVector2() * 16f, 16, 16, 16)];
                        d.noGravity = true;
                        d.velocity = Vector2.Zero;
                    }
                    */

					// If I need to jump...
					if (WorldGen.SolidTile(cTile.X, cTile.Y) ||
						Main.tile[cTile.X, cTile.Y].IsHalfBlock ||
						Main.tile[cTile.X, cTile.Y].Slope > 0 ||
						(
							TileID.Sets.Platforms[(int)Main.tile[cTile.X, cTile.Y].TileType] &&
							Main.tile[cTile.X, cTile.Y].HasTile && !Main.tile[cTile.X, cTile.Y].IsActuated
							))
					{
						try
						{
							cTile = Utils.ToTileCoordinates(Projectile.Center);
							cTile.X += dirMod + (int)Projectile.velocity.X;
							if (!WorldGen.SolidTile(cTile.X, cTile.Y - 1) && !WorldGen.SolidTile(cTile.X, cTile.Y - 2))
							{
								Projectile.velocity.Y = -5.1f;
							}
							else if (!WorldGen.SolidTile(cTile.X, cTile.Y - 2))
							{
								Projectile.velocity.Y = -7.1f;
							}
							else if (WorldGen.SolidTile(cTile.X, cTile.Y - 5))
							{
								Projectile.velocity.Y = -11.1f;
							}
							else if (WorldGen.SolidTile(cTile.X, cTile.Y - 4))
							{
								Projectile.velocity.Y = -10.1f;
							}
							else
							{
								Projectile.velocity.Y = -9.1f;
							}
						}
						catch
						{
							Projectile.velocity.Y = -9.1f;
						}
					}
				}
			}
		}

		#endregion


		private void ManageFrames(ref int frame, ref int frameCounter)
		{
			// Flying
			if (aiState == 1)
			{
				// Face direction of velocity
				if (Projectile.velocity.X != 0f)
				{
					Projectile.spriteDirection = Math.Sign(Projectile.velocity.X);
				}
				// Rotate in travel direction
				Projectile.rotation = Projectile.velocity.X * 0.1f * flyRotationMod;

				if (Projectile.frame < flyFrame || Projectile.frame > flyFrame + flyFrameCount - 1) Projectile.frame = flyFrame;

				Projectile.frameCounter--;
				if (Projectile.frameCounter <= 0 || Projectile.frameCounter > flyFrameSpeed)
				{
					Projectile.frame++;
					Projectile.frameCounter = flyFrameSpeed;
					if (Projectile.frame > flyFrame + flyFrameCount - 1)
					{
						Projectile.frame = flyFrame;
					}
				}
			}

			// Attacking
			if (aiState == 2)
			{
				Projectile.spriteDirection = Projectile.direction;
				Projectile.rotation = 0f;

				if (Projectile.frame < attackFrame || Projectile.frame > attackFrame + attackFrameCount - 1) Projectile.frame = attackFrame;

				//progress from 0 to 1
				float progress = 1f - (float)aiAttackAnimation / attackAnimationMax;
				if (attackFrameCount > 0)
				{
					float frameStep = 1f / attackFrameCount;
					Projectile.frame = attackFrame + (int)(progress / frameStep);
				}
			}

			// Movement + Falling
			if (aiState == 0)
			{
				Projectile.spriteDirection = Projectile.direction;
				Projectile.rotation = 0f;

				// Falling, take into account weird slope behaviour
				if (Projectile.velocity.Y != 0f && (Projectile.oldVelocity.Y != 0f && Projectile.velocity.Y != 0.4f))
				{
					if (Projectile.frame < fallFrame || Projectile.frame > fallFrame + fallFrameCount - 1) Projectile.frame = fallFrame;

					Projectile.frameCounter--;
					if (Projectile.frameCounter <= 0 || Projectile.frameCounter > fallFrameSpeed)
					{
						Projectile.frame++;
						Projectile.frameCounter = fallFrameSpeed;
						if (Projectile.frame > fallFrame + fallFrameCount - 1)
						{
							Projectile.frame = fallFrame;
						}
					}
				}
				else
				{
					if (Projectile.velocity.X == 0f)
					{
						if (Projectile.frame < idleFrame || Projectile.frame > idleFrame + idleFrameCount - 1) Projectile.frame = idleFrame;

						Projectile.frame = idleFrame;
					}
					else
					{
						if (Projectile.frame < runFrame || Projectile.frame > runFrame + runFrameCount - 1) Projectile.frame = runFrame;

						Projectile.frameCounter -= (int)Math.Min(
							512f,
							512f * Math.Abs(Projectile.velocity.X * runFrameSpeedMod));
						if (Projectile.frameCounter <= 0f)
						{
							Projectile.frame++;
							Projectile.frameCounter = 1024;
							if (Projectile.frame > runFrame + runFrameCount - 1)
							{
								Projectile.frame = runFrame;
							}
						}
					}
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
	}
}
