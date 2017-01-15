using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

sealed public class Tile
{
    public int Cost { get; private set; }
    public AbstractPlayer Owner { get; private set; }
    private Roboticon installedRoboticon { get; set; }
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
        installedRoboticon = null;
    }

    /// <summary>
    /// Adds a roboticon to the tile
    /// </summary>
    /// <param name="newRoboticon">Roboticon to be added</param>
    public void AddRoboticon(Roboticon newRoboticon)
    {
        installedRoboticon = newRoboticon;
    }

    /// <summary>
    /// Removes the roboticon from the tile
    /// </summary>
    public void RemoveRoboticon()
    {
        installedRoboticon = null;
    }

    public void AddPlayer(Player player)
    {
        Owner = player;
    }

    /// <summary>
    /// Returns the Ore produced by the tile
    /// </summary>
    /// <returns>Ore</returns>
    public int ProduceOre()
    {
        return Ore;
    }

    /// <summary>
    /// Returns the Power produced by the tile
    /// </summary>
    /// <returns>Power</returns>
    public int ProducePower()
    {
        return Power;
    }

    public int Ore
    {
        get { return resources[ItemType.Ore]; }
    }

    public int Power
    {
        get { return resources[ItemType.Ore]; }
    }

}
