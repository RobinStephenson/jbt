using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An inventory. Holds all resources for a player or market and facilitates transactions with other inventories.
/// </summary>
sealed public class Inventory
{
    public int Money { get; private set; }
    public List<Tile> Tiles;

    private List<Tile> tiles;
    private Dictionary<ItemType, int> Items;

    /// <summary>
    /// Create an inventory instance, which holds all resources for a player or market.
    /// </summary>
    /// <param name="money">The money in the inventory.</param>
    /// <param name="ore">The ore in the inventory.</param>
    /// <param name="power">The power in the inventory.</param>
    /// <param name="roboticons">The roboticons in the inventory.</param>
    /// <exception cref="ArgumentOutOfRangeException"> Exception thrown when the inventory class is constructed with negative parameters. </exception>
    public Inventory(int money, int ore, int power, int roboticons)
    {
        if(money < 0 || ore < 0 || power < 0 || roboticons < 0)
        {
            throw new ArgumentOutOfRangeException("Inventories cannot hold negative amounts of money/items");
        }

        Money = money;
        Tiles = new List<Tile>();
        Items = new Dictionary<ItemType, int>();
        Items[ItemType.Ore] = ore;
        Items[ItemType.Power] = power;
        Items[ItemType.Roboticon] = roboticons;
    }

    /// <summary>
    /// Creates an empty inventory instance, which holds all resources for a player or market.
    /// </summary>
    public Inventory() : this(0, 0, 0, 0) { }

    //TEMP
    public void SetTiles(List<Tile> t)
    {
        Tiles = t;
    }
  
    /// <summary>
    /// Get the amount of the specified item in this inventory instance.
    /// </summary>
    /// <param name="item">The item to get the amount of.</param>
    /// <returns>The amount of the specified item.</returns>
    public int GetItemAmount(ItemType item)
    {
        return Items[item];
    }

    /// <summary>
    /// Add an amount of an item to this inventory instance.
    /// </summary>
    /// <param name="item">The item to add</param>
    /// <param name="amount">The amount of the item to add.</param>
    /// <exception cref="ArgumentOutOfRangeException">The Excpetion thrown when the the number of items added is negative.</exception>
    public void AddItem(ItemType item, int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException("Cannot transfer negative amounts of items");
        }
        Items[item] += amount;
    }

    /// <summary>
    /// Add an amount of money to this inventory instance.
    /// </summary>
    /// <param name="amount">The amount of money to add.</param>
    /// <exception cref="ArgumentOutOfRangeException">The Exception thrown when a negative amount of money is added.</exception>
    public void AddMoney(int amount)
    {
        if(amount < 0)
        {
            throw new ArgumentOutOfRangeException("Cannot transfer negative amounts of money");
        }
        Money += amount;
    }

    /// <summary>
    /// Remove an amount of money from this player instance
    /// </summary>
    /// <param name="amount">The amount of money to remove</param>
    public void SubtractMoney(int amount)
    {
        if(amount > Money)
        {
            throw new NotEnoughMoneyException("Tried to remove more money than the inventory has");
        }
        Money -= amount;
    }

    /// <summary>
    /// Add the supplied tile to this inventory instance
    /// </summary>
    /// <param name="tile">The tile to add to this inventory instance</param>
    public void AddTile(Tile tile)
    {
        if(tiles.Contains(tile))
        {
            throw new ArgumentException("The supplied tile already located in this inventory");
        }
        else
        {
            tiles.Add(tile);
        }
    }

    /// <summary>
    /// Transfers a select quantity of an item to the provided inventory.
    /// </summary>
    /// <param name="item">The item being transferred.</param>
    /// <param name="quantity">The quantity being transferred.</param>
    /// <param name="to">The inventory to transfer to.</param>
    /// <exception cref="ArgumentOutOfRangeException">The Exception thrown when a negative quantity of items are transfered.</exception>
    /// <exception cref="NotEnoughItemException">The Excpetion thrown when market doesn't have enough of the specified item. </exception>
    public void TransferItem(ItemType item, int quantity, Inventory to)
    {
        if (Items[item] - quantity < 0)
        {
            throw new NotEnoughItemException();
        }

        if(quantity < 0)
        {
            throw new ArgumentOutOfRangeException("Cannot transfer negative amounts of items");
        }

        Items[item] -= quantity;
        to.AddItem(item, quantity);
        return;
    }

    /// <summary>
    /// Transfers a select amount of money to the provided inventory.
    /// </summary>
    /// <param name="amount">The amount of money to send.</param>
    /// <param name="to">The inventory to transfer to</param>
    /// <exception cref="NotEnoughMoneyException">The Exception thrown when the provided market has not enough money.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The Exception thrown when the market attempts to transfer a negative amount of money.</exception>
    public void TransferMoney(int amount, Inventory to)
    {
        if (Money - amount < 0)
        {
            throw new NotEnoughMoneyException();
        }

        if(amount < 0)
        {
            throw new ArgumentOutOfRangeException("Cannot transfer negative amounts of money");
        }

        Money -= amount;
        to.AddMoney(amount);
        return;
    }

    /// <summary>
    /// Transfers a selected tile to the provided inventory.
    /// </summary>
    /// <param name="tile">The tile to transfer</param>
    /// <param name="to">The inventory to transfer to</param>
    public void TransferTile(Tile tile, Inventory to)
    {
        if(!tiles.Contains(tile))
        {
            throw new ArgumentOutOfRangeException("Supplied tile does not belong to this inventory");
        }

        tiles.Remove(tile);
        to.AddTile(tile);
        return;
    }

    /// <summary>
    /// Getter used to get the amount of tiles in the tiles list. Cannot allow access to the list directly, even in a getter, as methods like Add can still be called
    /// </summary>
    /// <returns>The amount of tiles in the tiles list</returns>
    public int TileCount()
    {
        return tiles.Count;
    }

    /// <summary>
    /// Getter used to get the tile at the supplied index in the tiles list. Cannot allow access to the list directly, even in a getter, as methods like Add can still be called
    /// </summary>
    /// <param name="index">The index to get the tile of</param>
    /// <returns>The requested tile reference</returns>
    public Tile GetTile(int index)
    {
        if(index < tiles.Count - 1)
        {
            throw new ArgumentOutOfRangeException("Supplied index is greater than length of tile list: " + tiles.Count.ToString());
        }

        return tiles[index];
    }
}