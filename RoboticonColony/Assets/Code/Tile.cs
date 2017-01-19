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
        owner = null;
        installedRoboticon = null;
    }

    public void AddPlayer(Player player)
    {
        owner = player;
    }

    /// <summary>
    /// Returns the Ore produced by the tile
    /// </summary>
    /// <returns>Ore</returns>
    public int ProduceOre()
    {
        //Will apply roboticon multiplier later on
        return Ore;
    }

    /// <summary>
    /// Returns the Power produced by the tile
    /// </summary>
    /// <returns>Power</returns>
    public int ProducePower()
    {
        //Will apply roboticon multiplier later on
        return Power;
    }

    public int Ore
    {
        get { return resources[ItemType.Ore]; }
    }

    public int Power
    {
        get { return resources[ItemType.Power]; }
    }

}
