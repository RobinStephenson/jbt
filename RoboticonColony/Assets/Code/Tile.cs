using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

sealed public class Tile
{
    public int Cost { get; private set; }
    public int OwnerID { get; private set; }

    private Dictionary<ItemType, int> _Resources;
    private int _ownerID;

    /// <summary>
    /// Constructor for tile with provided parameters
    /// </summary>
    /// <param name="cost">Cost of the tile</param>
    /// <param name="ore">Ore that the tile has</param>
    /// <param name="power">Power that the tile has</param>
    /// <param name="roboticon">Roboticon on the tile, null if no roboticon</param>
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
    /// <param name="cost">Cost of the tile</param>
    /// <param name="ore">Ore that the tile has</param>
    /// <param name="power">Power that the tile has</param>
    public Tile(int cost, int ore, int power)
    {
        Cost = cost;
        _Resources = new Dictionary<ItemType, int>();
        _Resources[ItemType.Ore] = ore;
        _Resources[ItemType.Power] = power;
        OwnerID = -1;
    }

    /// <summary>
    /// Adds a roboticon to the tile
    /// </summary>
    /// <param name="newRoboticon">Roboticon to be added</param>
    //public void AddRoboticon(Roboticon newRoboticon)
    //{
    //    _Resources[ItemType.Roboticon] = newRoboticon;
    //}

    /// <summary>
    /// Removes the roboticon from the tile
    /// </summary>
    //public void RemoveRoboticon()
    //{
    //    _Resources[ItemType.Roboticon] = null;
    //}

    /// <summary>
    /// Adds the resources on the tile to the player's inventory
    /// </summary>
    public void Produce(Inventory playerInv)
    {
        playerInv.AddItem(ItemType.Ore, Ore);
        playerInv.AddItem(ItemType.Power, Power);
    }

    public void update()
    {

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
    //    get { return _Resources[ItemType.Roboticon]; }
    //    set { _Resources[ItemType.Roboticon] = value; }
    //}

}
