using UnityEngine;
using UnityEditor;
using System;
using NUnit.Framework;

public class MarketTest
{
    [Test]
    public void CreateMarketWithoutInventory()
    {
        //Create an empty market instance
        Market market = new Market(3, 9, 3, 8, 4, 6);

        //Verify that the markets inventory is empty
        Assert.AreEqual(0, market.Stock.Money);
        Assert.AreEqual(0, market.Stock.GetItemAmount(ItemType.Ore));
        Assert.AreEqual(0, market.Stock.GetItemAmount(ItemType.Power));
        Assert.AreEqual(0, market.Stock.GetItemAmount(ItemType.Roboticon));
    }

    [Test]
    public void CreateMarketWithInventory()
    {
        //Create an inventory to give to the market
        Inventory inv = new Inventory(1, 4, 2, 3);

        //Create the market instance
        Market market = new Market(inv, 7, 4, 1, 3, 6, 7);

        //Verify that the market contains the items from the inventory
        Assert.AreEqual(1, market.Stock.Money);
        Assert.AreEqual(4, market.Stock.GetItemAmount(ItemType.Ore));
        Assert.AreEqual(2, market.Stock.GetItemAmount(ItemType.Power));
        Assert.AreEqual(3, market.Stock.GetItemAmount(ItemType.Roboticon));
    }

    [Test]
    public void CreateNegativeMarket()
    {
        //Attempt to create a market with some negative prices, which should not work and return an error
        Assert.Throws<ArgumentOutOfRangeException>(() => NegativeMarket());
    }

    [Test]
    public void GettingBuyPrices()
    {
        //Create an empty market instance
        Market market = new Market(10, 11, 12, 9, 8, 7);

        //Verify the buy price is the same as the price set in the markets construction
        Assert.AreEqual(10, market.GetBuyPrice(ItemType.Ore));
        Assert.AreEqual(11, market.GetBuyPrice(ItemType.Power));
        Assert.AreEqual(12, market.GetBuyPrice(ItemType.Roboticon));
    }

    [Test]
    public void GettingSellPrices()
    {
        //Create an empty market instance
        Market market = new Market(10, 11, 12, 9, 8, 7);

        //Verify the sell price is the same as the price set in the markets construction
        Assert.AreEqual(9, market.GetSellPrice(ItemType.Ore));
        Assert.AreEqual(8, market.GetSellPrice(ItemType.Power));
        Assert.AreEqual(7, market.GetSellPrice(ItemType.Roboticon));
    }

    [Test]
    public void FailedBuy()
    {
        //Create an inventory instance
        Inventory marketInv = new Inventory(5, 6, 7, 2);

        //Create a market instance with the inventory
        Market market = new Market(marketInv, 10, 11, 12, 9, 8, 7);

        //Create a mock player inventory
        Inventory playerInv = new Inventory(50, 0, 0, 0);

        HumanPlayer player = new HumanPlayer("p1", playerInv, market, new Sprite());

        //Attempt to buy 5 power from the market at the cost of 11 each, more than the player can afford, which should throw an exception
        Assert.Throws<NotEnoughMoneyException>(() => market.Buy(ItemType.Power, 5, player));

        //Now try to buy 3 roboticons, when the market only has 2, which should throw an exception
        Assert.Throws<NotEnoughItemException>(() => market.Buy(ItemType.Roboticon, 3, player));

        //Now try to buy a negative amount of items from the market, which should throw an exception
        Assert.Throws<ArgumentOutOfRangeException>(() => market.Sell(ItemType.Ore, -4, player));

        //Check if the purchase was successful, which it shouldnt have been, and check if both inventories contain the same amount of roboticons and money
        Assert.AreEqual(2, market.Stock.GetItemAmount(ItemType.Roboticon));
        Assert.AreEqual(0, playerInv.GetItemAmount(ItemType.Roboticon));
        Assert.AreEqual(5, market.Stock.Money);
        Assert.AreEqual(50, playerInv.Money);
    }

    [Test]
    public void SuccessfulBuy()
    {
        //Create an inventory instance
        Inventory marketInv = new Inventory(5, 6, 7, 2);

        //Create a market instance with the inventory
        Market market = new Market(marketInv, 10, 11, 12, 9, 8, 7);

        //Create a mock player inventory
        Inventory playerInv = new Inventory(50, 0, 0, 0);

        HumanPlayer player = new HumanPlayer("p1", playerInv, market, new Sprite());

        //Attempt to buy 3 power from the market at the cost of 11 each, which should work and not throw an exception
        market.Buy(ItemType.Power, 3, player);

        //Check if both inventories have been updated accordingly
        Assert.AreEqual(4, market.Stock.GetItemAmount(ItemType.Power));
        Assert.AreEqual(3, playerInv.GetItemAmount(ItemType.Power));
        Assert.AreEqual(38, market.Stock.Money);
        Assert.AreEqual(17, playerInv.Money);
    }

