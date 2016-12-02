using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboticonCustomisation : MonoBehaviour {

    private string Name;
    private int BonusMultiplier;
    private ItemType Resource;
    private List<RoboticonCustomisation> Prerequisites;

    /// <summary>
    /// Creates an customisation type for roboticons.
    /// </summary>
    /// <param name="name"> The name of the customisation. </param>
    /// <param name="bonus_multiplier"> The multiplier which the production will be boosted by. </param>
    /// <param name="prerequisites"> The list of other customisations which must completed already before this customisation can be applied. </param>
    /// <param name="resource"> The type of resource which the customisation augments. </param>
    public RoboticonCustomisation(string name, int bonus_multiplier, List<RoboticonCustomisation> prerequisites, ItemType resource)
    {
        Name = name;
        BonusMultiplier = bonus_multiplier;
        Prerequisites = prerequisites;
        Resource = resource;
    }




}
