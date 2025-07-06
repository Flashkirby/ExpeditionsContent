using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Moonstone
{
	public class Moonstone : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Moonlit Gemstone");
            Tooltip.SetDefault("'Touched by the night sky'");*/
			Main.RegisterItemAnimation(Type, new DrawAnimationVertical(30, 8));
		}
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 22;
			Item.maxStack = 99;
			Item.rare = 1;
			Item.value = Item.sellPrice(0, 0, 30, 0);
		}

		public override void Update(ref float gravity, ref float maxFallSpeed)
		{
			int lightMult = Main.moonPhase - 4;
			lightMult = System.Math.Abs(lightMult);
			Lighting.AddLight(Item.Center,
				0.66f - 0.02f * lightMult,
				0.62f + 0.05f * lightMult, 0.9f);
		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Main.itemAnimations[Item.type].Frame = Main.moonPhase;
			Main.itemAnimations[Item.type].FrameCounter = 0;
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Main.itemFrame[whoAmI] = Main.moonPhase;
			Main.itemFrameCounter[whoAmI] = 0;
		}
	}
}
