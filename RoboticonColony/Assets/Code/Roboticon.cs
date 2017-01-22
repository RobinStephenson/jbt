using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Roboticon. Holds all information about the roboticon and facilitates upgrading said roboticon. 
/// </summary>
public class Roboticon
{
    public Tile CurrentTile { get; private set; }

    public List<RoboticonCustomisation> InstalledCustomisations;

    public Roboticon(Tile selectedTile)
    {
        CurrentTile = selectedTile;
    }

    /// <summary>
    /// Add the given customisation to this roboticon
    /// </summary>
    /// <param name="customisation">the customisation to apply</param>
    /// <exception cref="ArgumentException">The prerequisite customisations are not installed</exception>
    public void AddCustomisation(RoboticonCustomisation customisation)
    {
        // check customisations prerequisites are installed
        foreach (RoboticonCustomisation prerequisite in customisation.Prerequisites)
        {
            if (!InstalledCustomisations.Contains(prerequisite))
            {
                throw new ArgumentException("Prerequisite customisations not installed");
            }
        }
        InstalledCustomisations.Add(customisation);
    }

    /// <summary>
    /// get the best production multiplier of any of the installed customisations
    /// defaults to 0 if there are no installed customisations
    /// </summary>
    /// <param name="itemType">the ItemType the multiplier is to be applied too</param>
    /// <returns>the highest production multiplier</returns>
    public int ProductionMultiplier(ItemType itemType)
    {
        if (itemType == ItemType.Roboticon)
        {
            throw new ArgumentException("Roboticon is not valid");
        }

        int CurrentBestMultiplier = 0;
        foreach (RoboticonCustomisation customisation in InstalledCustomisations)
        {
            int NewMultiplier = customisation.GetMultiplier(itemType);
            if ( NewMultiplier > CurrentBestMultiplier)
            {
                CurrentBestMultiplier = NewMultiplier;
            }
        }
        return CurrentBestMultiplier;
    }

}