    [Test]
    public void FailedSell()
    {
        //Create an inventory instance
        Inventory marketInv = new Inventory(150, 6, 7, 2);

        //Create a market instance with the inventory
        Market market = new Market(marketInv, 10, 11, 12, 9, 8, 7);

        //Create a mock player inventory
        Inventory playerInv = new Inventory(37, 3, 6, 27);

        HumanPlayer player = new HumanPlayer("p1", playerInv, market, new Sprite());

        //Attempt to sell 5 ore to the market at the cost of 9 each, which is more ore than the player has, which should throw an exception
        Assert.Throws<NotEnoughItemException>(() => market.Sell(ItemType.Ore, 5, player));

        //Now try to sell 25 roboticons at 7 each, which the market cannot afford, which should throw an exception
        Assert.Throws<NotEnoughMoneyException>(() => market.Sell(ItemType.Roboticon, 25, player));

        //Now try to sell a negative amount of items to the market, which should throw an exception
        Assert.Throws<ArgumentOutOfRangeException>(() => market.Sell(ItemType.Power, -4, player));

        //Check if both inventories contain the same amount of roboticons and money
        Assert.AreEqual(2, market.Stock.GetItemAmount(ItemType.Roboticon));
        Assert.AreEqual(27, playerInv.GetItemAmount(ItemType.Roboticon));
        Assert.AreEqual(150, market.Stock.Money);
        Assert.AreEqual(37, playerInv.Money);
    }

    [Test]
    public void SuccessfulSell()
    {
        //Create an inventory instance
        Inventory marketInv = new Inventory(150, 6, 7, 2);

        //Create a market instance with the inventory
        Market market = new Market(marketInv, 10, 11, 12, 9, 8, 7);

        //Create a mock player inventory
        Inventory playerInv = new Inventory(37, 3, 6, 27);

        HumanPlayer player = new HumanPlayer("p1", playerInv, market, new Sprite());

        //Attempt to sell 4 power to the market at the cost of 8 each, which should work and not throw an exception
        market.Sell(ItemType.Power, 4, player);

        //Check if both inventories have been updated accordingly
        Assert.AreEqual(11, market.Stock.GetItemAmount(ItemType.Power));
        Assert.AreEqual(2, playerInv.GetItemAmount(ItemType.Power));
        Assert.AreEqual(118, market.Stock.Money);
        Assert.AreEqual(69, playerInv.Money);
    }

    [Test]
    public void CreateNewCustomisations()
    {
        //Create an inventory instance
        Inventory marketInv = new Inventory(150, 6, 7, 2);

        //Create a market instance with the inventory
        Market market = new Market(marketInv, 10, 11, 12, 9, 8, 7);

        //TODO: Add customisation to market
        //Create new customisation
        //market.createCustomisations("test2", 3, null, ItemType.Ore, 10);

        //Check if customisation was added to Customisation list
        //Assert.AreEqual(1, market.CustomisationList.count());
    }

    [Test]
    public void SuccessfulBuyRoboticonWithOre()
    {
        //Create an inventory instance
        Inventory marketInv = new Inventory(150, 6, 7, 2);

        //Create a market instance with the inventory
        Market market = new Market(marketInv, 10, 11, 12, 9, 8, 7);

        //Attempt to buy roboticons with ore
        market.BuyRoboticonOre();

        //Check if the roboticon was bought and ore was taken
        Assert.AreEqual(7, market.Stock.GetItemAmount(ItemType.Roboticon));
        Assert.AreEqual(1, market.Stock.GetItemAmount(ItemType.Ore));
    }

    /// <summary>
    /// Used by the MetNegativeMarket Test method to attempt to create a market with negative prices
    /// </summary>
    private Market NegativeMarket()
    {
        return new Market(4, 5, 6, 2, -3, 5);
    }

    [Test]
    public void SuccessfulBuyTile()
    {
        //Creates a market and player and their inventories and assigns a tile to the market
        Inventory marketInv = new Inventory(150, 6, 7, 2);
        Market market = new Market(marketInv, 10, 11, 12, 9, 8, 7);
        Inventory playerInv = new Inventory(50, 0, 0, 0);
        HumanPlayer player = new HumanPlayer("p1", playerInv, market, new Sprite());
        Tile tile = new Tile(2, 23, 4, 3);
        marketInv.AddTile(tile);
        //Gets player to buy the tile
        market.BuyTile(tile, player);

        //Checks that the tile is in the player's inventory and that the player's money has been reduced by the tile price
        Assert.Contains(tile, playerInv.Tiles);
        Assert.AreEqual(50 - 23, playerInv.Money);
    }

    [Test]
    public void BuyTileNotEnoughMoney()
    {
        //Creates a market and player and their inventories and assigns a tile to the market
        Inventory marketInv = new Inventory(150, 6, 7, 2);
        Market market = new Market(marketInv, 10, 11, 12, 9, 8, 7);
        Inventory playerInv = new Inventory(22, 0, 0, 0);
        HumanPlayer player = new HumanPlayer("p1", playerInv, market, new Sprite());
        Tile tile = new Tile(2, 23, 4, 3);
        marketInv.AddTile(tile);

        //Tests if an exception is thrown if the player doesn't have enough money
        Assert.Throws<NotEnoughMoneyException>(() => market.BuyTile(tile, player));
    }

    [Test]
    public void BuyTileNotInMarket()
    {
        //Creates a market and player and their inventories and creates a tile
        Inventory marketInv = new Inventory(150, 6, 7, 2);
        Market market = new Market(marketInv, 10, 11, 12, 9, 8, 7);
        Inventory playerInv = new Inventory(50, 0, 0, 0);
        HumanPlayer player = new HumanPlayer("p1", playerInv, market, new Sprite());
        Tile tile = new Tile(2, 23, 4, 3);

        //Checks that an exception is thrown if the tile is not in the market
        Assert.Throws<ArgumentOutOfRangeException>(() => market.BuyTile(tile, player));
    }
}
