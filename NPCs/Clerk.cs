﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Expeditions;

namespace ExpeditionsContent.NPCs
{
    /// <summary>
    /// The sole executive in charge of the Ministry of Cartography and 
    /// Taxonomy's foreign branch. Her employers rarely actually 
    /// communicate, but they send plenty of shipments over everytime a 
    /// new report rolls in, which she is more than happy to divulge.
    /// 
    /// LIKES:
    /// Discovering and documenting new things.
    /// Uses magic gratuitously in everyday life.
    /// Books.
    /// 
    /// DISLIKES:
    /// Being stuck at the bottom of the corporate ladder.
    /// Rain.
    /// Not having coffee.
    /// 
    /// </summary>    
	[AutoloadHead]
    class Clerk : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Clerk");
            Main.npcFrameCount[npc.type] = 21;
            NPCID.Sets.ExtraTextureCount[npc.type] = 1;
            NPCID.Sets.ExtraFramesCount[npc.type] = 7; // Extra frames after walk animation
            NPCID.Sets.AttackFrameCount[npc.type] = 2; // Attack frames at the end
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.MagicAuraColor[npc.type] = new Color(238, 82, 255);
            NPCID.Sets.AttackType[npc.type] = 2;
            NPCID.Sets.AttackTime[npc.type] = 30; // time to execute 1 attack
            NPCID.Sets.AttackAverageChance[npc.type] = 40; // 1/chance to attack per frame
            NPCID.Sets.HatOffsetY[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 40;
            npc.townNPC = true;
            npc.friendly = true;

            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;

            animationType = NPCID.Stylist;
        }
        public override string[] AltTextures
        {
            get
            {
                return new string[] { "ExpeditionsContent/NPCs/Clerk_Party" };
            }
        }
        public override bool Autoload(ref string name)
        {
            name = "Clerk";
            return mod.Properties.Autoload;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life > 0)
            {
                // Not Dead
                for (int i = 0; i < damage / npc.lifeMax * 100.0; i++)
                {
                    Dust.NewDust(npc.position, npc.height, npc.width,
                        DustID.Blood, hitDirection, -1f);
                }
            }
            else
            {
                // Probably Dead
                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust(npc.position, npc.height, npc.width,
                        DustID.Blood, hitDirection * 2.5f, -2.5f);
                }
                // Spawn gores á la vanilla
                int goreHead = mod.GetGoreSlot("Gores/Clerk1");
                int goreArms = mod.GetGoreSlot("Gores/Clerk2");
                int goreLegs = mod.GetGoreSlot("Gores/Clerk3");
                Gore.NewGore(npc.position, npc.velocity, goreHead, 1f);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 20f), npc.velocity, goreArms, 1f);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 20f), npc.velocity, goreArms, 1f);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 34f), npc.velocity, goreLegs, 1f);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 34f), npc.velocity, goreLegs, 1f);
            }
        }

        #region Spawning

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return WorldExplorer.savedClerk;
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return true;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(9))
            {
                case 0:
                    return "Nastasia"; //Super Paper Mario
                case 1:
                    return "Jolene"; //Paper Mario TTYD
                case 2:
                    return "Susan"; //Planet Robobot
                case 3:
                    return "Rachel"; //Hotel Dusk: Room 215
                case 4:
                    return "Eva"; //Grim Fandango
                case 5:
                    return "Carol"; //Dilbert
                case 6:
                    return "Jen"; //IT Crowd
                case 7:
                    return "Donna"; //Dr. Who and Suits
                default:
                    return "Isabelle"; //Animal Crossing
            }
        }

        #endregion

        #region AI Behaviours

        private bool danger = false;
        private float wasSittingTimer = 0f;
        public override void PostAI()
        {
            Player player = Main.player[Main.myPlayer];
            // You 'saved' me!
            WorldExplorer.savedClerk = true;

            danger = CheckDangered();

            // Overwrite getting off chair due to type != 15
            StayOnChairOverwriteVanilla();

            // Track if leaving npc whilst window is open
            bool anotherNPC = player.talkNPC > 0 && Main.npc[player.talkNPC].type == npc.type;
            if (Expeditions.ExpeditionUI.viewMode == Expeditions.ExpeditionUI.viewMode_NPC && Expeditions.ExpeditionUI.visible &&
                (player.talkNPC != npc.whoAmI && !anotherNPC)
                )
            {
                Expeditions.Expeditions.CloseExpeditionMenu();
            }

            SitAtBountyBoard();
        }

        private bool CheckDangered()
        {
            bool danger = false;
            float num389 = (float)NPCID.Sets.DangerDetectRange[npc.type];
            for (int num395 = 0; num395 < 200; num395++)
            {
                bool? flag45 = NPCLoader.CanHitNPC(Main.npc[num395], npc);
                if (!flag45.HasValue || flag45.Value)
                {
                    bool flag46 = flag45.HasValue && flag45.Value;
                    if (Main.npc[num395].active && !Main.npc[num395].friendly && Main.npc[num395].damage > 0 && Main.npc[num395].Distance(npc.Center) < num389 && (!NPCID.Sets.Skeletons.Contains(Main.npc[num395].netID) | flag46))
                    {
                        danger = true;
                        break;
                    }
                }
            }

            return danger;
        }

        private void StayOnChairOverwriteVanilla()
        {

            // If standing around when I could still be sitting
            if (npc.ai[0] == 0f && !danger && wasSittingTimer > 0)
            {
                Point point = npc.Center.ToTileCoordinates();
                Tile tile = Main.tile[point.X, point.Y];
                if (tile.type == mod.TileType("BountyBoard"))
                {
                    TakeSeat(point, tile);
                    npc.ai[1] = 2; //normally sit around all day
                }
                else
                {
                    wasSittingTimer = 0f;
                }
            }
            else
            {
                wasSittingTimer = 0f;
            }
        }
        private void SitAtBountyBoard()
        {
            // Sit down on expedition boards
            if (npc.ai[0] == 1f && !danger && npc.velocity.Y == 0f && Main.dayTime)
            {
                // Get my tile
                Point point2 = npc.Center.ToTileCoordinates();
                bool flag61 = WorldGen.InWorld(point2.X, point2.Y, 1);

                // Check first if anyone else is sitting here
                if (flag61)
                {
                    for (int num471 = 0; num471 < 200; num471++)
                    {
                        if (Main.npc[num471].active && Main.npc[num471].aiStyle == 7 && Main.npc[num471].townNPC && Main.npc[num471].ai[0] == 5f)
                        {
                            Point a = Main.npc[num471].Center.ToTileCoordinates();
                            if (a.Equals(point2))
                            {
                                flag61 = false;
                                break;
                            }
                        }
                    }
                }
                if (flag61)
                {
                    Tile tile2 = Main.tile[point2.X, point2.Y];
                    flag61 = (tile2.type == mod.TileType("BountyBoard"));
                    // disregard parts with no seat
                    if (tile2.frameY <= 52)
                    {
                        if (tile2.frameX < 54) flag61 = false;
                    }
                    else
                    {
                        if (tile2.frameX >= 16) flag61 = false;
                    }
                    if (flag61)
                    {
                        TakeSeat(point2, tile2);
                        npc.ai[1] = (float)(900 + Main.rand.Next(10800));
                    }
                }
            }

            wasSittingTimer = (npc.ai[0] == 5f && Main.dayTime) ? npc.ai[1] : 0;
        }
        private void TakeSeat(Point point2, Tile tile2)
        {
            npc.ai[0] = 5f;
            bool flag = true;
            foreach (Player p in Main.player)
            {
                if (p.talkNPC == npc.whoAmI)
                {
                    flag = false;
                }
            }
            if (flag) npc.direction = ((tile2.frameY <= 52) ? -1 : 1);
            npc.Bottom = new Vector2((float)(point2.X * 16 + 8 + 2 * npc.direction), (float)(point2.Y * 16 + 32));
            npc.velocity = Vector2.Zero;
            npc.localAI[3] = 0f;
            if (Main.netMode != 1)
            {
                npc.netUpdate = true;
            }
        }

        #endregion
        #region AI Attacks

        private int getWeaponType()
        {
            if (Main.hardMode)
            { return 2; }
            else if (NPC.downedBoss1)
            { return 1; }
            return 0;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            switch (getWeaponType())
            {
                case 2:
                    damage = 40;
                    knockback = 5f;
                    break;
                case 1:
                    damage = 21;
                    knockback = 5f;
                    break;
                default:
                    damage = 10;
                    knockback = 0f;
                    break;
            }
        }
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            switch (getWeaponType())
            {
                case 2:
                    cooldown = 32; //min cooldown from START of attack (no inc. attack time)
                    randExtraCooldown = 33;
                    break;
                case 1:
                    cooldown = 36; //min cooldown from START of attack (no inc. attack time)
                    randExtraCooldown = 26;
                    break;
                default:
                    cooldown = 40; //min cooldown from START of attack (no inc. attack time)
                    randExtraCooldown = 20;
                    break;
            }
        }
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            switch (getWeaponType())
            {
                case 2:
                    projType = ProjectileID.CrystalPulse;
                    attackDelay = 30; //how many frames before projectile spawns
                    if (npc.localAI[3] == attackDelay - 1) Main.PlaySound(2, npc.Center, 109);
                    break;
                case 1:
                    projType = ProjectileID.AmethystBolt; //ruby stats though
                    attackDelay = 30; //how many frames before projectile spawns
                    if (npc.localAI[3] == attackDelay - 1) Main.PlaySound(2, npc.Center, 43);
                    break;
                default:
                    projType = ProjectileID.Spark;
                    attackDelay = 30; //how many frames before projectile spawns
                    if (npc.localAI[3] == attackDelay - 1) Main.PlaySound(2, npc.Center, 8);
                    break;
            }
        }
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            gravityCorrection = 0f;
            switch (getWeaponType())
            {
                case 2:
                    multiplier = 13f;
                    randomOffset = 0.6f;
                    break;
                case 1:
                    multiplier = 9f;
                    randomOffset = 0.3f;
                    break;
                default:
                    multiplier = 9f;
                    randomOffset = 0.5f;
                    gravityCorrection = 1f;
                    break;
            }
        }
        public override void TownNPCAttackMagic(ref float auraLightMultiplier)
        {
            auraLightMultiplier = 0.5f;
        }

        #endregion

        #region Talk Shop

        public override string GetChat()
        {
            Expeditions.Expeditions.CloseExpeditionMenu(true); // Stop conflict caused by Bounty Book
            if (npc.homeless)
            {
                if (Main.bloodMoon)
                {
                    switch (Main.rand.Next(3))
                    {
                        case 1:
                            return "Hey what's the difference between me and zombies? I'm still alive - but not for much longer out here. Hahahah, get it? It's not funny. ";
                        case 2:
                            return "Gee, wouldn't it be great if SOMEBODY pulled their own weight and built some housing. ";
                        default:
                            return "I don't mean to sound overbearing... but COFFEE. NOW. ";
                    }
                }
                else
                {
                    switch (Main.rand.Next(3))
                    {
                        case 1:
                            return "Hmm... maybe a tent over here... ";
                        case 2:
                            return "Now, you wouldn't happen to know a realtor, would you? ";
                        default:
                            return "I could really do with a coffee right now. Or a water cooler. Or an office. ";
                    }
                }
            }

            List<string> speech = new List<string>();

            if (Terraria.GameContent.Events.BirthdayParty.PartyIsUp && Main.rand.Next(3) == 0)
            {
                return "Let's make this party one to remember! And also cake. Lots of cake. ";
            }

            string theTime = GetTheTime();
            string theMap = GetTheMap();
            if (Main.dayTime)
            {
                if (Main.raining) speech.Add("Watch out! There's water coming from the sky! Haha, just kidding. ");
                if (Main.slimeRain) speech.Add("Uhm yeah, I'm just going to stay indoors until the slimes are gone, thanks. ");

                speech.Add(theTime);
                speech.Add(theTime);
                speech.Add(theMap);

                if (Main.time < 16200.0)
                {
                    speech.Add("Good morning, d'you need to look at today's agenda? ");
                    speech.Add("I hate mondays. *Yawn*. ");
                    speech.Add("Did you know, 63% of all statistics are made up? ");
                    speech.Add("How do I post expeditions on every notice board at the same time? Magic. ");
                }
                else if (Main.time <= 37800.0)
                {
                    speech.Add("Good afternoon, how are your explorations faring? ");
                    speech.Add("Hmm. Need more stamp ink. Oh hello, what do you need today? ");
                    speech.Add("I drew a smiley face on my boss once. " +
                        (Main.rand.Next(2) == 0 ? "He" : "She") +
                        " was NOT happy. ");
                    speech.Add("How do you float a paperclip on water? Magic. ");
                }
                else
                {
                    speech.Add("Good evening, do you have any completed expeditions for me to sign off? ");
                    speech.Add("I tried using a magical self-writing quill once. All my notes ended up looking like tabloids. ");
                    speech.Add("Does the fact that everything seems to carry money strike you as weird? Or is it just me? ");
                    speech.Add("Why can I hold all these items? Magic. ");
                }
            }
            else
            {
                if (Main.bloodMoon)
                {
                    speech.Add("Ahem. I believe you have my stapler. ");
                    speech.Add("Sorry, you'll just have to wait. I am VERY busy, and this paperwork will not sort itself. ");
                    speech.Add("Did you know there's a blood moon outside? If ONLY I knew someone who could go and out and deal with it... ");
                    speech.Add("I'm stressed. I'm holding a sharpened quill. I'd stay out of my way if I were you. ");
                }
                else
                {
                    speech.Add(theTime);
                    speech.Add(theMap);
                    speech.Add(theMap);

                    speech.Add("I'll have you know I am unbeaten in waste paper basketball. ");
                    speech.Add("I'll have you know I excel at spreadsheets. ");
                    speech.Add("I'll have you know I was employee of the month once. ");
                    speech.Add("Sometimes I miss the urban sprawl. Less of everything trying to kill you - well most of the time. ");
                    speech.Add("What is 'sleep' anyways? ");
                    speech.Add("How do I stay up all night? Coffee. Uh... I mean magic. ");
                }
            }

            // End early
            if (Main.bloodMoon) return speech[Main.rand.Next(speech.Count)];

            // NPC extra dialogues, irrespective of time
            string name = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
            if (name != null) speech.Add(name + " seems like a really helpful guy, but I heard he has a notorious reputation for inappropriate door opening. ");

            name = NPC.GetFirstNPCNameOrNull(NPCID.GoblinTinkerer);
            if (name != null) speech.Add("Can you believe goblins don't know their east from west? " + name + " seems to be a bit more educated though.");

            name = NPC.GetFirstNPCNameOrNull(NPCID.TravellingMerchant);
            if (name != null) speech.Add(name + " tells me he's been to all sorts of places, only he never tells me where exactly. At least not without paying up. ");

            name = NPC.GetFirstNPCNameOrNull(NPCID.Wizard);
            if (name != null) speech.Add(name + " has been showing me some cool tricks, wanna to see? No? Ok. ");

            name = NPC.GetFirstNPCNameOrNull(NPCID.TaxCollector);
            if (name != null) speech.Add("I keep telling " + name + " that I don't handle finances, so he'll just have to settle for stationary. ");

            return speech[Main.rand.Next(speech.Count)];
        }

        public static string GetTheTime()
        {
            double fullTime = Main.time;
            string twelveHr = "AM";
            if (!Main.dayTime) fullTime += 54000.0;
            fullTime = (fullTime / 86400.0 * 24.0) - 19.75;
            if (fullTime < 0.0) fullTime += 24.0;
            if (fullTime >= 12.0) twelveHr = "PM";

            int minutes = (int)((fullTime - (int)fullTime) * 60.0);
            string divider = ":";
            if (minutes < 10) divider += "0";

            return "Have I got the time? Sure, it's " + string.Concat((int)fullTime, divider, minutes, " ", twelveHr) + ".";
        }
        public static string GetTheMap()
        {
            int completion = 0;
            int revealed = 0;
            int maxCounted = 0;
            string comment = ". Keep exploring!";

            int tileLeft = (int)Main.leftWorld / 16;
            int tileRight = (int)Main.rightWorld / 16;
            int tileTop = (int)Main.topWorld / 16;
            int tileBottom = (int)Main.bottomWorld / 16;
            // Minsize is 800x600
            int checkStepX = (int)(Main.rightWorld - Main.leftWorld) / 800;
            int checkStepY = (int)(Main.bottomWorld - Main.topWorld) / 600;
            /*
            Main.NewText(string.Concat(
                    tileLeft,":",tileRight,"=",tileTop,":",tileBottom," ",checkStepX,"-",checkStepY
                ));
                */
            for (int y = 1; y < checkStepY; y++)
            {
                int yTile = tileTop + tileBottom / checkStepY * y;
                for (int x = 1; x < checkStepX; x++)
                {
                    maxCounted++;
                    int xTile = tileLeft + tileRight / checkStepX * x;
                    if (Main.Map.IsRevealed(
                        xTile,
                        yTile))
                    {
                        revealed++;
                    }
                }
            }
            completion = (int)((100f * revealed) / maxCounted);

            comment = "Your map is " + completion + "% complete. ";
            if (completion == 100)
            { comment = "Wow! You've fully mapped out " + Main.worldName + ". You have my upmost respect! "; }
            else if (completion >= 95)
            { comment += "Nearly! "; }
            else if (completion >= 90)
            { comment += "So close! It's almost a perfect map... "; }
            else if (completion >= 80)
            { comment += "You're not far off now... "; }
            else if (completion >= 70)
            { comment += "Your efforts are greatly appreciated! "; }
            else if (completion >= 60)
            { comment += "Keep going! You've got this! "; }
            else if (completion >= 50)
            { comment += "You're over halfway there. "; }
            else if (completion >= 40)
            { comment += "Neat! From this point onwards it might get more difficult. "; }
            else if (completion >= 33) // A well-explored normal map (full surface, dungeon etc.)
            { comment += "Well done! You must know this place pretty well by now. "; }
            else if (completion >= 25)
            { comment += "You've cleared the first hurdle, but there's still ways to go. "; }
            else if (completion >= 18)
            { comment += Main.worldName + " still has more to offer. "; }
            else if (completion >= 11)
            { comment += "Keep exploring! "; }
            else if (completion >= 5)
            { comment += "There's still plenty left to explore. "; }
            else if (completion >= 1)
            { comment += "You should try mapping out the surface of " + Main.worldName + ". "; }
            else
            { comment = "You've mapped out " + completion + "% of " + Main.worldName + ". Well, everyone has to start somewhere right? "; }

            return comment;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Lang.inter[28].Value;
            button2 = "Expeditions";
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            else
            {
                Expeditions.Expeditions.OpenExpeditionMenu(ExpeditionUI.viewMode_NPC);
            }
        }

        private const bool ShowAll = true;
        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            // Sell book
            shop.item[nextSlot].SetDefaults(API.ItemIDExpeditionBook); nextSlot++;
            // Sell Camera and ammo
            shop.item[nextSlot].SetDefaults(mod.ItemType<Items.QuestItems.PhotoCamera>()); nextSlot++;
            if (API.FindExpedition<Quests.Clerk.ProCamSkill>(mod).completed)
            {
                API.AddShopItemVoucher(shop, ref nextSlot,
                    mod.ItemType<Items.QuestItems.PhotoCamPro>(), 2);
            }
            shop.item[nextSlot].SetDefaults(mod.ItemType<Items.QuestItems.PhotoBlank>()); nextSlot++;
            API.AddShopItemVoucher(shop, ref nextSlot,
                mod.ItemType<Items.Albums.CopyPrint>(), 1);

            // Sell telescope
            API.AddShopItemVoucher(shop, ref nextSlot, mod.ItemType<Items.QuestItems.Telescope>(), 1);
            // Sell boxes
            API.AddShopItemVoucher(shop, ref nextSlot, API.ItemIDRustedBox, 1);
            if (Main.hardMode)
            { API.AddShopItemVoucher(shop, ref nextSlot, API.ItemIDRelicBox, 2); }
            // Unlock Albums Moonstone set
            if (API.FindExpedition<Quests.Clerk.AlbumOmnibus3>(mod).completed)
            {
                API.AddShopItemVoucher(shop, ref nextSlot,
                    mod.ItemType<Items.Moonstone.LootBagMoonstone>(), 2);
            }

            // Heart Compass
            if (API.FindExpedition<Quests.Clerk.CrystalHeart>(mod).completed)
            {
                API.AddShopItemVoucher(shop, ref nextSlot, 
                    mod.ItemType<Items.QuestItems.HeartCompass>(), 1);
            }
            // Jungle Eyepiece
            if (API.FindExpedition<Quests.Clerk.FruitsOfLabour>(mod).completed)
            {
                API.AddShopItemVoucher(shop, ref nextSlot, mod.ItemType<Items.QuestItems.JungleEyepiece>(), 1);
            }
            // Loyalty Badge
            if (API.FindExpedition<Quests.Clerk.SecretSummon>(mod).completed ||
                API.FindExpedition<Quests.Clerk.SecretSummon2>(mod).completed)
            {
                API.AddShopItemVoucher(shop, ref nextSlot,
                    mod.ItemType<Items.QuestItems.LoyaltyBadge>(), 1);
            }

            // Basic Wayfarer
            if (API.FindExpedition<Quests.Clerk.WayfarerWeapons>(mod).completed)
            {
                API.AddShopItemVoucher(shop, ref nextSlot, 
                    mod.ItemType<Items.Wayfarer.WayfarerSword>(), 1);
                API.AddShopItemVoucher(shop, ref nextSlot, 
                    mod.ItemType<Items.Wayfarer.WayfarerPike>(), 1);
                API.AddShopItemVoucher(shop, ref nextSlot, 
                    mod.ItemType<Items.Wayfarer.WayfarerBow>(), 1);
                // Way Subber
                if (NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3)
                {
                    API.AddShopItemVoucher(shop, ref nextSlot,
                        mod.ItemType<Items.Wayfarer.WayfarerClaymore>(), 1);
                }
            }

            // Alternate method of aquiring guns
            if (API.FindExpedition<Quests.Clerk.WayfererGuns>(mod).completed)
            {
                if (!WorldGen.crimson)
                {
                    API.AddShopItemVoucher(shop, ref nextSlot, 
                        mod.ItemType<Items.Wayfarer.WayfarerCarbine>(), 1);
                }
                else
                {
                    API.AddShopItemVoucher(shop, ref nextSlot, 
                        mod.ItemType<Items.Wayfarer.WayfarerRepeater>(), 1);
                }
                // Way Subber
                if (NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3)
                {
                    API.AddShopItemVoucher(shop, ref nextSlot,
                        mod.ItemType<Items.Wayfarer.WayfarerSubArm>(), 1);
                }
                shop.item[nextSlot].SetDefaults(ItemID.MusketBall); nextSlot++;
            }

            // Way Book
            bool addBeam = false;
            if (API.FindExpedition<Quests.Clerk.SkysTheLimit>(mod).completed)
            {
                API.AddShopItemVoucher(shop, ref nextSlot, 
                    mod.ItemType<Items.Wayfarer.WayfarerBook>(), 1);
                addBeam = true;
            }

            // Way Staff
            if (API.FindExpedition<Quests.Clerk.RoseByAnyName>(mod).completed)
            {
                API.AddShopItemVoucher(shop, ref nextSlot,
                    mod.ItemType<Items.Wayfarer.WayfarerStaff>(), 1);
                addBeam = true;
            }

            // Way Beam
            if (NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3 && addBeam)
            {
                API.AddShopItemVoucher(shop, ref nextSlot,
                    mod.ItemType<Items.Wayfarer.WayfarerBeam>(), 1);
            }

            // Summon Staff
            if (API.FindExpedition<Quests.Clerk.SecretSummon2>(mod).completed)
            {
                API.AddShopItemVoucher(shop, ref nextSlot,
                    mod.ItemType<Items.Wayfarer.WayfarerSummon>(), 2);
            }
        }

        #endregion

    }
}
