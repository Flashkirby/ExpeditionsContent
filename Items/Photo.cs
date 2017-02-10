using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ExpeditionsContent.Items
{
    public class Photo : ModItem
    {
        public const int brokenPrefix = 40;
        public const int ignorantPrefix = 30;
        public string npcName = "";
        public string npcMod = "";
        private Texture2D npcTexture = null;
        public Texture2D NpcTexture
        {
            get
            {
                if (npcTexture == null)
                {
                    // Don't try setting up if the photo is not normal
                    if (item.prefix == 0)
                    {
                        try
                        {
                            if (Main.netMode != 2)
                            {
                                npcTexture = Main.npcTexture[item.stack];
                            }
                        }
                        catch
                        {
                            // NPC at this id doesn't exist
                            npcTexture = Main.magicPixel;
                        }
                    }
                    // Set photo info
                    SetNameAndMod();
                }

                // Give a magic pixel until mod photo can be resolved
                if (item.prefix == ignorantPrefix) return Main.magicPixel;

                return npcTexture;
            }
        }

        public override void SetDefaults()
        {
            item.name = "Photo Film";
            item.toolTip = "Used in conjuction with a Photocam";
            item.toolTip2 = "Right click to clear the image";
            item.width = 28;
            item.height = 28;
            item.rare = 0;
            item.value = Item.buyPrice(0, 0, 3, 0);

            npcName = "";
            npcMod = "";
            npcTexture = null;
        }
        public override TagCompound Save()
        {
            return new TagCompound
            {
                { "npcName", npcName },
                { "npcMod", npcMod }
            };
        }
        public override void Load(TagCompound tag)
        {
            npcName = tag.GetString("npcName");
            npcMod = tag.GetString("npcMod");
            npcTexture = null;

            // Set the photo to the item stack of this NPC
            // In case the mod order shuffled around or something
            Mod loadMod = ModLoader.GetMod(npcMod);
            if (loadMod != null)
            {
                // Mod found!
                item.prefix = ignorantPrefix; 
                //ignorant lmao, just so the other method can assign it
            }
            else
            {
                if (npcMod.Equals("VANILLA"))
                {
                    // Vanilla NPC so yeah
                    NPC npc = GenerateNPC();
                    item.stack = npc.type;
                }
                // NPC is unloaded
                else
                {
                    item.prefix = brokenPrefix;
                }
            }
        }

        /// <summary>
        /// Generate a default npc object
        /// </summary>
        /// <returns></returns>
        private NPC GenerateNPC()
        {
            try
            {
                NPC n = new NPC();
                n.SetDefaults(item.stack);
                if (n.modNPC != null) n.modNPC.SetDefaults();
                if (n.townNPC)
                {
                    // Get the actual town NPC if they exist already
                    int whoAmI = NPC.FindFirstNPC(n.type);
                    if (whoAmI >= 0)
                    {
                        return Main.npc[whoAmI];
                    }
                }

                return n;
            }
            catch (Exception e)
            {
                // Main.NewText("Gen " + item.stack + ": " + e.ToString());
            }
            return null;
        }
        /// <summary>
        /// Generate a
        /// </summary>
        public void SetNameAndMod()
        {
            if(item.prefix == ignorantPrefix)
            {
                // This is a 1 stack item that got loaded via a mod
                // So we have to find where it is and re-set it.
                item.prefix = 0;
                Mod loadMod = ModLoader.GetMod(npcMod);
                item.stack = loadMod.NPCType(npcName);
            }

            // Get NPC from the stack
            NPC npc = GenerateNPC();

            // Early stop, if this NPC doesn't exist or photo is broken
            if (npc == null || item.prefix == brokenPrefix)
            {
                item.name = "Photo"; //With 'Damaged' prefix
                item.stack = 1;
                item.prefix = brokenPrefix;
                ;
                if (npcMod != "")
                {
                    item.toolTip = "The image is clouded beyond recognition";
                    item.toolTip2 = "Mod: " + npcMod;
                }
                else
                {
                    item.toolTip = "The image is damaged beyond repair...";
                }
                return;
            }

            // Save the name of the NPC
            npcName = npc.name;
            npcMod = "VANILLA";
            if (npc.modNPC != null) // Non-vanilla
            {
                npcName = npc.modNPC.GetType().Name; // Use mods
                npcMod = npc.modNPC.mod.Name;
            }

            // Set photo info
            if (npc.townNPC)
            { item.name = "Photo of " + npc.displayName + ", no."; }
            else
            { item.name = "Photo of " + npc.name + ", no."; }
            item.toolTip = item.toolTip;
            item.toolTip2 = "";

            // Set stack
            item.maxStack = item.stack;

            // Set photo rarity
            item.rare = 0;
            if (npc.defense >= 16) item.rare = 4;
            if (npc.boss || npc.townNPC)
            {
                item.rare++;
            }
        }

        #region Draw Photo
        // Going to use double size, since scale is half sized to fit NPC
        public const int viewPortWidth = 16 * 2;
        public const int viewPortHeight = 24 * 2;
        private Rectangle CalculateSourceRectangle()
        {
            // Make rectangle of first frame and get the centre point
            int frames = Main.npcFrameCount[item.stack];
            Rectangle rect = new Rectangle(
                0, 0, NpcTexture.Width,
                NpcTexture.Height / frames);
            Point center = new Point(rect.Width / 2, rect.Height / 2);

            // Offset the frame by the width/height
            int width = Math.Min(viewPortWidth, rect.Width);
            int height = Math.Min(viewPortHeight, rect.Height);
            rect.Location = new Point(
                 center.X - width / 2,
                center.Y - height / 2);

            //Set size of source rectangle
            rect.Width = width;
            rect.Height = height;
            return rect;
        }

        // Photo Clearing, actually to prevent stack fiddling
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            item.SetDefaults();
            player.QuickSpawnItem(mod.ItemType<PhotoBlank>());
        }

        // Draw
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (NpcTexture == null) return;

            Rectangle rect = CalculateSourceRectangle();

            spriteBatch.Draw(
                NpcTexture,
                position + new Vector2(6 * scale, 2 * scale),
                rect,
                drawColor,
                0f,
                origin,
                scale / 2,
                SpriteEffects.None, 0);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (NpcTexture == null) return;

            Rectangle rect = CalculateSourceRectangle();

            spriteBatch.Draw(
                NpcTexture,
                item.Center - Main.screenPosition,
                rect,
                lightColor,
                rotation,
                item.Size / 2 + new Vector2(2, 6),
                scale / 2,
                SpriteEffects.None, 0);
        }
        #endregion
    }
}

