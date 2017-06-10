using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Wayfarer
{
    public class WayfarerSubArm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wayfarer's Subber");
            Tooltip.SetDefault("50% chance not to consume ammo\n"
                + "'Spray and pray'");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Minishark);
            item.width = 42;
            item.height = 24;

            item.UseSound = new LegacySoundStyle(42, 194);
            item.damage = 4;
            item.knockBack = 0.5f;
            item.useAnimation = 5;
            item.useTime = 5;
            item.shootSpeed += 2f;

            item.value = Item.sellPrice(0, 1, 0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            speedX += Main.rand.NextFloatDirection() * 2f;
            speedY += Main.rand.NextFloatDirection() * 2f;
            return true;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextBool();
        }

        public override void HoldItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                player.itemRotation += Main.rand.NextFloatDirection() * 0.13f;
            }
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 3);
        }
    }
}