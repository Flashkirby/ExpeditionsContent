using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.QuestItems
{
    public class PhotoCamPro : ModItem
    {
        public const int frameWidth = 180;
        public const int frameHeight = 120;
        public const float maxFreeCapture = 450; // Max capture distance not relying on light
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hislops-3000");
            Tooltip.SetDefault("Takes photos of creatures\n"
                + "<right> to zoom out");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 24;
            item.useAmmo = mod.ItemType<PhotoBlank>();
            item.UseSound = new LegacySoundStyle(SoundID.Camera, 0);

            item.useStyle = 4;
            item.useAnimation = 25;
            item.useTime = 25;
            item.autoReuse = true;

            item.rare = 5;
            item.value = Item.buyPrice(0, 15, 0, 0);
        }

        // Flashing effect
        public override void HoldItem(Player player)
        {
            player.scope = true;
            if (player.itemAnimation > 0)
            {
                float brightness = (float)player.itemAnimation / player.itemAnimationMax;
                Lighting.AddLight(player.Top + new Vector2(32 * player.direction, 0),
                    brightness * 1.2f,
                    brightness * 1.35f,
                    brightness * 1.5f);
            }
        }

        // This works because UI layer;
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            PhotoCamera.DrawCameraFrame(spriteBatch, item, frameWidth, frameHeight);
        }

        public override bool UseItem(Player player)
        {
            return PhotoCamera.TakePhoto(player, item, frameWidth, frameHeight, maxFreeCapture);
        }
    }
}
