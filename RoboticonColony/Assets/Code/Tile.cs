using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

sealed public class Tile
{
    public int Cost { get; private set; }
    //public Player OwnerID { get; private set; }
    //public Roboticon InstalledRoboticon { get; private set; }
    private Dictionary<ItemType, int> _Resources;

    /// <summary>
    /// Constructor for tile
    /// </summary> 
    /// <param name="cost">Cost of the tile</param>
    /// <param name="ore">Ore that the tile has</param>
    /// <param name="power">Power that the tile has</param>
    public Tile(int cost, int ore, int power)
    {
        Cost = cost;
        _Resources = new Dictionary<ItemType, int>();
        _Resources[ItemType.Ore] = ore;
        _Resources[ItemType.Power] = power;
        //_OwnerID = null;
        //_InstalledRoboticon = null;
    }

    /// <summary>
    /// Adds a roboticon to the tile
    /// </summary>
    /// <param name="newRoboticon">Roboticon to be added</param>
    //public void AddRoboticon(Roboticon newRoboticon)
    //{
    //    _InstalledRoboticon = newRoboticon;
    //}

    /// <summary>
    /// Removes the roboticon from the tile
    /// </summary>
    //public void RemoveRoboticon()
    //{
    //    _InstalledRoboticon = null;
    //}

    //public void AddPlayer(Player playerID)
    //{
    //   _OwnerID = playerID;
    //}

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
        get { return _Resources[ItemType.Ore]; }
    }

    public int Power
    {
        get { return _Resources[ItemType.Ore]; }
    }

    //public Roboticon InstalledRoboticon
    //{
    //    get { return _Roboticon; }
    //    set { _Roboticon = value; }
    //}

}
