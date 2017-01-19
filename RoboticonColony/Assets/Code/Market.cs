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

    private Dictionary<ItemType, int> _buyprice;
    private Dictionary<ItemType, int> _sellprice;
  
    /// <summary>
    /// customisationsList is a list of all RoboticonCustomisations
    /// </summary>
    public List<RoboticonCustomisation> CustomisationsList { get; private set; }
  
    /// <summary>
    /// Creates a market instance with the provided inventory as its stock.
    /// </summary>
    /// <param name="stock">The starting stock for the market</param>
    /// <summary>
    /// Creates a market instance with the provided inventory as its stock.
    /// </summary>
    /// <param name="stock">The starting stock for the market</param>
    public Market(Inventory stock, int oreBuyPrice, int powerBuyPrice, int roboticonBuyPrice, int oreSellPrice, int powerSellPrice, int roboticonSellPrice)
    {
        Stock = stock;
        _buyprice = new Dictionary<ItemType, int>();
        _sellprice = new Dictionary<ItemType, int>();
        CustomisationsList = null;

        //TEMP: Set buy and sell price manually, will probably populate them from a text file in future
        _buyprice[ItemType.Ore] = oreBuyPrice;
        _buyprice[ItemType.Power] = powerBuyPrice;
        _buyprice[ItemType.Roboticon] = roboticonBuyPrice;
        _sellprice[ItemType.Ore] = oreSellPrice;
        _sellprice[ItemType.Power] = powerSellPrice;
        _sellprice[ItemType.Roboticon] = roboticonSellPrice;


        /// Temporary initialisation of customisations, may be done through reading in a file later. 
        createCustomisations("Ore 1", 2, null, ItemType.Ore, 10);
        createCustomisations("Power 1", 2, null, ItemType.Power, 10);
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
    /// <returns>This market reference, for method chaining.</returns>
    public Market Buy(ItemType item, int quantity, Inventory playerInventory)
    {
        //Attempt to transfer money from the player to the market
        try
        {
            playerInventory.TransferMoney(_buyprice[item] * quantity, Stock);

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
                Stock.TransferMoney(_buyprice[item] * quantity, playerInventory);
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
        //Attempt to transfer money from the market to the player.
        try
        {
            Stock.TransferMoney(_sellprice[item] * quantity, playerInventory);

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
                playerInventory.TransferMoney(_sellprice[item] * quantity, Stock);
                throw;
            }
        }
        catch(NotEnoughMoneyException)
        {
            //If the initial money transfer was unsuccessful, then re-throw the exception
            throw;
        }
    }
 
    /// <summary>
    /// Creates an customisation type for roboticons.
    /// </summary>
    /// <param name="selectedName"> The name of the customisation. </param>
    /// <param name="bonusMult"> The multiplier which the production will be boosted by. </param>
    /// <param name="prereq"> The list of other customisations which must completed already before this customisation can be applied. </param>
    /// <param name="selectedResource"> The type of resource which the customisation augments. </param>
    /// <param name="reqPrice"> The required price of the customisation. </param>
    private void createCustomisations(string selectedName, int bonusMult, RoboticonCustomisation prereq, ItemType selectedResource, int reqPrice)
    {
        CustomisationsList.Add(new RoboticonCustomisation(selectedName, bonusMult, prereq, selectedResource, reqPrice));
    }

    /// <summary>
    /// Adds a roboticon to the market stock.
    /// </summary>
    private void BuyRoboticonOre()
    {
        if (Stock.GetItemAmount(ItemType.Ore) > 0)
        {
            Stock.AddItem(ItemType.Ore, -1);
            Stock.AddItem(ItemType.Roboticon, 1);
        }
    }

    /// <summary>
    /// Function to buy a specified customisation for the specified roboticon.
    /// </summary>
    /// <param name="inv"> The player's inventory</param>
    /// <param name="customisation"> The selected roboticon customisation</param>
    /// <param name="roboticon"> The selected Roboticon</param>
    public void BuyCustomisation(Inventory inv, RoboticonCustomisation customisation, Roboticon roboticon)
    {
        if (inv.Money > customisation.Price)
        {
            roboticon.AddCustomisation(customisation);
            inv.AddMoney(-customisation.Price);
        }
        else
        {
            throw new ArgumentException("Not enough money in inventory to buy this customisation. ");
        }

    }
}