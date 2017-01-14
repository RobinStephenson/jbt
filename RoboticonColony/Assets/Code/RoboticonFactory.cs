using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Roboticon Factory. Handles interactions between all roboticons and the player / market.  
/// </summary>
sealed class RoboticonFactory
{

    /// <summary>
    /// customisationsList is a list of all RoboticonCustomisations
    /// </summary>
    private List<RoboticonCustomisation> customisationsList;

    public RoboticonFactory()
    {
        customisationsList = null;
        roboticonList = null;

    }

    /// <summary>
    /// Creates a Roboticon instance that creates a non-customised roboticon in the specified tile.
    /// </summary>
    /// <param name="selectedTile"> The Tile that the roboticon is being placed on. </param>
    public void CreateRoboticon(Tile selectedTile)
    {
        roboticonList.Add(new Roboticon(selectedTile));
    }

    /// <summary>
    /// Creates an customisation type for roboticons.
    /// </summary>
    /// <param name="SelectedName"> The name of the customisation. </param>
    /// <param name="SelectedBonusMultiplier"> The multiplier which the production will be boosted by. </param>
    /// <param name="SelectedPrerequisites"> The list of other customisations which must completed already before this customisation can be applied. </param>
    /// <param name="SelectedResource"> The type of resource which the customisation augments. </param>
    /// <param name="reqPrice"> The required price of the customisation. </param>
    public void CreateCustomisation(string selectedName, int bonusMult, List<RoboticonCustomisation> prereq, ItemType selectedResource, int reqPrice)
    {
        customisationsList.Add(new RoboticonCustomisation(selectedName, bonusMult, prereq, selectedResource, reqPrice));
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
}