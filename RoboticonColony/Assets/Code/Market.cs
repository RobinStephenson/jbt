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
  
    /// <summary>
    /// customisationsList is a list of all RoboticonCustomisations
    /// </summary>
    public List<RoboticonCustomisation> CustomisationsList { get; private set; }
  
    private Dictionary<ItemType, int> buyprice;
    private Dictionary<ItemType, int> sellprice;

    /// <summary>
    /// Creates a market instance with the provided inventory as its stock.
    /// </summary>
    /// <param name="stock">The starting stock for the market</param>
    /// <exception cref="ArgumentOutOfRangeException">The Exception thrown when the market is initialised with negative parameters.</exception>
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
    /// <exception cref="ArgumentOutOfRangeException">The Excpetion thrown when a negative amount of items are bought.</exception>
    public void Buy(ItemType item, int quantity, Inventory playerInventory)
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

                //If the transfer completes without error, then the transaction is complete
                return;
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
    /// <exception cref="ArgumentOutOfRangeException">The Exception thrown when a negative quanitity of items are sold.</exception>
    public void Sell(ItemType item, int quantity, Inventory playerInventory)
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

                //If the transfer completes without error, then the transaction is complete
                return;
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
  
    /// <summary>
    /// Creates an customisation type for roboticons.
    /// </summary>
    /// <param name="selectedName"> The name of the customisation. </param>
    /// <param name="bonusMult"> The multiplier which the production will be boosted by. </param>
    /// <param name="prereq"> The list of other customisations which must completed already before this customisation can be applied. </param>
    /// <param name="selectedResource"> The type of resource which the customisation augments. </param>
    /// <param name="reqPrice"> The required price of the customisation. </param>
    private void CreateCustomisations(string selectedName, Dictionary<ItemType, int> bonuses, List<RoboticonCustomisation> prereq, ItemType selectedResource, int reqPrice)
    {
        CustomisationsList.Add(new RoboticonCustomisation(selectedName, bonuses, prereq, reqPrice));
    }

    public int GetTilePrice(Tile tile)
    {
        return tile.Price;
    }
  
    /// <summary>
    /// Adds a roboticon to the market stock.
    /// </summary>
    public void BuyRoboticonOre()
    {
        //TODO: Update to not use negative numbers, as this will no longer work with inventory in future
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
    public void BuyCustomisation(RoboticonCustomisation customisation, Roboticon roboticon, Inventory inventory)
    {
        if (inventory.Money > customisation.Price)
        {
            roboticon.AddCustomisation(customisation);
            inventory.AddMoney(-customisation.Price);
        }
        else
        {
            throw new ArgumentException("Not enough money in inventory to buy this customisation. ");
        }

    }

    /// <summary>
    /// Function to find the maximum amount of the specified item type that the market will buy.
    /// </summary>
    /// <param name="typeToBeBought"> The item type that the market will buy</param>
    /// <returns>An int which represents the maximum number of the specified type of item that the market will buy.</returns>
    public int MaxWillBuy(ItemType typeToBeBought)
    {
        if (Stock.Money - (GetBuyPrice(typeToBeBought) * 10) > 0)
        {
            if (Stock.GetItemAmount(typeToBeBought) >= 10)
            {
                return 10;
            }
            else
            {
                return Stock.GetItemAmount(typeToBeBought);
            }

        }
        else
        {
            int moneyAvailable = Stock.Money;
            int quantityAvailable = Stock.GetItemAmount(typeToBeBought);
            int quantity = 0;
            while ((moneyAvailable > 0) && quantityAvailable > 0)
            {
                moneyAvailable -= GetBuyPrice(typeToBeBought);
                quantity += 1;
                quantityAvailable -= 1;
            }
            return quantity;
        }
    }

    /// <summary>
    /// Allows players to buy tiles from the market. Purchased tiles from the market do not actually reduce the markets balance.
    /// </summary>
    /// <param name="tile">The tile to buy</param>
    /// <param name="playerInventory">Reference ot the players inventory</param>
    public void BuyTile(Tile tile, Inventory playerInventory)
    {
        //Attempt to remove the money for the purchase form the player
        try
        {
            playerInventory.SubtractMoney(tile.GetCost());

            //Attempt to transfer the requested tile from the markets inventory
            try
            {
                Stock.TransferTile(tile, playerInventory);

                //If the transfer completes without error, then the transaction is complete
                return;
            }
            catch (NotEnoughItemException)
            {
                //If the item transfer was unsuccessful, then revert the change in the players balance
                playerInventory.AddMoney(tile.GetCost());
                throw;
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            //If the initial money transfer was unsuccessful, then re-throw the exception
            throw;
        }
    }
}