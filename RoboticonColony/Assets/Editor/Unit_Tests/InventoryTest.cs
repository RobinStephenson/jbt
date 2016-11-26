﻿using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class InventoryTest
{
    [Test]
    public void CreateEmptyInventory()
    {
        //Create an empty inventory instance
        Inventory inv = new Inventory();

        //Check if all resources have initial value set to zero
        Assert.AreEqual(0, inv.Money);
        Assert.AreEqual(0, inv.GetItemAmount(ItemType.Ore));
        Assert.AreEqual(0, inv.GetItemAmount(ItemType.Power));
        Assert.AreEqual(0, inv.GetItemAmount(ItemType.Roboticon));
    }

    [Test]
    public void CreatePopulatedInventory()
    {
        //Create a populated inventory instance
        Inventory inv = new Inventory(4, 3, 2, 1);

        //Check if all resources have the value they were set
        Assert.AreEqual(4, inv.Money);
        Assert.AreEqual(3, inv.GetItemAmount(ItemType.Ore));
        Assert.AreEqual(2, inv.GetItemAmount(ItemType.Power));
        Assert.AreEqual(1, inv.GetItemAmount(ItemType.Roboticon));
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

        //Attempt to transfer 2 money from inv1 to inv2, even though inv1 only has 1 money
        bool successful = inv1.TransferMoney(2, inv2);

        //Check if the transfer failed
        Assert.AreEqual(false, successful);

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
        bool successful = inv1.TransferMoney(2, inv2);

        //Check if the transfer failed
        Assert.AreEqual(true, successful);

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

        //Attempt to transfer 5 power from inv2 to inv1, even though inv2 only has 4 power
        bool successful = inv2.TransferItem(ItemType.Power, 5, inv1);

        //Check if the transfer was successful
        Assert.AreEqual(false, successful);

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
        bool successful = inv2.TransferItem(ItemType.Power, 3, inv1);

        //Check if the transfer was successful
        Assert.AreEqual(true, successful);

        //Check that the two inventories power amounts have changed due to the transaction
        Assert.AreEqual(5, inv1.GetItemAmount(ItemType.Power));
        Assert.AreEqual(1, inv2.GetItemAmount(ItemType.Power));
    }
}