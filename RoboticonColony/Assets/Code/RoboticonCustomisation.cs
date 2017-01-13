using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticonCustomisation : MonoBehaviour {

    /// <summary>
    /// String that holds the name of the roboticon customisation.
    /// Also allows reference to itself to read, not write. 
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Integer that holds the bonus multiplier of the roboticon customisation.
    /// Also allows reference to itself to read, not write. 
    /// </summary>
    public int BonusMultiplier { get; private set; }

    /// <summary>
    /// A variable of type ItemType that holds the resource type of the customisation.
    /// Also allows reference to itself to read, not write. 
    /// </summary>
    public ItemType Resource { get; private set; }

    /// <summary>
    /// A list that holds each Prerequisite of type RoboticonCustomisation.
    /// Also allows reference to itself to read, not write. 
    /// </summary>
    public List<RoboticonCustomisation> Prerequisites { get; private set; }

    /// <summary>
    /// Creates an customisation type for roboticons.
    /// </summary>
    /// <param name="SelectedName"> The name of the customisation. </param>
    /// <param name="SelectedBonusMultiplier"> The multiplier which the production will be boosted by. </param>
    /// <param name="SelectedPrerequisites"> The list of other customisations which must completed already before this customisation can be applied. </param>
    /// <param name="SelectedResource"> The type of resource which the customisation augments. </param>
    public RoboticonCustomisation(string SelectedName, int SelectedBonusMultiplier, List<RoboticonCustomisation> SelectedPrerequisites, ItemType SelectedResource)
    {
        Name = SelectedName;
        BonusMultiplier = SelectedBonusMultiplier;
        Prerequisites = SelectedPrerequisites;
        Resource = SelectedResource;
    }
    
}
