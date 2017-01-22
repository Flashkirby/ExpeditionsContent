using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class PhotoCamPro : ModItem
    {
        public const int frameWidth = 180;
        public const int frameHeight = 120;
        public override void SetDefaults()
        {
            item.name = "Cannon-3000";
            item.toolTip = "Takes photos of creatures";
            item.toolTip2 = "Right click to zoom out";
            item.width = 34;
            item.height = 26;
            item.useAmmo = mod.ItemType<PhotoBlank>();
            item.UseSound = new LegacySoundStyle(SoundID.Camera, 0);

            item.useStyle = 4;
            item.useAnimation = 25;
            item.useTime = 25;
            item.autoReuse = true;

            item.rare = 2;
            item.value = Item.buyPrice(0, 3, 0, 0);
        }

        // Flashing effect
        public override void HoldItem(Player player)
        {
            player.scope = true;
            if (player.itemAnimation > 0)
            {
                float brightness = (float)player.itemAnimation / player.itemAnimationMax;
                Lighting.AddLight(player.Top + new Vector2(32 * player.direction, 0),
                    brightness * 1.5f,
                    brightness * 1.35f,
                    brightness * 1.2f);
            }
        }

        // This works because UI layer;
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            PhotoCamera.DrawCameraFrame(spriteBatch, item, frameWidth, frameHeight);
        }

        public override bool UseItem(Player player)
        {
            return PhotoCamera.TakePhoto(player, item, frameWidth, frameHeight);
        }
    }
}
