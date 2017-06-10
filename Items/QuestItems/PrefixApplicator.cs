using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.QuestItems
{
    public class PrefixApplicator : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prefix Applicator");
            Tooltip.SetDefault("<right> to use on next favourited accessory");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 24;
            item.consumable = true;
            item.rare = 0; // Shouldn't be at this value anyway because prefixes
            item.accessory = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
        }

        Item matchingAccessory = null;
        public override bool CanRightClick()
        {
            return matchingAccessory != null && item.prefix != 0;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (item.prefix == 0)
            {
                tooltips.Add(new TooltipLine(mod, "ApplyPrefixNull", "Apply to: No prefix to apply"));
                return;
            }

            // Search through the inventory for where I am, then
            // look for favourited items from that point, looping 
            // back to where my index is
            int myIndex = -1;

            // iterate first time to find me and items past me
            for (int i = 0; i < Main.LocalPlayer.inventory.Length; i++)
            {
                if (LookForMeAndMatch(ref myIndex, Main.LocalPlayer.inventory[i], i, tooltips))
                {
                    return;
                }
            }
            //iterate a second time and end at me
            for (int i = 0; i < myIndex; i++)
            {
                if (LookForMeAndMatch(ref myIndex, Main.LocalPlayer.inventory[i], i, tooltips))
                {
                    return;
                }
            }

            matchingAccessory = null;
            tooltips.Add(new TooltipLine(mod, "ApplyPrefixNone", "Apply to: No favourited accessory"));
        }

        private bool LookForMeAndMatch(ref int myIndex, Item invItem, int i, List<TooltipLine> tooltips)
        {
            if (myIndex == -1)
            {
                if (invItem.IsTheSameAs(this.item)) { myIndex = i; }
            }
            else
            {
                if (invItem.accessory &&
                    invItem.type != item.type &&
                    invItem.prefix != item.prefix &&
                    invItem.favorited)
                {
                    matchingAccessory = invItem;
                    tooltips.Add(new TooltipLine(mod, "ApplyPrefixAccessory", "Apply to: " + invItem.Name));
                    return true;
                }
            }
            return false;
        }

        bool consume;
        public override void RightClick(Player player)
        {
            if (matchingAccessory != null && item.prefix != 0)
            {
                // Apply the new prefix
                matchingAccessory.Prefix(item.prefix);

                // Make it obvious it's changed
                matchingAccessory.favorited = false;
                matchingAccessory.newAndShiny = true;

                consume = true;
            }
            else
            {
                consume = false;
            }
        }
        public override bool ConsumeItem(Player player)
        {
            return consume;
        }
    }
}
