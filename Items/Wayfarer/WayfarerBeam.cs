using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Wayfarer
{
    public class WayfarerBeam : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.AquaScepter);
            item.name = "Wayfarer's Beam";
            item.toolTip = "Fires a concentrated beam";
            item.width = 46;
            item.height = 16;
            item.UseSound = SoundID.Item12;

            item.mana = 11;
            item.damage = 30;
            item.useAnimation = 20;
            item.useTime = 19;
            item.knockBack = 2f;
            item.shoot = mod.ProjectileType<Projs.WayBeam>();
            item.shootSpeed = 3f;

            item.rare = 2;
            item.value = Item.sellPrice(0, 1, 0, 0);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 2);
        }
    }
}