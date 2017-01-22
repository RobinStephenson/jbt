using System;
using System.Collections.Generic;

sealed public class RoboticonCustomisation
{
    public string Name { get; private set; }

    /// <summary>
    /// Prerequisite customisations that a roboticon must have in order for this one to be applied
    /// </summary>
    public List<RoboticonCustomisation> Prerequisites { get; private set; }

    /// <summary>
    /// Resource -> Bonus production of that resource
    /// Bonus should be multiplied eg Bonus multiplier of 2 is 2 * production of that resource
    /// </summary>
    private Dictionary<ItemType, int> ProductionMultipliers;

    /// <summary>
    /// The cost of this customisation
    /// </summary>
    public int Price { get; private set; }

    public RoboticonCustomisation(string name, Dictionary<ItemType, int> bonuses, List<RoboticonCustomisation> prerequisites, int price)
    {
        Name = name;
        ProductionMultipliers = bonuses;
        Price = price;
    }

    /// <summary>
    /// Get the production multiplier for a given itemtype from this customisation
    /// </summary>
    /// <param name="itemType">the item type the multiplier is to be applied to</param>
    /// <returns>the multiplier</returns>
    public int GetMultiplier(ItemType itemType)
    {
        if (itemType == ItemType.Roboticon)
        {
            throw new ArgumentException("Roboticon is not valid");
        }
        return ProductionMultipliers[itemType];
    }
}