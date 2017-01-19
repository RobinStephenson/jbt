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
    public Tile(int cost, int ore, int power)
    {
        Cost = cost;
        resources = new Dictionary<ItemType, int>();
        resources[ItemType.Ore] = ore;
        resources[ItemType.Power] = power;
        Owner = null;
        InstalledRoboticon = null;
    }

    /// <summary>
    /// A function which will return an int representing the amount of the specified ItemType produced on the tile. 
    /// </summary>
    /// <param name="resourceType"></param>
    /// <returns></returns>
    public int Produce(ItemType resourceType)
    {
        //Will apply roboticon multiplier later on
        return resources[resourceType];
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
