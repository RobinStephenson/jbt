using UnityEngine;
using System;
using UnityEditor;
using NUnit.Framework;
using NUnit;
using System.Collections.Generic;

public class RoboticonCustomisationTest
{
    [Test]
    public void SuccessfulCreateNewRoboticonCustomisation()
    {
        //Create a new RoboticonCustomisation instance
        Dictionary<ItemType, int> multiplier = new Dictionary<ItemType, int>();
        multiplier[ItemType.Ore] = 2;
        multiplier[ItemType.Power] = 1;
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test ore x2", multiplier, null, 8, "");

        //Check if all resources have initial value set to one, lists are empty and the current tile has been assigned correctly.
        Assert.AreEqual("test ore x2", NewCustomisation.Name);
        Assert.AreEqual(2, NewCustomisation.GetMultiplier(ItemType.Ore));
        Assert.AreEqual(1, NewCustomisation.GetMultiplier(ItemType.Power));
        Assert.AreEqual(null, NewCustomisation.Prerequisites);
        Assert.AreEqual(8, NewCustomisation.Price);
    }

    [Test]
    public void TileProduceWithCustomisationTest()
    {
        //Create tile and assign roboticon to tile
        Tile tile = new Tile(2, 4, 3, 2);
        Inventory playerInv = new Inventory(100, 10, 10, 10);
        HumanPlayer player = new HumanPlayer("P1", playerInv, new Market(2, 2, 2, 2, 2, 2), new Sprite());
        tile.InstallRoboticon(player);

        //Create a new RoboticonCustomisation instance
        Dictionary<ItemType, int> multiplier = new Dictionary<ItemType, int>();
        multiplier[ItemType.Ore] = 1;
        multiplier[ItemType.Power] = 2;
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test power x2", multiplier, null, 8, "");
        tile.InstalledRoboticon.AddCustomisation(NewCustomisation);

        //Check if the correct values for ore and power are returned, ore should be 3, whilst power should be 4, as it is multipled by 2
        Dictionary<ItemType, int> production = tile.Produce();
        Assert.AreEqual(production[ItemType.Ore], 3);
        Assert.AreEqual(production[ItemType.Power], 4);
    }
}
