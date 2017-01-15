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
    /// Creates a Roboticon Factory.
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
    public Roboticon BuyRoboticon(Tile selectedTile, Inventory inv)
    {
        Roboticon newRoboticon = new Roboticon(selectedTile);
        RoboticonList.Add(newRoboticon);
        return newRoboticon;
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
    /// <param name="inv"> The amount of money </param>
    public void BuyCustomisation(Roboticon robo, RoboticonCustomisation custom, Inventory inv)
    {
        if (customisationRequirementsTest(robo.CurrentCustomisations, custom.Prerequisites))
        {

            if (curMoney > custom.Price)
            {
                robo.CurrentCustomisations.Add(custom);
                robo.SetBonusProduction(custom.ResourceType, custom.BonusMultiplier);
            }
            else
            {
                throw new ArgumentException("Not enough money in inventory to buy this customisation. ");
            }
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
    /// A Customisation for roboticons. Holds all information about 
    /// </summary>
    sealed public class RoboticonCustomisation
    {

        /// <summary>
        /// String that holds the name of the roboticon customisation.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Integer that holds the bonus multiplier of the roboticon customisation.
        /// </summary>
        public int BonusMultiplier { get; private set; }

        /// <summary>
        /// A variable of type ItemType that holds the resource type of the customisation.
        /// </summary>
        public ItemType ResourceType { get; private set; }

        /// <summary>
        /// A list that holds each Prerequisite of type RoboticonCustomisation.
        /// </summary>
        public List<RoboticonCustomisation> Prerequisites { get; private set; }

        /// <summary>
        /// An integer that holds the required money to purchase the customisation.
        /// </summary>
        public int Price { get; private set; }

        /// <summary>
        /// Creates an customisation type for roboticons.
        /// </summary>
        /// <param name="SelectedName"> The name of the customisation. </param>
        /// <param name="SelectedBonusMultiplier"> The multiplier which the production will be boosted by. </param>
        /// <param name="SelectedPrerequisites"> The list of other customisations which must completed already before this customisation can be applied. </param>
        /// <param name="SelectedResource"> The type of resource which the customisation augments. </param>
        /// <param name="reqPrice"> The required price of the customisation. </param>
        public RoboticonCustomisation(string selectedName, int bonusMult, List<RoboticonCustomisation> prereq, ItemType selectedResource, int reqPrice)
        {
            if (selectedResource == ItemType.Roboticon)
            {
                throw new ArgumentException("Roboticon not valid production type");
            }

            Name = selectedName;
            BonusMultiplier = bonusMult;
            Prerequisites = prereq;
            ResourceType = selectedResource;
            Price = reqPrice;
        }

    }

    /// <summary>
    /// A Roboticon. Holds all information about the roboticon and facilitates upgrading said roboticon. 
    /// </summary>
    sealed public class Roboticon
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
        public List<RoboticonFactory> CurrentCustomisations { get; private set; }

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
        /// Returns the Bonus production of the selected resource
        /// </summary>
        /// <param name="resourceType"> The resource type selected (enum ItemType) </param>
        /// <returns></returns>
        public int GetBonusProduction(ItemType resourceType)
        {
            if (resourceType == ItemType.Roboticon)
            {
                throw new ArgumentException("Roboticon not valid production type");
            }
            return bonusProduction[resourceType];
        }

        /// <summary>
        /// Sets the bonus multiplier for the selected resource type
        /// </summary>
        /// <param name="resourceType"> The resource type selected (enum ItemType) </param>
        /// <param name="newVal"> The new value the bonus multiplier should be. </param>
        /// <returns></returns>
        public void SetBonusProduction(ItemType resourceType, int newVal)
        {
            if (resourceType == ItemType.Roboticon)
            {
                throw new ArgumentException("Roboticon not valid production type");
            }

            bonusProduction[resourceType] = newVal;
        }

    }


}