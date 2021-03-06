﻿using UnityEngine;
using System;
using UnityEditor;
using NUnit.Framework;
using NUnit;

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
    public void CreateNegativeInventory()
    {
        //Attempt to create an inventory with some negative items, which should not work and return an error
        Assert.Throws<ArgumentOutOfRangeException>(() => NegativeInventory());
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
        Assert.Throws<NotEnoughMoneyException>(() => inv1.TransferMoney(2, inv2));

        //Attempt to transfer -1 money from inv1 to inv2, which should throw an error as it is not possible
        Assert.Throws<ArgumentOutOfRangeException>(() => inv1.TransferMoney(-1, inv2));

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
        Assert.Throws<NotEnoughItemException>(() => inv2.TransferItem(ItemType.Power, 5, inv1));

        //Attempt to transfer -2 power from inv2 to inv1, which should throw an error as it is not possible
        Assert.Throws<ArgumentOutOfRangeException>(() => inv2.TransferItem(ItemType.Power, -2, inv1));

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

    [Test]
    public void SuccessfulTileTransfer()
    {
        //creates 2 players, adds a tile to one of the players' inventories then transfers the tile
        Tile tile = new Tile(1, 5, 2, 3);
        Inventory playerInv = new Inventory(100, 10, 10, 10);
        Inventory playerToInv = new Inventory(100, 10, 10, 10);
        HumanPlayer player = new HumanPlayer("P1", playerInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());
        HumanPlayer playerTo = new HumanPlayer("p2", playerToInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());
        playerInv.AddTile(tile);
        playerInv.TransferTile(tile, playerToInv);

        //Check that both inventories have the right number of tiles after transfer and that the tile is contained in the new inventory
        Assert.AreEqual(1, playerToInv.TileCount());
        Assert.AreEqual(0, playerInv.TileCount());
        Assert.Contains(tile, playerToInv.Tiles);
    }

    [Test]
    public void FailedTileTransfer()
    {
        //creates 2 players and adds a tile to one of the players' inventories
        Tile tile = new Tile(1, 5, 2, 3);
        Inventory playerInv = new Inventory(100, 10, 10, 10);
        Inventory playerToInv = new Inventory(100, 10, 10, 10);
        HumanPlayer player = new HumanPlayer("P1", playerInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());
        HumanPlayer playerTo = new HumanPlayer("p2", playerToInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());
        playerToInv.AddTile(tile);
        
        //Checks that an exception is thrown if the player does not own the tile being transferred
        Assert.Throws<ArgumentOutOfRangeException>(() => playerInv.TransferTile(tile, playerToInv));
    }

    /// <summary>
    /// Used by the CreateNegativeInventory Test method to attempt to create an inventory with some negative items
    /// </summary>
    Inventory NegativeInventory()
    {
        return new Inventory(46, 3, -5, 2);
    }
}
