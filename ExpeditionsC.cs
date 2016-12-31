using Terraria.ModLoader;
using Expeditions;

namespace ExpeditionsContent {
	public class ExpeditionC : Mod
	{
        public ExpeditionC()
        {
            Properties = new ModProperties()
            {
                Autoload = true
            };
        }

        private static int _npcClerk;
        public static int npcClerk { get { return _npcClerk; } }

        public override void Load()
        {
            _npcClerk = NPCType("Clerk");

            API.AddExpedition(this, new Quests.Tier0.MakingBase());
            API.AddExpedition(this, new Quests.Tier0.BeaconOfPurity());

            API.AddExpedition(this, new Quests.Core.WelcomeQuest());
            API.AddExpedition(this, new Quests.Core.Tier1Quest());
            API.AddExpedition(this, new Quests.Core.Tier2Quest());
            API.AddExpedition(this, new Quests.Core.Tier3Quest());
            API.AddExpedition(this, new Quests.Core.Tier4Quest());
            API.AddExpedition(this, new Quests.Core.HardModeQuest());
            API.AddExpedition(this, new Quests.Core.Tier5Quest());
            API.AddExpedition(this, new Quests.Core.Tier6Quest());
            API.AddExpedition(this, new Quests.Core.Tier7Quest());
            API.AddExpedition(this, new Quests.Core.Tier8Quest());
            API.AddExpedition(this, new Quests.Core.Tier9Quest());
            API.AddExpedition(this, new Quests.Core.Tier10Quest());
            API.AddExpedition(this, new Quests.Core.Tier11Quest());
        }
	}
}