using UnityEngine;
using System;
using UnityEditor;
using NUnit.Framework;
using NUnit;
using System.Collections.Generic;

public class RoboticonTest
{
    [Test]
    public void CreateNewRoboticon()
    {

        //Create an empty Roboticon instance
        Tile current_tile = new Tile();
        Roboticon NewRoboticon = new Roboticon(current_tile);

        //Check if all resources have initial value set to zero and lists are empty.
        Assert.AreEqual(1, NewRoboticon.BonusProductionGetter(ItemType.Ore));
        Assert.AreEqual(1, NewRoboticon.BonusProductionGetter(ItemType.Power));
        Assert.AreEqual(0, NewRoboticon.CurrentCustomisations.Count);
    }

    [Test]
    public void SuccessfulCustomiseRoboticon()
    {
        //Create a new RoboticonCustomisation instance
        List<RoboticonCustomisation> prelist = new List<RoboticonCustomisation>();
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test", 2, prelist, ItemType.Ore);

        //Create an empty Roboticon instance
        Tile current_tile = new Tile();
        Roboticon NewRoboticon = new Roboticon(current_tile);

        //Customise the empty roboticon
        NewRoboticon.CustomiseRoboticon(NewCustomisation);

        //Check if all resources have the value they were set
        Assert.AreEqual(2, NewRoboticon.BonusProductionGetter(ItemType.Ore));
        Assert.AreEqual(2, NewRoboticon.BonusProductionGetter(ItemType.Power));
        Assert.AreEqual(prelist, NewRoboticon.CurrentCustomisations);
    }

    [Test]
    public void AddMoneyToInventory()
    {
        //Create an empty inventory instance
        Inventory inv = new Inventory();

        //Prove that the inventory has no money
        Assert.AreEqual(0, inv.Money);

        //Now add money
        inv.AddMoney(6);

        //Check if the money has successfully been added
        Assert.AreEqual(6, inv.Money);
    }

    [Test]
    public void AddItemToInventory()
    {
        //Create an empty inventory instance
        Inventory inv = new Inventory();

        //Prove that the inventory has no ore
        Assert.AreEqual(0, inv.GetItemAmount(ItemType.Ore));

        //Now add money
        inv.AddItem(ItemType.Ore, 6);

        //Check if the ore has successfully been added
        Assert.AreEqual(6, inv.GetItemAmount(ItemType.Ore));
    }

    [Test]
    public void FailedMoneyTransaction()
    {
        //Create two inventories
        Inventory inv1 = new Inventory(1, 2, 3, 4);
        Inventory inv2 = new Inventory(10, 11, 12, 13);

        //Attempt to transfer 2 money from inv1 to inv2, even though inv1 only has 1 money, which should throw an error
        Assert.True(TestHelper.Throws(() => inv1.TransferMoney(2, inv2), typeof(ArgumentOutOfRangeException)));

        //Check that both inventories still have the amount of money they started with
        Assert.AreEqual(1, inv1.Money);
        Assert.AreEqual(10, inv2.Money);
    }

    [Test]
    public void SuccessfulMoneyTransaction()
    {
        //Create two inventories
        Inventory inv1 = new Inventory(8, 2, 3, 4);
        Inventory inv2 = new Inventory(10, 11, 12, 13);
        
        //Attempt to transfer 2 money from inv1 to inv2, which should work
        inv1.TransferMoney(2, inv2);

        //Check that the amount of money in both inventories has changed due to the transaction
        Assert.AreEqual(6, inv1.Money);
        Assert.AreEqual(12, inv2.Money);
    }

    [Test]
    public void FailedItemTransaction()
    {
        //Create two inventories
        Inventory inv1 = new Inventory(3, 7, 2, 5);
        Inventory inv2 = new Inventory(8, 2, 4, 7);

        //Attempt to transfer 5 power from inv2 to inv1, even though inv2 only has 4 power, which should throw an error
        Assert.True(TestHelper.Throws(() => inv2.TransferItem(ItemType.Power, 5, inv1), typeof(ArgumentOutOfRangeException)));

        //Check that the two inventories still have the same amount of power they started with
        Assert.AreEqual(2, inv1.GetItemAmount(ItemType.Power));
        Assert.AreEqual(4, inv2.GetItemAmount(ItemType.Power));
    }

    [Test]
    public void SuccessfulItemTransaction()
    {
        //Create two inventories
        Inventory inv1 = new Inventory(3, 7, 2, 5);
        Inventory inv2 = new Inventory(8, 2, 4, 7);

        //Attempt to transfer 3 power from inv2 to inv1, which should work
        inv2.TransferItem(ItemType.Power, 3, inv1);

        //Check that the two inventories power amounts have changed due to the transaction
        Assert.AreEqual(5, inv1.GetItemAmount(ItemType.Power));
        Assert.AreEqual(1, inv2.GetItemAmount(ItemType.Power));
    }
}
