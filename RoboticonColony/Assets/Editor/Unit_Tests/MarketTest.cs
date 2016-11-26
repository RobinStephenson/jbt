using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class MarketTest {

    [Test]
    public void CreateMarketWithoutInventory()
    {
        //Create an empty market instance
        Market market = new Market();

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
        Market market = new Market(inv);

        //Verify that the market contains the items from the inventory
        Assert.AreEqual(1, market.Stock.Money);
        Assert.AreEqual(4, market.Stock.GetItemAmount(ItemType.Ore));
        Assert.AreEqual(2, market.Stock.GetItemAmount(ItemType.Power));
        Assert.AreEqual(3, market.Stock.GetItemAmount(ItemType.Roboticon));
    }

    [Test]
    public void GettingBuyPrices()
    {
        //Create an empty market instance
        Market market = new Market();

        //Verify the buy price is the same as the price set in the markets construction
        //TEMP: This will likely get set an alternate way in future
        Assert.AreEqual(10, market.GetBuyPrice(ItemType.Ore));
        Assert.AreEqual(11, market.GetBuyPrice(ItemType.Power));
        Assert.AreEqual(12, market.GetBuyPrice(ItemType.Roboticon));
    }

    [Test]
    public void GettingSellPrices()
    {
        //Create an empty market instance
        Market market = new Market();

        //Verify the sell price is the same as the price set in the markets construction
        //TEMP: This will likely get set an alternate way in future
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
        Market market = new Market(marketInv);

        //Create a mock player inventory
        Inventory playerInv = new Inventory(50, 0, 0, 0);

        //Attempt to buy 5 power from the market at the cost of 11 each, more than the player can afford
        bool successful = market.Buy(ItemType.Power, 5, playerInv);

        //Check if the purchase was successful, which it shouldnt have been, and check if both inventories contain the same amount of power and money
        Assert.AreEqual(false, successful);
        Assert.AreEqual(7, market.Stock.GetItemAmount(ItemType.Power));
        Assert.AreEqual(0, playerInv.GetItemAmount(ItemType.Power));
        Assert.AreEqual(5, market.Stock.Money);
        Assert.AreEqual(50, playerInv.Money);

        //Now try to buy 3 roboticons, when the market only has 2, which should not work
        successful = market.Buy(ItemType.Roboticon, 3, playerInv);

        //Check if the purchase was successful, which it shouldnt have been, and check if both inventories contain the same amount of roboticons and money
        Assert.AreEqual(false, successful);
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
        Market market = new Market(marketInv);

        //Create a mock player inventory
        Inventory playerInv = new Inventory(50, 0, 0, 0);

        //Attempt to buy 3 power from the market at the cost of 11 each, which the player can afford
        bool successful = market.Buy(ItemType.Power, 3, playerInv);

        //Check if the purchase was successful, which it should have been, and check if both inventories have been updated accordingly
        Assert.AreEqual(true, successful);
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
        Market market = new Market(marketInv);

        //Create a mock player inventory
        Inventory playerInv = new Inventory(37, 3, 6, 27);

        //Attempt to sell 5 ore to the market at the cost of 9 each, which is more ore than the player has
        bool successful = market.Sell(ItemType.Ore, 5, playerInv);

        //Check if the sell was successful, which it shouldnt have been, and check if both inventories contain the same amount of ore and money
        Assert.AreEqual(false, successful);
        Assert.AreEqual(6, market.Stock.GetItemAmount(ItemType.Ore));
        Assert.AreEqual(3, playerInv.GetItemAmount(ItemType.Ore));
        Assert.AreEqual(150, market.Stock.Money);
        Assert.AreEqual(37, playerInv.Money);

        //Now try to sell 25 roboticons at 7 each, which the market cannot afford
        successful = market.Sell(ItemType.Roboticon, 25, playerInv);

        //Check if the sell was successful, which it shouldnt have been, and check if both inventories contain the same amount of roboticons and money
        Assert.AreEqual(false, successful);
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
        Market market = new Market(marketInv);

        //Create a mock player inventory
        Inventory playerInv = new Inventory(37, 3, 6, 27);

        //Attempt to sell 4 power to the market at the cost of 8 each, which should work
        bool successful = market.Sell(ItemType.Power, 4, playerInv);

        //Check if the sell was successful, which it should have been, and check if both inventories have been updated accordingly
        Assert.AreEqual(true, successful);
        Assert.AreEqual(11, market.Stock.GetItemAmount(ItemType.Power));
        Assert.AreEqual(2, playerInv.GetItemAmount(ItemType.Power));
        Assert.AreEqual(118, market.Stock.Money);
        Assert.AreEqual(69, playerInv.Money);
    }
}
