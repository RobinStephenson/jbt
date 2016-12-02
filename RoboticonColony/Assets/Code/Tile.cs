using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

sealed public class Tile
{
    private Dictionary<ItemType, int> _Resources;
    private int _cost;
    private int _ownerID;

    /// <summary>
    /// Constructor for tile with provided parameters
    /// </summary>
    /// <param name="resources">Resources that the tile has</param>
    /// <param name="roboticon">Roboticon on the tile, null if no roboticon</param>
    /// <param name="cost">Cost of the tile</param>
    /// <param name="owner">Owner of the tile</param>
    //public Tile(int cost, int ore, int power, Roboticon roboticon, Player owner)
    //{
    //    _Resources = new Dictionary<ItemType, int>();
    //    _Resources[ItemType.Ore] = ore;
    //    _Resources[ItemType.Power] = power;
    //    _Resources[ItemType.Roboticon] = roboticon;
    //    _cost = cost;
    //    _ownerID = owner.PlayerID;
    //}

    /// <summary>
    /// Constructor for tile with no owner or roboticon
    /// </summary>
    /// <param name="resource">Resources that the tile has</param>
    /// <param name="cost">Cost of the tile</param>
    public Tile(int cost, int ore, int power, int roboticon)
    {
        _Resources = new Dictionary<ItemType, int>();
        _Resources[ItemType.Ore] = ore;
        _Resources[ItemType.Power] = power;
        _Resources[ItemType.Roboticon] = roboticon;
        _cost = cost;
        _ownerID = -1;
    }

    /// <summary>
    /// Adds a roboticon to the tile
    /// </summary>
    /// <param name="newRoboticon">Roboticon to be added</param>
    //public void addRoboticon(Roboticon newRoboticon)
    //{
    //    _Resources[ItemType.Roboticon] = newRoboticon;
    //}

    /// <summary>
    /// Removes the roboticon from the tile
    /// </summary>
    //public void removeRoboticon()
    //{
    //    _Resources[ItemType.Roboticon] = null;
    //}

    /// <summary>
    /// Gets the cost of the tile
    /// </summary>
    /// <returns></returns>
    public int getCost()
    {
        return _cost;
    }

    //public void produce()
    //{

    //}

    //public void update()
    //{

    //}

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
    //    get { return _Resources[ItemType.Roboticon]; }
    //    set { _Resources[ItemType.Roboticon] = value; }
    //}

    public int Cost
    {
        get { return _cost; }
    }

    public int OwnerID
    {
        get { return _ownerID; }
        set { _ownerID = value; }
    }
}
