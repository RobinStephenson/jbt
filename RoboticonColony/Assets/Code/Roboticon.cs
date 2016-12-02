using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Roboticon. Holds all information about the roboticon and facilitates upgrading said roboticon. 
/// </summary>
sealed public class Roboticon : MonoBehaviour {

    private Tile Current_tile;

    /// <summary>
    /// BonusProduction is a dictionary which holds the int value (Total production of each recource) it produces.
    /// </summary>
    public Dictionary<ItemType, int> BonusProduction;

    /// <summary>
    /// Creates a Roboticon instance that creates a non-customised roboticon in the specified tile.
    /// </summary>
    /// <param name="selected_tile"> The Tile that the roboticon is being placed on. </param>
    public Roboticon(Tile selected_tile)
    {
        Current_tile = selected_tile;
        BonusProduction = new Dictionary<ItemType, int>();
        BonusProduction[ItemType.Ore] = 0;
        BonusProduction[ItemType.Power] = 0;
    }
   
}
