using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An inventory. Holds all resources for a player or market and facilitates transactions with other inventories.
/// </summary>
sealed public class Inventory
{
    public int Money { get; private set; }

    private Dictionary<ItemType, int> Items;

    /// <summary>
    /// Create an inventory instance, which holds all resources for a player or market.
    /// </summary>
    /// <param name="money">The money in the inventory.</param>
    /// <param name="ore">The ore in the inventory.</param>
    /// <param name="power">The power in the inventory.</param>
    /// <param name="roboticons">The roboticons in the inventory.</param>
    public Inventory(int money, int ore, int power, int roboticons)
    {
        Money = money;
        Items = new Dictionary<ItemType, int>();
        Items[ItemType.Ore] = ore;
        Items[ItemType.Power] = power;
        Items[ItemType.Roboticon] = roboticons;
    }

    /// <summary>
    /// Creates an empty inventory instance, which holds all resources for a player or market.
    /// </summary>
    public Inventory()
    {
        Money = 0;
        Items = new Dictionary<ItemType, int>();
        Items[ItemType.Ore] = 0;
        Items[ItemType.Power] = 0;
        Items[ItemType.Roboticon] = 0;
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
    public void AddItem(ItemType item, int amount)
    {
        Items[item] += amount;
    }

    /// <summary>
    /// Add an amount of money to this inventory instance.
    /// </summary>
    /// <param name="amount">The amount of money to add.</param>
    public void AddMoney(int amount)
    {
        Money += amount;
    }

    /// <summary>
    /// Transfers a select quantity of an item to the provided inventory.
    /// </summary>
    /// <param name="item">The item being transferred.</param>
    /// <param name="quantity">The quantity being transferred.</param>
    /// <param name="to">The inventory to transfer to.</param>
    /// <returns>True if the transfer was successful, false otherwise.</returns>
    public bool TransferItem(ItemType item, int quantity, Inventory to)
    {
        if (Items[item] - quantity < 0)
            return false;

        Items[item] -= quantity;
        to.AddItem(item, quantity);
        return true;
    }

    /// <summary>
    /// Transfers a select amount of money to the provided inventory.
    /// </summary>
    /// <param name="amount">The amount of money to send.</param>
    /// <param name="to">The inventory to transfer to</param>
    /// <returns>True if the transfer was successful, false otherwise.</returns>
    public bool TransferMoney(int amount, Inventory to)
    {
        if (Money - amount < 0)
            return false;

        Money -= amount;
        to.AddMoney(amount);
        return true;
    }
}