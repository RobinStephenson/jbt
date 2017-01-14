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

    /// <summary>
    /// RoboticonList is a list of all the roboticons
    /// </summary>
    public List<Roboticon> RoboticonList { get; private set; }

    /// <summary>
    /// Creates a Roboticon Controller class.
    /// </summary>
    public RoboticonFactory()
    {
        customisationsList = null;
        RoboticonList = null;

        /// Temporary initialisation of customisations, may be done through reading in a file later. 
        createCustomisations("Ore 1", 2, null, ItemType.Ore, 10);
        createCustomisations("Power 1", 2, null, ItemType.Power, 10);
    }

    /// <summary>
    /// Creates a Roboticon instance that creates a non-customised roboticon in the specified tile.
    /// </summary>
    /// <param name="selectedTile"> The Tile that the roboticon is being placed on. </param>
    public void CreateRoboticon(Tile selectedTile)
    {
        RoboticonList.Add(new Roboticon(selectedTile));
    }

    /// <summary>
    /// Creates an customisation type for roboticons.
    /// </summary>
    /// <param name="selectedName"> The name of the customisation. </param>
    /// <param name="bonusMult"> The multiplier which the production will be boosted by. </param>
    /// <param name="prereq"> The list of other customisations which must completed already before this customisation can be applied. </param>
    /// <param name="selectedResource"> The type of resource which the customisation augments. </param>
    /// <param name="reqPrice"> The required price of the customisation. </param>
    private void createCustomisations(string selectedName, int bonusMult, List<RoboticonCustomisation> prereq, ItemType selectedResource, int reqPrice)
    {
        customisationsList.Add(new RoboticonCustomisation(selectedName, bonusMult, prereq, selectedResource, reqPrice));
    }


    /// <summary>
    /// If the customisation's prerequisites are met and the player has enough money, the customisation will be applied to the selected roboticon. 
    /// </summary>
    /// <param name="robo"> Reference to the Roboticon the customisation should be applied to</param>
    /// <param name="custom"> Reference to the Customisation that should be applied to the Roboticon</param>
    public void BuyCustomisation(Roboticon robo, RoboticonCustomisation custom, int curMoney)
    {
        if (customisationRequirementsTest(robo.CurrentCustomisations, custom.Prerequisites))
        {

            if (curMoney > custom.Price) {
                robo.CurrentCustomisations.Add(custom);
                robo.SetBonusProduction(custom.ResourceType, custom.BonusMultiplier);
            }
            else {
                throw new Exception("Not enough money in inventory to buy this customisation. ");
            }
        }
        else
        {
            throw new ArgumentException("Roboticon doesn't meet the requirements for the specified customisation. ");
        }

    }

    private bool customisationRequirementsTest(List<RoboticonCustomisation> completedCustom, List<RoboticonCustomisation> prereq)
    {
        // If the customisation has Prerequisites then...
        if (prereq.Count > 0)
        {
            foreach (RoboticonCustomisation currentCustomisation in prereq)
            {
                if (!completedCustom.Contains(currentCustomisation))
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