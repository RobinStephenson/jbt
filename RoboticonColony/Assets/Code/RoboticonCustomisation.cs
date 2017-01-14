using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticonCustomisation {

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
    /// Creates an customisation type for roboticons.
    /// </summary>
    /// <param name="SelectedName"> The name of the customisation. </param>
    /// <param name="SelectedBonusMultiplier"> The multiplier which the production will be boosted by. </param>
    /// <param name="SelectedPrerequisites"> The list of other customisations which must completed already before this customisation can be applied. </param>
    /// <param name="SelectedResource"> The type of resource which the customisation augments. </param>
    public RoboticonCustomisation(string selectedName, int bonusMult, List<RoboticonCustomisation> prereq, ItemType selectedResource)
    {
        if (selectedResource == ItemType.Roboticon)
        {
            throw new ArgumentException("Roboticon not valid production type");
        }

        Name = selectedName;
        BonusMultiplier = bonusMult;
        Prerequisites = prereq;
        ResourceType = selectedResource;
    }
    
}
