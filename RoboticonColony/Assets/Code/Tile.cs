﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

sealed public class Tile
{
    public int Id { get; private set; }
    public int Price { get; private set; }
    public AbstractPlayer Owner { get; private set; }
    public Roboticon InstalledRoboticon { get; private set; }
    private Dictionary<ItemType, int> resources;

    /// <summary>
    /// Constructor for tile
    /// </summary> 
    /// <param name="cost">Cost of the tile</param>
    /// <param name="ore">Ore that the tile has</param>
    /// <param name="power">Power that the tile has</param>
    public Tile(int id, int price, int ore, int power)
    {
        Id = id;
        Price = price;
        resources = new Dictionary<ItemType, int>();
        resources[ItemType.Ore] = ore;
        resources[ItemType.Power] = power;
        Owner = null;
        InstalledRoboticon = null;
    }

    public void SetOwner(AbstractPlayer p)
    {
        Owner = p;
    }

    /// <summary>
    /// Get the production of the tile
    /// </summary>
    /// <returns>Dictionary of ItemType -> Amount produced of that resource</returns>
    public Dictionary<ItemType, int> Produce()
    {
        Dictionary<ItemType, int> produced = new Dictionary<ItemType, int>();
        List<ItemType> itemTypes = new List<ItemType>();
        itemTypes.Add(ItemType.Ore);
        itemTypes.Add(ItemType.Power);
        foreach (ItemType itemType in itemTypes)
        {
            if (InstalledRoboticon == null)
            {
                produced[itemType] = 0;
            }
            else
            {
                produced[itemType] = resources[itemType] * InstalledRoboticon.ProductionMultiplier(itemType);
            }
        }
        return produced;
    }

    /// <summary>
    /// install a roboticon on this tile
    /// </summary>
    /// <exception cref="RoboticonAlreadyInstalledException">A roboticon is already installled</exception>
    /// <exception cref="ArgumentOutOfRangeException">No spare roboticons to install</exception>
    /// <param name="player">Reference to the player buying the tile</param>
    public void InstallRoboticon(AbstractPlayer player)
    {
        if (InstalledRoboticon != null)
        {
            throw new RoboticonAlreadyInstalledException();
        }

        if (player.Inv.GetItemAmount(ItemType.Roboticon) <= player.InstalledRoboticonCount)
        {
            throw new ArgumentOutOfRangeException("Not enough spare roboticons in inventory to install");
        }

        InstalledRoboticon = new Roboticon(this);
    }

    /// <summary>
    /// Remove the roboticon installed on this tile
    /// </summary>
    /// <exception cref="InvalidOperationException">there is no roboticon installed</exception>
    /// <returns>the removed roboticon</returns>
    public void RemoveRoboticon()
    {
        if (InstalledRoboticon == null)
        {
            throw new InvalidOperationException("No Roboticon installed");
        }
        InstalledRoboticon = null;
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

    public override bool Equals(object obj)
    {
        if (!(obj is Tile))
            return false;

        return Id == ((Tile)obj).Id;
    }

    public override int GetHashCode()
    {
        return Id;
    }

}