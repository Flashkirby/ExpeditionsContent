using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class PhotoCamera : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Photocam";
            item.toolTip = "Takes photos of creatures";
            item.toolTip = "'Say cheese!'";
            item.width = 34;
            item.height = 26;
            item.useAmmo = mod.ItemType<PhotoBlank>();
            item.UseSound = new LegacySoundStyle(SoundID.Camera, 0);

            item.useStyle = 4;
            item.useAnimation = 40;
            item.useTime = 40;
            item.useTurn = true;

            item.rare = 2;
            item.value = Item.buyPrice(0, 3, 0, 0);
        }

        public override void HoldItem(Player player)
        {
            if (player.itemAnimation > 0)
            {
                float brightness = (float)player.itemAnimation / player.itemAnimationMax;
                Lighting.AddLight(player.Top,
                    brightness * 1f,
                    brightness * 0.9f,
                    brightness * 0.8f);
            }
        }

        // This works because UI layer;
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem] == item)
            {
                Rectangle r = GetCameraFrame();

                // Draw camera frame
                    spriteBatch.Draw(
                    ExpeditionC.CameraFrameTexture,
                    r.Location.ToVector2() - Main.screenPosition,
                    Color.White
                    );
            }
        }

        public Rectangle GetCameraFrame()
        {
            return new Rectangle(
                Main.mouseX + (int)Main.screenPosition.X - frameWidth / 2,
                Main.mouseY + (int)Main.screenPosition.Y - frameHeight / 2,
                frameWidth,
                frameHeight);
        }

        public const int frameWidth = 180;
        public const int frameHeight = 120;
        public override bool UseItem(Player player)
        {
            if (player.whoAmI != Main.myPlayer) return true;

            bool canTakePicture = false;
            
            Rectangle cameraFrame = GetCameraFrame();

            NPC npc = Main.npc[0];
            float distance = 1000f;
            foreach (NPC n in Main.npc)
            {
                if (!n.active) continue;
                if (cameraFrame.Intersects(n.getRect()))
                {
                    float dist = n.Distance(cameraFrame.Center.ToVector2());
                    if (dist < distance)
                    {
                        distance = dist;
                        npc = n;
                        canTakePicture = true;
                    }
                }
            }

            if (!canTakePicture) return true;

            // Check for camera roll
            foreach(Item i in player.inventory)
            {
                if(i.ammo == item.useAmmo)
                {
                    i.stack--;
                    canTakePicture = true;
                }
            }
            if (!canTakePicture) return true;

            // Spawn the item
            int number = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType<Photo>(), npc.type, false, -1, false, false);

            // Send the item
            if (Main.netMode == 1)
            {
                NetMessage.SendData(21, -1, -1, "", number, 1f, 0f, 0f, 0, 0, 0);
            }
            return true;
        }
    }
}
