using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A market. Used for the selling and buying of items to and from players.
/// </summary>
sealed public class Market
{
    public Inventory Stock { get; private set; }
    public bool Open { get; private set; }

    private Dictionary<ItemType, int> _buyprice;
    private Dictionary<ItemType, int> _sellprice;

    /// <summary>
    /// Creates a market instance with an empty inventory as its stock.
    /// </summary>
    public Market()
    {
        Stock = new Inventory();
        _buyprice = new Dictionary<ItemType, int>();
        _sellprice = new Dictionary<ItemType, int>();

        //TEMP: Set buy and sell price manually, will probably populate them from a text file in future
        _buyprice[ItemType.Ore] = 10;
        _buyprice[ItemType.Power] = 11;
        _buyprice[ItemType.Roboticon] = 12;
        _sellprice[ItemType.Ore] = 9;
        _sellprice[ItemType.Power] = 8;
        _sellprice[ItemType.Roboticon] = 7;
    }

    /// <summary>
    /// Creates a market instance with the provided inventory as its stock.
    /// </summary>
    /// <param name="stock">The starting stock for the market</param>
    public Market(Inventory stock)
    {
        Stock = stock;
        _buyprice = new Dictionary<ItemType, int>();
        _sellprice = new Dictionary<ItemType, int>();

        //TEMP: Set buy and sell price manually, will probably populate them from a text file in future
        _buyprice[ItemType.Ore] = 10;
        _buyprice[ItemType.Power] = 11;
        _buyprice[ItemType.Roboticon] = 12;
        _sellprice[ItemType.Ore] = 9;
        _sellprice[ItemType.Power] = 8;
        _sellprice[ItemType.Roboticon] = 7;
    }

    /// <summary>
    /// Gets the price to buy the specified item from this market instance.
    /// </summary>
    /// <param name="item">The item to check the buy price of.</param>
    /// <returns>The buy price of the specified item.</returns>
    public int GetBuyPrice(ItemType item)
    {
        return _buyprice[item];
    }

    /// <summary>
    /// Gets the price to sell the specified item to this market instance.
    /// </summary>
    /// <param name="item">The item to check the sell price of.</param>
    /// <returns>The sell price of the specified item.</returns>
    public int GetSellPrice(ItemType item)
    {
        return _sellprice[item];
    }

    /// <summary>
    /// Allows a player to buy from the market.
    /// </summary>
    /// <param name="item">The item the player wishes to buy.</param>
    /// <param name="Quantity">The quantity the player withes to buy.</param>
    /// <param name="playerInventory">Reference to the players inventory.</param>
    /// <returns></returns>
    public bool Buy(ItemType item, int quantity, Inventory playerInventory)
    {
        //Attempt to transfer money from the player to the market. If successful, then try to transfer the purchased item(s)
        if (playerInventory.TransferMoney(_buyprice[item] * quantity, Stock))
        {
            //Attempt to transfer the requested item(s) into the players inventory. If true, then the transaction is complete, if false, then revert the money transaction and return false.
            if (Stock.TransferItem(item, quantity, playerInventory))
                return true;
            else
                Stock.TransferMoney(_buyprice[item] * quantity, playerInventory);
        }

        return false;
    }

    /// <summary>
    /// Allows a player to sell to the market.
    /// </summary>
    /// <param name="item">The item the player wishes to sell.</param>
    /// <param name="quantity">The quantity the player wishes to sell</param>
    /// <param name="playerInventory">Reference to the players inventory.</param>
    /// <returns></returns>
    public bool Sell(ItemType item, int quantity, Inventory playerInventory)
    {
        //Attempt to transfer money from the market to the player. If successful, then try to transfer the purchased item(s)
        if (Stock.TransferMoney(_sellprice[item] * quantity, playerInventory))
        {
            //Attempt to transfer the requested item(s) into the markets inventory. If true, then the transaction is complete, if false, then revert the money transaction and return false.
            if (playerInventory.TransferItem(item, quantity, Stock))
                return true;
            else
                playerInventory.TransferMoney(_sellprice[item] * quantity, Stock);
        }

        return false;
    }
}

public enum ItemType
{
    Ore = 0,
    Power,
    Roboticon
}