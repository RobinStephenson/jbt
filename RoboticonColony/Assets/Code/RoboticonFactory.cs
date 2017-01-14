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
    /// roboticonList is a list of all built Roboticons
    /// </summary>
    private List<Roboticon> roboticonList;

    public RoboticonFactory()
    {
        customisationsList = null;
        roboticonList = null;

    }

}