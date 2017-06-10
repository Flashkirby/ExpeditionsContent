using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Moonstone
{
    public class Moonstone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moonlit Gemstone");
            Tooltip.SetDefault("'Touched by the night sky'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(30, 8));
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.maxStack = 99;
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 30, 0);
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            int lightMult = Main.moonPhase - 4;
            lightMult = System.Math.Abs(lightMult);
            Lighting.AddLight(item.Center, 
                0.66f - 0.02f * lightMult, 
                0.62f + 0.05f * lightMult, 0.9f);
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Main.itemAnimations[item.type].Frame = Main.moonPhase;
            Main.itemAnimations[item.type].FrameCounter = 0;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Main.itemFrame[whoAmI] = Main.moonPhase;
            Main.itemFrameCounter[whoAmI] = 0;
        }
    }
}
