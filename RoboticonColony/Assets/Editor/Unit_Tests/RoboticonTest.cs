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
        Tile currentTile = new Tile();
        Roboticon NewRoboticon = new Roboticon(currentTile);

        //Check if all resources have initial value set to one, lists are empty and the current tile has been assigned correctly.
        Assert.AreEqual(1, NewRoboticon.BonusProductionGetter(ItemType.Ore));
        Assert.AreEqual(1, NewRoboticon.BonusProductionGetter(ItemType.Power));
        Assert.AreEqual(0, NewRoboticon.CurrentCustomisations.Count);
        Assert.AreEqual(currentTile, NewRoboticon.CurrentTile);
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
        Assert.AreEqual(1, NewRoboticon.BonusProductionGetter(ItemType.Power));
        Assert.AreEqual(prelist, NewRoboticon.CurrentCustomisations);
    }



    public void FailedCustomiseRoboticon()
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
}
