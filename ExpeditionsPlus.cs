using Terraria.ModLoader;
using Expeditions;

namespace ExpeditionsPlus {
	public class ExpeditionAddons : Mod
	{
        public ExpeditionAddons()
        {
            Properties = new ModProperties()
            {
                Autoload = true
            };
        }

        public override void Load()
        {
            API.AddExpedition(this, new Tier0.MakingBase());
            API.AddExpedition(this, new Tier0.SurfaceExplorationKit());
            API.AddExpedition(this, new Tier0.BeaconOfPurity());
        }
	}
}