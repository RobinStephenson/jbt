using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

sealed public class Tile
{
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
    /// Get the production of the tile
    /// </summary>
    /// <returns>Dictionary of ItemType -> Ammount produced of that resource</returns>
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

    /// <summary>
    /// Sets the owner of the tile to the newOwner
    /// </summary>
    /// <param name="newOwner">The reference to the new owner.</param>
    /// <exception cref="ArgumentException">The Exception thrown when the old owner = the new owner. </exception>
    public void SetOwner(AbstractPlayer newOwner)
    {
        if(Owner == newOwner)
        {
            throw new ArgumentException("New owner cannot be the same as the old owner.");
        }
        Owner = newOwner;
    }
}