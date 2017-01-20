using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

sealed public class Tile
{
    public int Cost { get; private set; }
    private AbstractPlayer Owner { get; set; }
    private Roboticon InstalledRoboticon { get; set; }
    private Dictionary<ItemType, int> resources;

    /// <summary>
    /// Constructor for tile
    /// </summary> 
    /// <param name="cost">Cost of the tile</param>
    /// <param name="ore">Ore that the tile has</param>
    /// <param name="power">Power that the tile has</param>
    public Tile(int price, int ore, int power)
    {
        Price = price;
        resources = new Dictionary<ItemType, int>();
        resources[ItemType.Ore] = ore;
        resources[ItemType.Power] = power;
        Owner = null;
        InstalledRoboticon = null;
    }

    /// <summary>
    /// Returns a dictionary of what the roboticon produces each turn.
    /// </summary>
    /// <returns>A dictionary of (ItemType, Int) containing the amount of each resource produced each turn. </ItemType></returns>
    public Dictionary<ItemType, int> Production()
    {
        Dictionary<ItemType, int> produced = new Dictionary<ItemType, int>();
        if (!(InstalledRoboticon == null))
        {
            produced = InstalledRoboticon.ProductionMultiplier;
            return produced;
        }
        else
        {
            produced[ItemType.Ore] = 0;
            produced[ItemType.Power] = 0;
            return produced;
        }

    }
  
    /// <summary>
    /// install a roboticon on this tile
    /// </summary>
    /// <exception cref="RoboticonAlreadyInstalledException">A roboticon is already installled</exception>
    /// <param name="inventory">the players inventory</param>
    public void InstallRoboticon(Inventory inventory)
    {
        // should also set the installedTile on the roboticon to this one
        throw new RoboticonAlreadyInstalledException();
    }

     /// <summary>
    /// remove the roboticon installed on this tile
    /// </summary>
    /// <exception cref="InvalidOperationException">there is no roboticon installed</exception>
    /// <returns>the removed roboticon</returns>
    public Roboticon RemoveRoboticon()
    {
        // should also set installed tile to null on the roboticon
        throw new InvalidOperationException("No Roboticon installed");
    }
  
    /// <summary>
    /// Returns the available ore on the tile
    /// </summary>
    public int Ore
    {
        get { return resources[ItemType.Ore]; }
    }

    /// <summary>
    /// Returns the available power on the tile
    /// </summary>
    public int Power
    {
        get { return resources[ItemType.Power]; }
    }

}