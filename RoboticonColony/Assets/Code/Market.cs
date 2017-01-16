using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A market. Used for the selling and buying of items to and from players.
/// </summary>
sealed public class Market
{
    public Inventory Stock { get; private set; }
    public bool Open { get; private set; }

    private Dictionary<ItemType, int> buyprice;
    private Dictionary<ItemType, int> sellprice;

    /// <summary>
    /// Creates a market instance with the provided inventory as its stock.
    /// </summary>
    /// <param name="stock">The starting stock for the market</param>
    public Market(Inventory stock, int oreBuyPrice, int powerBuyPrice, int roboticonBuyPrice, int oreSellPrice, int powerSellPrice, int roboticonSellPrice)
    {
        if (oreBuyPrice < 0 || powerBuyPrice < 0 || roboticonBuyPrice < 0 || oreSellPrice < 0 || powerSellPrice < 0 || roboticonSellPrice < 0)
        {
            throw new ArgumentOutOfRangeException("Buy and sell prices cannot be negative");
        }

        Stock = stock;
        buyprice = new Dictionary<ItemType, int>();
        sellprice = new Dictionary<ItemType, int>();

        //TEMP: Set buy and sell price manually, will probably populate them from a text file in future
        buyprice[ItemType.Ore] = oreBuyPrice;
        buyprice[ItemType.Power] = powerBuyPrice;
        buyprice[ItemType.Roboticon] = roboticonBuyPrice;
        sellprice[ItemType.Ore] = oreSellPrice;
        sellprice[ItemType.Power] = powerSellPrice;
        sellprice[ItemType.Roboticon] = roboticonSellPrice;
    }

    /// <summary>
    /// Creates a market instance with an empty inventory as its stock.
    /// </summary>
    public Market(int oreBuyPrice, int powerBuyPrice, int roboticonBuyPrice, int oreSellPrice, int powerSellPrice, int roboticonSellPrice) : this(new Inventory(), oreBuyPrice, powerBuyPrice, roboticonBuyPrice, oreSellPrice, powerSellPrice, roboticonSellPrice) { }

    /// <summary>
    /// Gets the price to buy the specified item from this market instance.
    /// </summary>
    /// <param name="item">The item to check the buy price of.</param>
    /// <returns>The buy price of the specified item.</returns>
    public int GetBuyPrice(ItemType item)
    {
        return buyprice[item];
    }

    /// <summary>
    /// Gets the price to sell the specified item to this market instance.
    /// </summary>
    /// <param name="item">The item to check the sell price of.</param>
    /// <returns>The sell price of the specified item.</returns>
    public int GetSellPrice(ItemType item)
    {
        return sellprice[item];
    }

    /// <summary>
    /// Allows a player to buy from the market.
    /// </summary>
    /// <param name="item">The item the player wishes to buy.</param>
    /// <param name="Quantity">The quantity the player withes to buy.</param>
    /// <param name="playerInventory">Reference to the players inventory.</param>
    /// <returns>This market reference, for method chaining.</returns>
    public Market Buy(ItemType item, int quantity, Inventory playerInventory)
    {
        if(quantity < 0)
        {
            throw new ArgumentOutOfRangeException("Cannot buy negative amounts of items");
        }

        //Attempt to transfer money from the player to the market
        try
        {
            playerInventory.TransferMoney(buyprice[item] * quantity, Stock);

            //Attempt to transfer the requested item(s) into the players inventory.
            try
            {
                Stock.TransferItem(item, quantity, playerInventory);

                //If the transfer completes without error, then the transaction is complete and a reference to this market instance is returned.
                return this;
            }
            catch (NotEnoughItemException)
            {
                //If the item transfer was unsuccessful, then revert the money transfer and re-throw the exception
                Stock.TransferMoney(buyprice[item] * quantity, playerInventory);
                throw;
            }
        }
        catch (NotEnoughMoneyException)
        {
            //If the initial money transfer was unsuccessful, then re-throw the exception
            throw;
        }
    }

    /// <summary>
    /// Allows a player to sell to the market.
    /// </summary>
    /// <param name="item">The item the player wishes to sell.</param>
    /// <param name="quantity">The quantity the player wishes to sell</param>
    /// <param name="playerInventory">Reference to the players inventory.</param>
    /// <returns>This market reference, for method chaining.</returns>
    public Market Sell(ItemType item, int quantity, Inventory playerInventory)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException("Cannot sell negative amounts of items");
        }

        //Attempt to transfer money from the market to the player.
        try
        {
            Stock.TransferMoney(sellprice[item] * quantity, playerInventory);

            //Attempt to transfer the requested item(s) into the markets inventory.
            try
            {                
                playerInventory.TransferItem(item, quantity, Stock);

                //If the transfer completes without error, then the transaction is complete and a reference to this market instance is returned.
                return this;
            }
            catch (NotEnoughItemException)
            {
                //If the item transfer was unsuccessful, then revert the money transfer and re-throw the exception
                playerInventory.TransferMoney(sellprice[item] * quantity, Stock);
                throw;
            }
        }
        catch(NotEnoughMoneyException)
        {
            //If the initial money transfer was unsuccessful, then re-throw the exception
            throw;
        }
    }
}