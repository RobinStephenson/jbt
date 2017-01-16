using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Roboticon. Holds all information about the roboticon and facilitates upgrading said roboticon. 
/// </summary>
public class Roboticon
{

    /// <summary>
    /// The current Tile that the roboticon is assigned to. 
    /// </summary>
    public Tile CurrentTile { get; private set; }

    /// <summary>
    /// _bonusProduction is a dictionary which holds the int value (Total production of each recource) it produces.
    /// </summary>
    private Dictionary<ItemType, int> productionMultiplier;

    /// <summary>
    /// CurrentCustomisations is a RoboticonCustomisations which the roboticon currently has applied. 
    /// </summary>
    public RoboticonCustomisation CurrentCustomisation { get; private set; }

    /// <summary>
    /// Creates a Roboticon instance that creates a non-customised roboticon in the specified tile.
    /// </summary>
    /// <param name="selectedTile"> The Tile that the roboticon is being placed on. </param>
    public Roboticon(Tile selectedTile)
    {
        CurrentTile = selectedTile;
        productionMultiplier = new Dictionary<ItemType, int>();
        CurrentCustomisation = null;

        // Set Bonus Production to base roboticon stats
        productionMultiplier[ItemType.Ore] = 1;
        productionMultiplier[ItemType.Power] = 1;
    }

    /// <summary>
    /// Returns a dictionary of what the roboticon produces each turn.
    /// </summary>
    /// <returns>A dictionary of (ItemType, Int) containing the amount of each resource produced each turn. </ItemType></returns>
    public Dictionary<ItemType, int> Production()
    {
        Dictionary<ItemType, int> produced = new Dictionary<ItemType, int>();
        produced[ItemType.Ore] = CurrentTile.Ore * productionMultiplier[ItemType.Ore];
        produced[ItemType.Power] = CurrentTile.Ore * productionMultiplier[ItemType.Power];
        return produced;    
    }

    /// <summary>
    /// If the customisation's prerequisite is met the customisation will be applied to the selected roboticon. 
    /// </summary>
    /// <param name="customisation"> Reference to the Customisation that should be applied to the Roboticon</param>
    /// <exception cref="ArgumentException"> Thrown when customisation requirements are not met. </exception>
    public void AddCustomisation(RoboticonCustomisation customisation)
    {
        if (CurrentCustomisation == customisation.Prerequisites || customisation.Prerequisites == null)
        {
            CurrentCustomisation = customisation;
            productionMultiplier[customisation.ResourceType] = customisation.BonusMultiplier;
        }
        else
        {
            throw new ArgumentException("Roboticon doesn't meet the requirements for the specified customisation. ");
        }

    }

}
