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

    /// <summary>
    /// Tests to see if the roboticon has met the requriements of the customisation. 
    /// </summary>
    /// <param name="CompletedCustomisations"> A list of all the completed customisations on the roboticon. </param>
    /// <returns></returns>
    public bool RequirementsMetTest(List<RoboticonCustomisation> CompletedCustomisations)
    {
        // If the customisation has Prerequisites then...
        if (Prerequisites.Count > 0)
        {
            foreach (RoboticonCustomisation current_customisation in Prerequisites)
            {
                if (!CompletedCustomisations.Contains(current_customisation))
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


}
