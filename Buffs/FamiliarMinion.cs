using ExpeditionsContent144.Projs.Familiars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Buffs
{
	class FamiliarMinion : ModBuff
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Familiar");
            Description.SetDefault("The familiar will fight for you");*/
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			PlayerExplorer modPlayer = player.GetModPlayer<PlayerExplorer>();
			int minionCount = 0;
			minionCount += player.ownedProjectileCounts[ModContent.ProjectileType<MinionFox>()];
			minionCount += player.ownedProjectileCounts[ModContent.ProjectileType<MinionChicken>()];
			minionCount += player.ownedProjectileCounts[ModContent.ProjectileType<MinionCat>()];
			if (minionCount > 0)
			{
				modPlayer.familiarMinion = true;
			}
			if (!modPlayer.familiarMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}
