using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Roboticon. Holds all information about the roboticon and facilitates upgrading said roboticon. 
/// </summary>
sealed public class Roboticon {

    /// <summary>
    /// The current Tile that the roboticon is assigned to. 
    /// </summary>
    public Tile CurrentTile { get; private set; }

    /// <summary>
    /// _bonusProduction is a dictionary which holds the int value (Total production of each recource) it produces.
    /// </summary>
    private Dictionary<ItemType, int> bonusProduction;

    /// <summary>
    /// CurrentCustomisations is a list of RoboticonCustomisations which the roboticon currently has applied. 
    /// </summary>
    public List<RoboticonCustomisation> CurrentCustomisations { get; private set; }

    /// <summary>
    /// Creates a Roboticon instance that creates a non-customised roboticon in the specified tile.
    /// </summary>
    /// <param name="selectedTile"> The Tile that the roboticon is being placed on. </param>
    public Roboticon(Tile selectedTile)
    {
        CurrentTile = selectedTile;
        bonusProduction = new Dictionary<ItemType, int>();
        CurrentCustomisations = new List<RoboticonCustomisation>();

        // Set Bonus Production to base roboticon stats
        bonusProduction[ItemType.Ore] = 1;
        bonusProduction[ItemType.Power] = 1;
    }

   

    /// <summary>
    /// Adds the selectd customisation of type RoboticonCustomisations to the Roboticon.
    /// Adds selected customisation's resource multiplier to the selected bonus 
    /// </summary>
    /// <param name="selectedCustomisation"> An instanciated class of RoboticonCustomisation</param>
    public void Customise(RoboticonCustomisation selectedCustomisation)
    {
        bonusProduction[selectedCustomisation.ResourceType] = selectedCustomisation.BonusMultiplier;
        CurrentCustomisations.Add(selectedCustomisation);
    }

    /// <summary>
    /// Returns the Bonus production of the selected resource
    /// </summary>
    /// <param name="resourceType"> The resource type selected (enum ItemType) </param>
    /// <returns></returns>
    public int GetBonusProduction(ItemType resourceType)
    {
        if (resourceType == ItemType.Roboticon){
            throw new ArgumentException("Roboticon not valid production type");
        }
        return bonusProduction[resourceType];
    }

}
