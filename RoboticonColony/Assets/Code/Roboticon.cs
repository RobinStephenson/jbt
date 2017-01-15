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
    private Dictionary<ItemType, int> bonusProduction;

    /// <summary>
    /// CurrentCustomisations is a list of RoboticonCustomisations which the roboticon currently has applied. 
    /// </summary>
    public List<RoboticonCustomisation> CurrentCustomisations { get; private set; }

    /// <summary>
    /// Create an Roboticon instance with no CurrentTile.
    /// </summary>
    public Roboticon()
    {
        CurrentTile = null;
        bonusProduction = new Dictionary<ItemType, int>();
        CurrentCustomisations = new List<RoboticonCustomisation>();

        // Set Bonus Production to base roboticon stats
        bonusProduction[ItemType.Ore] = 1;
        bonusProduction[ItemType.Power] = 1;
    }

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
    /// Returns a dictionary of what the roboticon produces each turn.
    /// </summary>
    /// <returns>A dictionary of (ItemType, Int) containing the amount of each resource produced each turn. </ItemType></returns>
    public Dictionary<ItemType, int> Production()
    {
        Dictionary<ItemType, int> produced = new Dictionary<ItemType, int>();
        if (CurrentTile = null)
        {
            return produced;
        }
        else
        {
            Dictionary<ItemType, int> produced = new Dictionary<ItemType, int>();

            produced[ItemType.Ore] = CurrentTile.Ore * bonusProduction[ItemType.Ore];
            produced[ItemType.Power] = CurrentTile.Ore * bonusProduction[ItemType.Power];
            return produced;
        }
        
    }

    /// <summary>
    /// If the customisation's prerequisites are met the customisation will be applied to the selected roboticon. 
    /// </summary>
    /// <param name="customisation"> Reference to the Customisation that should be applied to the Roboticon</param>
    public void addCustomisation(RoboticonCustomisation customisation)
    {
        if (customisationRequirementsTest(CurrentCustomisations, customisation.Prerequisites))
        {
            CurrentCustomisations.Add(customisation);
            bonusProduction[customisation.ResourceType] = customisation.BonusMultiplier;
            

        }
        else
        {
            throw new ArgumentException("Roboticon doesn't meet the requirements for the specified customisation. ");
        }

    }

    /// <summary>
    /// Tests to ensure that, within the compled customisations list, every item of prerequisites is present.
    /// </summary>
    /// <param name="completedCustomisations"> List of roboticon customisations already applied to the roboticon</param>
    /// <param name="prerequisites">List of roboticon customisations required to have already been applied to the roboticon</param>
    /// <returns> If within the compled customisations list, every item of prerequisites is present, return True, else return False</returns>
    private bool customisationRequirementsTest(List<RoboticonCustomisation> completedCustomisations, List<RoboticonCustomisation> prerequisites)
    {
        // If the customisation has Prerequisites then...
        if (prerequisites.Count > 0)
        {
            foreach (RoboticonCustomisation currentCustomisation in prerequisites)
            {
                if (!completedCustomisations.Contains(currentCustomisation))
                {
                    // if Completed Customisations does not contains one Prerequisites then requirements are not met
                    return false;
                }
            }
            // if Completed Customisations contains all Prerequisites then requirements are met
            return true;
        }
        // else (Customisation has no Prerequisites)
        else
        {
            // As the customisation requires no prerequisites then the requirements are met
            return true;
        }
    }

    /// <summary>
    /// Move the Roboticon to the selectedTile
    /// </summary>
    /// <param name="selectedTile"> The tile the roboticon should be moved to</param>
    public void MoveRoboticon(Tile selectedTile)
    {
        CurrentTile = selectedTile;
    }

    /// <summary>
    /// Remove the Roboticon from the selected tile.
    /// </summary>
    public void RemoveRoboticon()
    {
        CurrentTile = null;
    }

    
}
