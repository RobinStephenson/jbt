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
        Items = new Dictionary<ItemType, int>();
        Items[ItemType.Ore] = ore;
        Items[ItemType.Power] = power;
        Items[ItemType.Roboticon] = roboticons;
    }

    /// <summary>
    /// Creates an empty inventory instance, which holds all resources for a player or market.
    /// </summary>
    public Inventory() : this(0, 0, 0, 0) { }


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
    /// Transfers a select quantity of an item to the provided inventory.
    /// </summary>
    /// <param name="item">The item being transferred.</param>
    /// <param name="quantity">The quantity being transferred.</param>
    /// <param name="to">The inventory to transfer to.</param>
    /// <returns>A reference to this inventory instance, used for method chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The Exception thrown when a negative quantity of items are transfered.</exception>
    /// <exception cref="NotEnoughItemException">The Excpetion thrown when market doesn't have enough of the specified item. </exception>
    public Inventory TransferItem(ItemType item, int quantity, Inventory to)
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
        return this;
    }

    /// <summary>
    /// Transfers a select amount of money to the provided inventory.
    /// </summary>
    /// <param name="amount">The amount of money to send.</param>
    /// <param name="to">The inventory to transfer to</param>
    /// <returns>A reference to this inventory instance, used for method chaining.</returns>
    /// <exception cref="NotEnoughMoneyException">The Exception thrown when the provided market has not enough money.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The Exception thrown when the market attempts to transfer a negative amount of money.</exception>
    public Inventory TransferMoney(int amount, Inventory to)
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
        return this;
    }
}