using UnityEngine;
using System;
using UnityEditor;
using NUnit.Framework;
using NUnit;
using System.Collections.Generic;

public class RoboticonFactoryTest
{
    [Test]
    public void CreateNewRoboticonCustomisation()
    {
        //Creates a new RoboticonFactory instance
        RoboticonFactory TestFactory = new RoboticonFactory();

        //Check if lists are empty.
        Assert.AreEqual(null, TestFactory.RoboticonList);
    }

    [Test]
    public void CreateNewRoboticon()
    {
        //Creates a new Tile instance
        Tile selectedTile = new Tile();

        //Creates a new RoboticonFactory instance
        RoboticonFactory TestFactory = new RoboticonFactory();
        TestFactory.CreateRoboticon(selectedTile);

        //Check if lists are empty.
        Assert.AreEqual(1, TestFactory.RoboticonList.count);
    }

    [Test]
    public void SuccessfulBuyCustomisation()
    {
        //Create a new RoboticonCustomisation instance
        List<RoboticonCustomisation> prelist = new List<RoboticonCustomisation>();
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test", 2, prelist, ItemType.Ore, 10);

        //Creates a new Tile instance
        Tile selectedTile = new Tile();

        //Creates a new Roboticon instance
        Roboticon TestRobo = new Roboticon(selectedTile);

        //Creates a new RoboticonFactory instance
        RoboticonFactory TestFactory = new RoboticonFactory();

        //Buy customisation
        TestFactory.BuyCustomisation(TestRobo, NewCustomisation, 10);

        //Check if Roboticon has been customised
        Assert.AreEqual(1, TestRobo.CurrentCustomisations.count);
    }

    public void FailedBuyCustomisationViaMoney()
    {
        //Create a new RoboticonCustomisation instance
        List<RoboticonCustomisation> prelist = new List<RoboticonCustomisation>();
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test", 2, prelist, ItemType.Ore, 10);

        //Creates a new Tile instance
        Tile selectedTile = new Tile();

        //Creates a new Roboticon instance
        Roboticon TestRobo = new Roboticon(selectedTile);

        //Creates a new RoboticonFactory instance
        RoboticonFactory TestFactory = new RoboticonFactory();

        //Buy customisation
        TestFactory.BuyCustomisation(TestRobo, NewCustomisation, 5);

        //Check to ensure the class fails when a roboticon is used as the ItemType
        Assert.True(TestHelper.Throws(() => TestFactory.BuyCustomisation(TestRobo, NewCustomisation, 5), typeof(ArgumentException)));
    }

    public void FailedBuyCustomisationViaRequirements()
    {
        //Create 2 new RoboticonCustomisation instance and load the second initialisation with a non-empty prerequisite list
        List<RoboticonCustomisation> prelist = new List<RoboticonCustomisation>();
        RoboticonCustomisation PreCustomisation = new RoboticonCustomisation("req", 2, prelist, ItemType.Ore, 10);
        prelist.Add(PreCustomisation);
        RoboticonCustomisation NewCustomisation = new RoboticonCustomisation("test", 2, prelist, ItemType.Ore, 10);

        //Creates a new Tile instance
        Tile selectedTile = new Tile();

        //Creates a new Roboticon instance
        Roboticon TestRobo = new Roboticon(selectedTile);

        //Creates a new RoboticonFactory instance
        RoboticonFactory TestFactory = new RoboticonFactory();

        //Buy customisation
        TestFactory.BuyCustomisation(TestRobo, NewCustomisation, 10);

        //Check to ensure the class fails when a roboticon is used as the ItemType
        Assert.True(TestHelper.Throws(() => TestFactory.BuyCustomisation(TestRobo, NewCustomisation, 20), typeof(ArgumentException)));
    }

}
