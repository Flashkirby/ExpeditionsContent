using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Moonstone
{
    public class Moonstone : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Moonlit Gemstone";
            item.toolTip = "'Touched by the night sky'";
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

        public override DrawAnimation GetAnimation()
        {
            return new DrawAnimationMoonstone(8);
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Main.itemFrame[whoAmI] = Main.moonPhase;
            Main.itemFrameCounter[whoAmI] = 0;
            return true;
        }
    }

    internal class DrawAnimationMoonstone : DrawAnimation
    {
        public DrawAnimationMoonstone(int frameCount)
        {
            this.FrameCount = frameCount;
            this.TicksPerFrame = 2;
        }

        public override void Update()
        {
            this.Frame = Main.moonPhase;
            this.FrameCounter = 0;
        }

        public override Rectangle GetFrame(Texture2D texture)
        {
            return texture.Frame(1, this.FrameCount, 0, this.Frame);
        }
    }
}