/*
/// <summary>
/// Checks if the photo is ok, or does it need setting
/// </summary>
private void CheckPhoto()
{
    // Convert the item stack to the NPC
    if (npcTexture == null && item.name != "Faded Photo")
    {
        //Main.NewText("set photo");
        SetPhotoFromStack();
    }
}

public override void UpdateInventory(Player player)
{
    if (Main.netMode == 0)
    {
        item.toolTip = "Right click to clear the image";
    }
    else
    {
        item.toolTip = @"Right click to clear the image
Do not store in chests, use a safe/piggybank";
    }
}

/// <summary>
/// Generate a default npc object
/// </summary>
/// <returns></returns>
private NPC GenerateNPC()
{
    try
    {
        NPC n = new NPC();
        n.SetDefaults(item.stack);
        if (n.modNPC != null) n.modNPC.SetDefaults();
        if (n.townNPC)
        {
            int whoAmI = NPC.FindFirstNPC(n.type);
            if(whoAmI >= 0)
            {
                return Main.npc[whoAmI];
            }
        }

        return n;
    }
    catch (Exception e)
    {
        // Main.NewText("Gen " + item.stack + ": " + e.ToString());
    }
    return null;
}

/// <summary>
/// Generate the photo from the item.stack (aka npcType)
/// </summary>
private void SetPhotoFromStack()
{
    // Assign NPC to photo
    try
    {
        NPC n = GenerateNPC();

        SetPhoto(n);
    }
    catch (Exception e) { Main.NewText("Ex: " + e.ToString()); }
}

// Photo Clearing, actually to prevent stack fiddling
public override bool CanRightClick()
{
    return true;
}
public override void RightClick(Player player)
{
    item.SetDefaults(mod.ItemType<PhotoBlank>());
    item.stack = 2;
}

/// <summary>
/// Set the photo based on the NPC given
/// </summary>
/// <param name="npc"></param>
public void SetPhoto(NPC npc)
{
    npcName = npc.name; // Here's hoping these remain static... and indicative
    npcDisplayName = "";
    npcMod = "";
    npcTexture = null;
    if (npc.modNPC != null)
    {
        npcName = npc.modNPC.GetType().Name;
        npcMod = npc.modNPC.mod.Name;
    }
    if (!npcName.Equals(npc.displayName))
    {
        npcDisplayName = npc.displayName;
    }
    //Main.NewText("NPC " + npc.type + " captured: " + npcName + "(" + npcDisplayName + ") from " + npcMod);
    SetValues();
}
/// <summary>
///  Set up the values for the item
/// </summary>
private void SetValues()
{
    try
    {
        NPC n = GenerateNPC();
        if (npcMod != "")
        {
            // Try setting to the current index of this creature
            item.stack = ModLoader.GetMod(npcMod).NPCType(npcName); // catch if null
        }
        else
        {
            item.stack = n.type;
        }

        if(Main.netMode != 2)  npcTexture = Main.npcTexture[item.stack]; //catch if out of bounds

        if (npcDisplayName != "")
        {
            item.name = "Photo of " + npcDisplayName + ", no.";
            item.toolTip2 = "'Also known as " + npcName + "'";
        }
        else
        {
            item.name = "Photo of " + npcName + ", no.";
            item.toolTip2 = "";
        }

        if(n.boss || n.townNPC)
        {
            item.rare = 1;
        }
        else
        {
            item.rare = 0;
        }
    }
    catch
    {
        item.name = "Faded Photo";
        item.toolTip2 = "'Almost looks like... " + npcName + ", from " + npcMod + "'";
    }
    item.maxStack = item.stack;

    if (npcName == "")
    {
        item.stack = 1;
        item.SetDefaults(mod.ItemType<PhotoBlank>());
    }
}

//public override bool CanRightClick() { return true; item.maxStack = 2; }
//public override void RightClick(Player player)
//{
//    foreach(NPC n in Main.npc)
//    {
//        if (!n.active) continue;
//        if(!n.friendly)
//        {
//            SetPhoto(n);
//            break;
//        }
//    }
//}
//public override bool ConsumeItem(Player player)  {return false; }

*/
